namespace Dida.Waylen.Onboarding.Demo.Infrastructure.Database.Sqlite;

public static class SqliteDatabaseManager
{
    public static void Reigster()
    {
        DatabaseMesh.TryRegisterDatabaseProvider(SqliteDatabaseProvider.GlobalDatabaseType, new SqliteDatabaseProvider());
    }
}
