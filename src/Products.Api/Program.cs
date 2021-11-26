
using FluentValidation.AspNetCore;

using Hellang.Middleware.ProblemDetails;

using Microsoft.EntityFrameworkCore;

using Products.Api.Data;
using Products.Api.Data.Extensions;
using Products.Api.Data.Seeder;
using Products.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>())
.AddNewtonsoftJson();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddVersioning();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddProblemDetails(builder.Environment.IsDevelopment() || builder.Environment.IsStaging());

builder.Services.AddCorsPolicy();

builder.Services.AddHealthChecks();

builder.Services.AddScoped<Products.Api.Data.Repositories.V1.IProductRepository, Products.Api.Data.Repositories.V1.ProductRepository>();
builder.Services.AddScoped<Products.Api.Data.Repositories.V2.IProductRepository, Products.Api.Data.Repositories.V2.ProductRepository>();
builder.Services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

var app = builder.Build();

app.UseProblemDetails();
app.UseEnrichLogging();
app.SeedDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithVersions();
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
