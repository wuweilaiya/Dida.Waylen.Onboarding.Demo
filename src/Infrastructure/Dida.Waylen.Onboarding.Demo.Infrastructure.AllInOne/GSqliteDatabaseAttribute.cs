using Core.DataModel.Models.Internal.Database.Attributes;
using Dida.Waylen.Onboarding.Demo.Infrastructure.Database.Sqlite;

namespace Dida.Waylen.Onboarding.Demo.Infrastructure.AllInOne;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class GSqliteDatabaseAttribute : GDatabaseAttribute
{
    public string ConnectionStringSufix { get; set; }

    public GSqliteDatabaseAttribute()
    {
        base.DatabaseType = SqliteDatabaseProvider.GlobalDatabaseType;
    }

    public GSqliteDatabaseAttribute(string name)
        : base(name)
    {
        base.DatabaseType = SqliteDatabaseProvider.GlobalDatabaseType;
    }
}
