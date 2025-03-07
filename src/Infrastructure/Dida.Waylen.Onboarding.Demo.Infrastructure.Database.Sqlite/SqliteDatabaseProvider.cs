namespace Dida.Waylen.Onboarding.Demo.Infrastructure.Database.Sqlite;

public class SqliteDatabaseProvider : DatabaseProviderBase
{
    public const DatabaseType GlobalDatabaseType = (DatabaseType)3;
    public override DatabaseType DatabaseType => GlobalDatabaseType;

    public override bool TryGetConnectionString(string dataSource, string userId, string password, string databaseName, out string connectionString)
    {
        try
        {
            connectionString = dataSource;
            var connectionStringActual = $"{connectionString};Timeout=5;";

            if (!DatabaseMesh.ExistDatabaseName(GlobalDatabaseType, dataSource, databaseName, GetDatabaseOptions(connectionStringActual)))
            {
                connectionString = string.Empty;
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"{GetType().Name} 获取链接字符串失败,DataSource:{dataSource};", ex);
        }
    }

    public override string GetConnectionString(string connectionString, ConnectionStringOptions connectionStringOptions) => new SqliteConnectionStringBuilder(connectionString).BuildConnectionString(connectionStringOptions);


    public override List<string> GetDatabaseNames(DatabaseOptions databaseOptions)
    {
        return [];
    }

    public override string GetDatabaseVersion(string connectionString, int commandTimeout = 5)
    {
        using var connection = new SqliteConnection(connectionString);
        try
        {
            connection.Open();
            string query = "SELECT SQLITE_VERSION();";
            var command = new SqliteCommand(query, connection)
            {
                CommandTimeout = commandTimeout
            };
            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                return reader.GetString(0);
            }

            return "unknow";
        }
        catch (Exception ex)
        {
#if DEBUG
            Console.WriteLine($"[{GetType().Name}] {nameof(GetDatabaseVersion)} Failed: {ex.GetBaseException().ToString()}");
#endif
            throw;
        }
    }

    static DatabaseOptions GetDatabaseOptions(string connectionString, int commandTimeout = 5)
    {
        var databaseOptions = new DatabaseOptions(connectionString);
        databaseOptions.Args.Add("CommandTimeout", commandTimeout);

        return databaseOptions;
    }
}
