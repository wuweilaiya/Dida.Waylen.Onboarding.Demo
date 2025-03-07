var builder = WebApplication.CreateBuilder(args);

var aioOptions = new DidaAllInOneOptions(builder, Projects.DidaApi);

aioOptions.OnConfigureServicesBefore(AllInOneProvider.EF, service =>
{
    service.AddScoped<IUnitOfWork, UnitOfWork<DemoDbContext>>();

    service.AddLocalEventBus(localEventBusOptions: options =>
    {
    });
    service.AddSingleton(sp => UniversalIDGeneratorFactory.BuildSnowflake(1, SnowflakeIDSetting.CreateDefault()));
});

aioOptions.OnConfigureServicesBefore(AllInOneProvider.DI, services =>
{
    services.AddFluentValidation();
});

var app = builder.Services.AddDidaAllInOneDemo(aioOptions);

app.MapGet("/", () => $"Hello {aioOptions.ProjectName}!");

await app.RunAsync();
