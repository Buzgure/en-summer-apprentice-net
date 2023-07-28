using Microsoft.AspNetCore.Diagnostics;
using NLog.Web;
using System.Text.Json.Serialization;
using TMS.API.Middleware;
using TMS.API.Repository;
using TMS.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Setup NLog for dependecy injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

 

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }