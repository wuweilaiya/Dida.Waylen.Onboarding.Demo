using Core.Plugin.Data.Database.EntityFrameworkCore;
using Dida.Waylen.Onboarding.Demo.Infrastructure.Database.Sqlite;

namespace Dida.Waylen.Onboarding.Demo.Infrastructure.Database.EF.Sqlite;

public class SqliteDbContextManager
{
    public static void TryReigster()
    {
        DbContextMesh.TryRegisterDbContextProvider(SqliteDatabaseProvider.GlobalDatabaseType, new SqliteDbContextProvider());
    }
}
