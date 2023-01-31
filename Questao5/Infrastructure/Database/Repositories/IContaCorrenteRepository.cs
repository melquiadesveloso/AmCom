using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<GetContaCorrenteRepositoryResponse> Saldo(string idContaCorrente);
    }
}
