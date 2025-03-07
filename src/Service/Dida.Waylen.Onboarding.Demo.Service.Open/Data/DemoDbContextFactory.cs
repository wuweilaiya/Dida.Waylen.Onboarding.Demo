using Core.DataModel.Models.DidaSystem.Project.Settings.Model;
using Microsoft.EntityFrameworkCore.Design;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Data
{
    public class DemoDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<DemoDbContext>();
            var dbConnections = configuration.GetSection("Dida:DbConnections").Get<DbConnectionModel[]>() ?? throw new Exception("请配置数据库连接");
            optionsBuilder.UseSqlite(dbConnections.First().ConnectionString);

            return new DemoDbContext(optionsBuilder.Options, default);
        }
    }
}
