var builder = WebApplication.CreateBuilder(args);

var aioOptions = new DidaAllInOneOptions(builder, Projects.DidaApi);
aioOptions.GlobalServiceRouteOptions.AdditionalAssemblies = [typeof(BasicDataService).Assembly];

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

aioOptions.OnConfigureBefore(AllInOneProvider.MinimalAPIs, app =>
{
    app.Use(async (context, next) =>
    {
        if (!context.Request.Path.ToString().Contains("/api"))
        {
            await next(context);

            return;
        }

        var originalBodyStream = context.Response.Body;
        try
        {
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);

            var reader = new StreamReader(memoryStream);
            var responseBody = await reader.ReadToEndAsync();

            var data = JsonSerializer.Deserialize<object>(responseBody);
            await TypedResults.Ok(data).ExecuteAsync(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
        catch
        {
            context.Response.Body = originalBodyStream;
            await next(context);
        }
    });
});

var app = builder.Services.AddDidaAllInOneDemo(aioOptions);

app.MapGet("/", () => $"Hello {aioOptions.ProjectName}!");

await app.RunAsync();
