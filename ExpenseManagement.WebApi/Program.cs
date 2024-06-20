using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.WebApi.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.ConfigureDbContext();

builder.ConfigureIdentity();

builder.ConfigureLoggerService();

builder.ConfigureDIAutoMapper();

builder.ConfigureDIDataShaper();

builder.ConfigureDIRepsitory();

builder.ConfigureDIActionFilters();

builder.ConfigureDIModelState();

builder.ConfigureDIService();

builder.ConfigureAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
