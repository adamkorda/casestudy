using FluentValidation.AspNetCore;

using Hellang.Middleware.ProblemDetails;

using Microsoft.EntityFrameworkCore;

using Products.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

builder.Services.AddControllers()
    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddHealthChecks();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddProblemDetails(options =>
{
    options.GetTraceId = context => context.TraceIdentifier;
    options.ShouldLogUnhandledException = (context, ex, details) => true;
    options.IncludeExceptionDetails = (context, ex) => builder.Environment.IsDevelopment() || builder.Environment.IsStaging();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseProblemDetails();
app.UseEnrichLogging();
app.MigrateDatabase(app.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
