using log4net;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseEnrichLogging(this IApplicationBuilder builder)
    {
        return builder.Use(async (context, next) =>
        {
            LogicalThreadContext.Properties["CorrelationId"] = context.TraceIdentifier;
            await next();
        });
    }
}
