using Dida.Waylen.Onboarding.Demo.Infrastructure.Database.Sqlite;

namespace Dida.Waylen.Onboarding.Demo.Infrastructure.Database.EF.Sqlite;

public class SqliteDbContextProvider : DbContextProviderBase
{
    public override DatabaseType DatabaseType => SqliteDatabaseProvider.GlobalDatabaseType;

    public override string GetDatabaseVersion(string connectionString, int commandTimeout = 5)
    {
        throw new NotSupportedException("Unsupported Sqlite Database");
    }

    public override TDbContext GetDbContext<TDbContext>(DbContextOptionsBuilder<TDbContext> dbContextOptionsBuilder, string connectionString, Func<DbContextOptions, object> func)
    {
        throw new NotSupportedException("Unsupported Sqlite Database");
    }
}
