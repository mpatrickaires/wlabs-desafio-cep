using Microsoft.Extensions.DependencyInjection;
using Refit;
using WLabsDesafioCEP.Application;
using WLabsDesafioCEP.Infra.Data;
using WLabsDesafioCEP.Infra.Data.Refit;

namespace WLabsDesafioCEP.Infra.IoC
{
    public static class Bootstrapper
    {
        public static IServiceCollection ConfigurarServicos(this IServiceCollection services)
        {
            services.RegistrarServicos();
            services.ConfigurarRefit();
            services.ConfigurarRedis();

            return services;
        }

        private static void RegistrarServicos(this IServiceCollection services)
        {
            services.RegistrarServicosScoped<ClasseInfraDataAssembly>("Repository", "Gateway", "Client");
            services.RegistrarServicosScoped<ClasseApplicationAssembly>("Service");
        }

        private static void RegistrarServicosScoped<TClasseAssembly>(this IServiceCollection services, 
            params string[] sufixoTipos) 
        {
            services.Scan(scan => scan
                .FromAssemblyOf<TClasseAssembly>()
                .AddClasses(classes => classes.Where(tipo =>
                    sufixoTipos.Any(nomeTipo => tipo.Name.EndsWith(nomeTipo))))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }

        private static void ConfigurarRefit(this IServiceCollection services)
        {
            services.AddRefitClient<IViaCepRefit>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br/ws"));
            services.AddRefitClient<IApiCepRefit>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://cdn.apicep.com/file/apicep"));
            services.AddRefitClient<IAwesomeApiRefit>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://cep.awesomeapi.com.br/json"));
        }

        private static IServiceCollection ConfigurarRedis(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379");

            return services;
        }

    }
}
