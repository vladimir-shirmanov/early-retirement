using EarlyRetirement.Api.UserManagement;
using EarlyRetirement.Application.Utils;
using EarlyRetirement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRetirementDb(builder.Configuration);
builder.Services.AddEarlyRetirementLogic(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication().AddJwtBearer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("users/")
    .MapAuthApi()
    .WithOpenApi();

app.Run();

