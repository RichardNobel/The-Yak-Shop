using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data;
using YakShop.Server.Data.Repositories;
using YakShop.Server.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);
var app = builder.Build();

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
            return TypedResults.Ok();
        }
    )
    .WithName("GetStockInfo")
    .WithDescription("Returns a view of your stock after T days.")
    .WithOpenApi();

// POST /yak-shop/load
app.MapPost(
        "/yak-shop/load",
        ([FromBody] Herd herd, [FromServices] IHerdRepository herdRepo) =>
        {
            herdRepo.CreateHerd(herd);
            int numberOfRecordsCreated = herdRepo.Save();
            Debug.WriteLine($"{numberOfRecordsCreated} yaks were added to the new herd.");

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
app.MapPost(
        "/yak-shop/order/{daysAfterInit}",
        (
            [FromRoute] int daysAfterInit,
            [FromBody] CustomerOrder customerOrder,
            [FromServices] IOrderRepository orderRepo
        ) =>
        {
            // TODO: Check stock amounts for milk & skins.
            customerOrder.Order.Customer = new Customer(customerOrder.CustomerName);
            orderRepo.CreateOrder(customerOrder.Order);
            orderRepo.Save();

            // 201 - The order was placed successfully.
            return TypedResults.Created();

            // 206 - Can only deliver part of total order.
            //return TypedResults.StatusCode(StatusCodes.Status206PartialContent);

            // 404 - The full order is not in stock.
            //return TypedResults.NotFound("Unfortunately there is insufficient stock for the full order.");
        }
    )
    .WithName("PlaceOrder")
    .WithDescription(
        "Where [daysAfterInit] or T is the day the customer orders, this means that day T has _not_ elapsed."
    )
    .WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = true);

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddDbContext<YakShopDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TheYakShop"))
    );

#if DEBUG
    services.AddDatabaseDeveloperPageExceptionFilter();
#endif

    services.AddScoped<IHerdRepository, HerdRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
}
