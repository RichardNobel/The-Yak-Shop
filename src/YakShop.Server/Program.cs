using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data;
using YakShop.Server.Data.Repositories;
using YakShop.Server.Helpers;
using YakShop.Server.Models;
using YakShop.Server.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

CreateDbIfNotExists(app);

// TODO: Enable timer service
//var timerService = app.Services.GetRequiredService<TimeLapseSimulationHostedService>();
//timerService.IsEnabled = true;

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// This endpoint simply allows to check if the WebAPI is running.
app.MapGet("/yak", () => "Yak!").WithOpenApi();

// GET /yak-shop/herd/T
app.MapGet(
        "/yak-shop/herd/{daysAfterInit}",
        ([FromRoute] int daysAfterInit) =>
        {
            // TODO: Return herd view.
            return TypedResults.Ok();
        }
    )
    .WithName("GetHerdInfo")
    .WithDescription("Returns a view of your herd after T days.")
    .WithOpenApi();

// GET /yak-shop/stock/T
app.MapGet(
        "/yak-shop/stock/{daysAfterInit}",
        ([FromRoute] int daysAfterInit) =>
        {
            // TODO: Return stock view.
            return TypedResults.Ok();
        }
    )
    .WithName("GetStockInfo")
    .WithDescription("Returns a view of your stock after T days.")
    .WithOpenApi();

// GET /yak-shop/current-stock
app.MapGet(
        "/yak-shop/current-stock",
        ([FromServices] IStatRepository statRepo) =>
        {
            var milkAndSkinsAmounts = statRepo.GetCurrentStockStats();
            return TypedResults.Ok(milkAndSkinsAmounts);
        }
    )
    .WithName("GetCurrentStock")
    .WithDescription("Returns a view of your current stock.")
    .WithOpenApi();

// POST /yak-shop/load
app.MapPost(
        "/yak-shop/load",
        ([FromBody] Herd herd,
         [FromServices] IHerdRepository herdRepo,
         [FromServices] IStatRepository statRepo,
         [FromServices] IProduceDayRepository produceDayRepo) =>
        {
            herdRepo.DeleteHerd();
            herdRepo.CreateHerd(herd);
            int numberOfRecordsCreated = herdRepo.Save();
            logger.LogInformation(
                "A new herd consisting of {Count} yaks was created.",
                numberOfRecordsCreated
            );

            statRepo.SetValue(StatKey.ShopOpenDate, DateTime.Now.ToString());

            produceDayRepo.DeleteAll();
            var initialStockSkins = herd.Members.Length;
            var initialStockMilk = YakProduceCalculator.TotalHerdLitersOfMilkToday(herd.Members);
            produceDayRepo.Add(new ProduceDay(0, initialStockMilk, initialStockSkins));

            // 205 - Webshop is reset to the initial state.
            return TypedResults.StatusCode(StatusCodes.Status205ResetContent);
        }
    )
    .WithName("LoadHerd")
    .WithDescription(
        "Loads a new herd into the webshop. Any previous state of the webshop is reset to the initial state."
    )
    .WithOpenApi()
    .Produces(StatusCodes.Status205ResetContent);

// POST /yak-shop/order/T
app.MapPost("/yak-shop/order/{daysAfterInit}", HandleOrder)
    .WithName("PlaceOrder")
    .WithDescription(
        "Where [daysAfterInit] or T is the day the customer orders, this means that day T has _not_ elapsed."
    )
    .WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();

void ConfigureServices(IServiceCollection services, ConfigurationManager config)
{
    services.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = true);

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddDbContext<YakShopDbContext>(options =>
        options.UseSqlServer(config.GetConnectionString("TheYakShop"))
    );

    services.AddScoped<IDbInitializer, DbInitializer>();

#if DEBUG
    services.AddDatabaseDeveloperPageExceptionFilter();
#endif

    services.AddScoped<DailyHerdStatsUpdateService>();
    services.AddScoped<StockQuantitiesCalculatorService>();

    services.AddScoped<IHerdRepository, HerdRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IProduceDayRepository, ProduceDayRepository>();
    services.AddScoped<IStatRepository, StatRepository>();

    // Register as singleton first so it can be injected through Dependency Injection
    services.AddSingleton<TimeLapseSimulationHostedService>();

    // Add as hosted service using the instance registered as singleton before
    services.AddHostedService(
        provider => provider.GetRequiredService<TimeLapseSimulationHostedService>());

}

void CreateDbIfNotExists(WebApplication app)
{
    logger.LogInformation("Making sure the database is created, including migrations.");
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<YakShopDbContext>();
        context.Database.Migrate();

        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        var produceDayRepository = scope.ServiceProvider.GetRequiredService<IProduceDayRepository>();
        dbInitializer.Initialize(context, produceDayRepository);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

IResult HandleOrder([FromRoute] int daysAfterInit,
            [FromBody] CustomerOrder customerOrder,
            [FromServices] StockQuantitiesCalculatorService stockCalc,
            [FromServices] IOrderRepository orderRepo)
{
    var order = customerOrder.Order;
    order.DayNumber = daysAfterInit;
    order.Customer = new Customer(customerOrder.CustomerName);

    // Check stock amounts for milk & skins.
    var (milk, skins) = stockCalc.CalculateForDay(daysAfterInit);

    if (order.Milk > milk && order.Skins > skins)
    {
        // 404 - The full order is not in stock.
        return TypedResults.NotFound("Unfortunately there is insufficient stock for the full order.");
    }

    if (order.Milk > milk || order.Skins > skins)
    {
        if (order.Milk > milk)
        {
            order.Milk = 0;
        }
        else if (order.Skins > skins)
        {
            order.Skins = 0;
        }

        SaveOrder(orderRepo, order);
        // 206 - Can only deliver part of total order.
        return TypedResults.StatusCode(StatusCodes.Status206PartialContent);
    }

    SaveOrder(orderRepo, order);

    // 201 - The order was placed successfully.
    return TypedResults.Created();
}

static void SaveOrder(IOrderRepository orderRepo, Order order)
{
    orderRepo.CreateOrder(order);
    orderRepo.Save();
}