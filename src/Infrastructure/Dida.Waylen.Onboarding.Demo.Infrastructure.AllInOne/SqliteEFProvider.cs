using System.Reflection;
using Core.DataModel.Enums.Cms.Attributes;
using Core.DataModel.Models.DidaSystem.Project.Settings.Model;
using Core.DataModel.Models.Internal.Database.Attributes;
using Core.DataModel.Models.Internal.Database.Enum;
using Core.Plugin.Data.Auditing;
using Core.Plugin.Data.Database;
using Core.Plugin.Data.Database.ConnectionManagers;
using Core.Plugin.Data.Database.EntityFrameworkCore;
using Core.Plugin.Data.Database.EntityFrameworkCore.DbContextFactories;
using Core.Plugin.Data.Database.EntityFrameworkCore.MySql;
using Core.Plugin.Data.Database.EntityFrameworkCore.SqlServer;
using Core.Plugin.Data.EFCore;
using Core.Plugin.Data.EFCore.DataFilter;
using Core.Plugin.Web.MinimalAPIs.AllInOne;
using Core.Plugin.Web.MinimalAPIs.AllInOne.Providers;
using Dida.Waylen.Onboarding.Demo.Infrastructure.Database.EF.Sqlite;
using Dida.Waylen.Onboarding.Demo.Infrastructure.Database.Sqlite;
using Framework.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.Plugin.Web.MinimalAPIs.AllInOne.Interceptors;

namespace Dida.Waylen.Onboarding.Demo.Infrastructure.AllInOne;

public static class SqliteEFProvider
{
    private const string ConnectionStringPosition = "Dida:DbConnections";

    private static ManualMemoryCache<Type, string> _connectionStringData = new ManualMemoryCache<Type, string>();

    public static DidaAllInOneBuilder AddSqliteEFProvider(this DidaAllInOneBuilder builder)
    {
        var providerInterceptorType = typeof(EFProvider).Assembly.GetType("Core.Plugin.Web.MinimalAPIs.AllInOne.Interceptors.ProviderInterceptor");
        MethodInfo method = providerInterceptorType.GetMethod("AddServices", BindingFlags.NonPublic | BindingFlags.Static);
        method.Invoke(null, new object[] { builder, AllInOneProvider.EF, OnConfigureServices, OnConfigure });

        return builder;
    }

    private static void OnConfigureServices(DidaAllInOneBuilder builder)
    {
        builder.OnConfigureServices += delegate (IServiceCollection services)
        {
            services.TryAddScoped(typeof(DataFilter<>));
            services.TryAddScoped<IDataFilter, DataFilter>();
            if (builder.Options.EnabledNewConnectionString)
            {
                ConnectionStringManager.EnabledDbConnectionAccount = true;
            }
            if (builder.Options.ConnectionStrings is null)
            {
                var dbConnections = builder.Options.WebApplicationBuilder.Configuration.GetSection(ConnectionStringPosition).Get<DbConnectionModel[]>()!;
                var dbContextType = typeof(DbContext);
                var databaseName = builder.Options.DatabaseName ?? builder.Options.ProjectName;
                var connectionStrings = (builder.Options.InjectAssemblies ?? AppDomain.CurrentDomain.GetSafeAssemblies())
                    .SelectMany(assembly => assembly.GetTypes().Where(type => type.IsAssignableTo(dbContextType) && !type.Equals(dbContextType)
                        && type != typeof(GDbContext) && !type.IsAbstract && type.Namespace != "Microsoft.AspNetCore.Identity.EntityFrameworkCore"))
                    .Select(type =>
                    {
                        if (builder.Options.EnabledNewConnectionString)
                        {
                            return GetConnectionStringFuncWithNew(type, builder.Options.EnabledConnectionStringCompatible);
                        }
                        else
                        {
                            return GetConnectionStringFuncWithOld(dbConnections, type);
                        }
                    })
                    .ToList();

                builder.Options.ConnectionStrings = () => connectionStrings;
            }
            builder.Options.ConnectionStrings?.Invoke().ForEach(cs =>
            {
                var parameterTypes = new[] { typeof(IServiceCollection), typeof(Action<DbContextOptionsBuilder>), typeof(ServiceLifetime), typeof(ServiceLifetime) };
                MethodInfo addDbContextMethodInfo;
                if (typeof(Core.Plugin.Data.EFCore.GDbContext).IsAssignableFrom(cs.ContextType))
                {
                    addDbContextMethodInfo = typeof(AddGDbContextServiceCollectionExtensions).GetMethods().FirstOrDefault(mi =>
                    {
                        if (mi.Name != nameof(AddGDbContextServiceCollectionExtensions.AddGDbContext)) return false;
                        if (!mi.IsGenericMethod) return false;
                        if (mi.GetGenericArguments().Length != 1) return false;

                        return mi.GetParameters().All(pi => parameterTypes.Contains(pi.ParameterType));
                    })!;
                }
                else
                {
                    addDbContextMethodInfo = typeof(EntityFrameworkServiceCollectionExtensions).GetMethods().FirstOrDefault(mi =>
                    {
                        if (mi.Name != nameof(EntityFrameworkServiceCollectionExtensions.AddDbContext)) return false;
                        if (!mi.IsGenericMethod) return false;
                        if (mi.GetGenericArguments().Length != 1) return false;

                        return mi.GetParameters().All(pi => parameterTypes.Contains(pi.ParameterType));
                    })!;
                }

                var addDbContextGenericMethodInfo = addDbContextMethodInfo.MakeGenericMethod(cs.ContextType);
                var dataBaseType = GetDatabaseType(cs.ContextType);
                TryRegisterDbContextManager(dataBaseType);

                var useDatabaseAction = (DbContextOptionsBuilder options) =>
                {
                    if (dataBaseType == DatabaseType.SqlServer)
                    {
                        if (builder.Options.UseHierarchyId)
                        {
                            options.UseSqlServer(cs.ConnectionString(), sqlServerDbContextOptionsBuilder =>
                            {
                                sqlServerDbContextOptionsBuilder.UseHierarchyId();
                                builder.Options.SqlServerOptionsAction?.Invoke(cs.ContextType, sqlServerDbContextOptionsBuilder);
                            });
                        }
                        else
                        {
                            options.UseSqlServer(cs.ConnectionString(), sqlServerDbContextOptionsBuilder =>
                            {
                                builder.Options.SqlServerOptionsAction?.Invoke(cs.ContextType, sqlServerDbContextOptionsBuilder);
                            });
                        }
                    }
                    else if (dataBaseType == DatabaseType.MySql)
                    {
#if NET8_0_OR_GREATER
                        var connectionString = cs.ConnectionString();
                        var version = DbContextMesh.GetDatabaseVersion(DatabaseType.MySql, connectionString, 5);
                        options.UseMySql(connectionString, new MySqlServerVersion(version), mysqlDbContextOptionsBuilder =>
                        {
                            builder.Options.MySqlOptionsAction?.Invoke(cs.ContextType, mysqlDbContextOptionsBuilder);
                        });
#else
        throw new NotSupportedException("Unsupported TargetFramework version");
#endif
                    }
                    else if(dataBaseType == SqliteDatabaseProvider.GlobalDatabaseType)
                    {
                        var connectionString = cs.ConnectionString();
                        options.UseSqlite(connectionString);
                    }

                    builder.Options.DbContextOptionsAction?.Invoke(cs.ContextType, options);
                };

                _ = addDbContextGenericMethodInfo.Invoke(null, new object[] { services, useDatabaseAction, ServiceLifetime.Scoped, ServiceLifetime.Scoped })!;
            });
        };

        static void TryRegisterDbContextManager(DatabaseType dataBaseType)
        {
            switch (dataBaseType)
            {
                case DatabaseType.SqlServer:
                    SqlServerDbContextManager.TryReigster();
                    break;
                case DatabaseType.MySql:
                    MySqlDbContextManager.TryReigster();
                    break;
                case SqliteDatabaseProvider.GlobalDatabaseType:
                    SqliteDbContextManager.TryReigster();
                    break;
            }
        }
    }

    static DatabaseType GetDatabaseType(Type dbContextType)
    {
        var attribute = dbContextType.GetCustomAttribute<GDatabaseAttribute>();

        return attribute?.DatabaseType ?? default;
    }

    private static (Type ContextType, Func<string> ConnectionString) GetConnectionStringFuncWithNew(Type dbContextType, bool enabledConnectionStringCompatible)
    {
        var dataBaseType = GetDatabaseType(dbContextType);
        if(dataBaseType == SqliteDatabaseProvider.GlobalDatabaseType)
        {
            SqliteDatabaseManager.Reigster();
            return (dbContextType, () => GDbContextFactory.GetConnectionString(dbContextType, delegate
            {
            }, enabledConnectionStringCompatible));
        }
        return (dbContextType, () => GDbContextFactory.GetConnectionString(dbContextType, delegate
        {
        }, enabledConnectionStringCompatible));
    }

    /// <summary>
    /// 获取数据库连接字符串 (旧方式, 不支持 Mysql 数据库)
    /// </summary>
    private static (Type ContextType, Func<string> ConnectionString) GetConnectionStringFuncWithOld(
        DbConnectionModel[] dbConnections,
        Type dbContextType)
    {
        var dataBaseType = GetDatabaseType(dbContextType);
        if (dataBaseType == SqliteDatabaseProvider.GlobalDatabaseType)
        {
            SqliteDatabaseManager.Reigster();
            //数据库名优先级：DataBaseName > ProjectName > 类名（移除DbConntext） 特殊情况：使用CmsContent特性的不替换库名
            (Type ContextType, Func<string> ConnectionString) connectionString = (dbContextType, () =>
            {
                var dbConnection = dbConnections.FirstOrDefault(connection => dbContextType.Name.Equals($"{connection.Name}DbContext", StringComparison.OrdinalIgnoreCase)) ?? throw new Exception($"DbConnection {dbContextType.Name} not found");
                return dbConnection.ConnectionString;
            }
            );


            return connectionString;
        }

        throw new Exception();
    }

    private static void OnConfigure(DidaAllInOneBuilder builder)
    {
    }
}


public static class AddGDbContextServiceCollectionExtensions
{
    /// <summary>
    /// 支持自动开启事务
    /// </summary>
    public static IServiceCollection AddGDbContext
        <TContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder>? optionsAction = null,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContext : GDbContext
    {
        services.AddAuditingService();

        services.TryAddScoped(typeof(DataFilter<>));
        services.TryAddScoped<IDataFilter, DataFilter>();

        return services.AddDbContext<TContext>(dbContextOptionsBuilder =>
        {
            //dbContextOptionsBuilder.UseAutoCommit();
            optionsAction?.Invoke(dbContextOptionsBuilder);
        }, contextLifetime, optionsLifetime);
    }
}
