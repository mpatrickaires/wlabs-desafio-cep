using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace WLabsDesafioCEP.Infra.IoC.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigurarSerilog(this WebApplicationBuilder builder)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.Host.UseSerilog((ctx, cfg) => cfg
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Environment", environment)
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"wlabs-desafio-cep-{environment}-{DateTime.UtcNow:yyyy-MM}",
            })
            .ReadFrom.Configuration(builder.Configuration));
        }
    }
}
