using Questao5.Application.Services;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.Repositories;

namespace Questao5.Business.Configuration
{
    public static class RegisterTypes 
    {
        public static IServiceCollection AddRegister(this IServiceCollection services, IConfiguration config)
        {
            //Services
            services.AddScoped<IContaCorrenteMovimentoService, ContaCorrenteMovimentoService>();
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();

            //Infra
            services.AddScoped<IContaCorrenteMovimentoRepository, ContaCorrenteMovimentoRespositoryRequest>();
            services.AddScoped<IContaCorrenteRepository, GetContaCorrenteRespositoryRequest>();


            return services;

        }

    }
}
