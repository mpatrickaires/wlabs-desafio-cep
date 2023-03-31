using Microsoft.Extensions.DependencyInjection;
using Refit;
using WLabsDesafioCEP.Application.Interfaces;
using WLabsDesafioCEP.Application.Services;
using WLabsDesafioCEP.Domain.Interfaces;
using WLabsDesafioCEP.Infra.Data.Clients;
using WLabsDesafioCEP.Infra.Data.Gateways;
using WLabsDesafioCEP.Infra.Data.Interfaces;
using WLabsDesafioCEP.Infra.Data.Repositories;

namespace WLabsDesafioCEP.Infra.IoC
{
    public static class Bootstrapper
    {
        public static IServiceCollection ConfigurarServicos(this IServiceCollection services)
        {
            RegistrarServicos(services);
            ConfigurarRedis(services);

            return services;   
        }

        private static IServiceCollection RegistrarServicos(IServiceCollection services)
        {
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<ICepGateway, CepGateway>();
            services.AddScoped<ICacheGateway, CacheGateway>();

            services.AddRefitClient<IViaCepClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br/ws"));
            services.AddRefitClient<IApiCepClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://cdn.apicep.com/file/apicep"));
            services.AddRefitClient<IAwesomeApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://cep.awesomeapi.com.br/json"));

            return services;
        }

        private static IServiceCollection ConfigurarRedis(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379");

            return services;
        }

    }
}
