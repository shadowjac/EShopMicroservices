
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(assembly);
    opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
    opt.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.UseExceptionHandler(opt => { });

await app.RunAsync();
