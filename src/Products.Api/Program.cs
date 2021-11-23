
using FluentValidation.AspNetCore;

using Hellang.Middleware.ProblemDetails;

using Microsoft.EntityFrameworkCore;

using Products.Api.Data;
using Products.Api.Data.Repositories;
using Products.Api.Data.Repositories.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

builder.Services.AddControllers()
    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>())
    .AddNewtonsoftJson();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddApiVersioning();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddProblemDetails(builder.Environment.IsDevelopment() || builder.Environment.IsStaging());

builder.Services.AddCorsPolicy();

builder.Services.AddHealthChecks();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseProblemDetails();
app.UseEnrichLogging();
app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithVersions();
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
