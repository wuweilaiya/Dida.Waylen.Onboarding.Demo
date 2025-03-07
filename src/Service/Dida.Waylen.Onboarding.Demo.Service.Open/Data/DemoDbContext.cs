namespace Dida.Waylen.Onboarding.Demo.Data
{
    [GSqliteDatabase]
    public class DemoDbContext : GDbContext
    {
        public DemoDbContext()
        {
        }

        public DemoDbContext(DbContextOptions<DemoDbContext> options, IServiceProvider? serviceProvider)
            : base(options, serviceProvider)
        {
        }

        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            //todo 日志
        }
    }
}
