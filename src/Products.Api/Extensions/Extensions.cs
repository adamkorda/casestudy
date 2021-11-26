
using Hellang.Middleware.ProblemDetails;

using log4net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

using Products.Api.Extensions;
using Products.Api.Swagger.Configuration;
using Products.Api.Swagger.Examples;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Products.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
        }

        public static IServiceCollection AddProblemDetails(this IServiceCollection services, bool includeExceptionDetails)
        {
            return services.AddProblemDetails(options =>
            {
                options.GetTraceId = context => context.TraceIdentifier;
                options.ShouldLogUnhandledException = (context, ex, details) => true;
                options.IncludeExceptionDetails = (context, ex) => includeExceptionDetails;
            });
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                //options.ApiVersionReader = ApiVersionReader.Combine(
                //    new QueryStringApiVersionReader("api-version"),
                //    new HeaderApiVersionReader("X-api-version"));
            });

            return services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();
            services.AddSwaggerGenNewtonsoftSupport();
            return services.AddSwaggerExamplesFromAssemblyOf<PatchProductDescriptionExample>();
        }

        public static IApplicationBuilder UseEnrichLogging(this IApplicationBuilder builder)
        {
            return builder.Use(async (context, next) =>
            {
                LogicalThreadContext.Properties["CorrelationId"] = context.TraceIdentifier;
                await next();
            });
        }

        public static IApplicationBuilder UseSwaggerWithVersions(this WebApplication app)
        {
            app.UseSwagger();
            return app.UseSwaggerUI(
                options =>
                {
                    var apiVersioningProvider = app.Services.GetService<IApiVersionDescriptionProvider>();
                    if (apiVersioningProvider is not null)
                    {
                        foreach (var description in apiVersioningProvider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        }
                    }
                });
        }
    }
}