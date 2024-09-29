using System.Net;
using Microsoft.AspNetCore.Mvc;
using YakShop.Server.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = true);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
            return Results.Ok();
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
            return Results.Ok();
        }
    )
    .WithName("GetStockInfo")
    .WithDescription("Returns a view of your stock after T days.")
    .WithOpenApi();

// POST /yak-shop/load
app.MapPost(
        "/yak-shop/load",
        ([FromBody] Herd herd) =>
        {
            // 205 - Webshop is reset to the initial state.
            return Results.StatusCode((int)HttpStatusCode.ResetContent);
        }
    )
    .WithName("LoadHerd")
    .WithDescription("Returns a view of your stock after T days.")
    .WithOpenApi()
    .Produces(StatusCodes.Status205ResetContent);

// POST /yak-shop/order/T
app.MapPost(
        "/yak-shop/order/{daysAfterInit}",
        ([FromRoute] int daysAfterInit) =>
        {
            // 201 - The order was placed successfully.
            //return Results.Created(order);

            // 206 - Can only deliver part of total order.
            //return Results.StatusCode(StatusCodes.Status206PartialContent);

            // 404 - The full order is not in stock.
            return Results.NotFound("This endpoint is not yet implemented.");
        }
    )
    .WithName("PlaceOrder")
    .WithDescription("Returns a view of your stock after T days.")
    .WithOpenApi()
    .Produces(StatusCodes.Status201Created, typeof(Order))
    .Produces(StatusCodes.Status206PartialContent)
    .Produces(StatusCodes.Status404NotFound);

app.MapFallbackToFile("/index.html");

app.Run();
