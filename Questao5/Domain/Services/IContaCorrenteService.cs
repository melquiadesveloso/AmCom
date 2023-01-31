using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Domain.Services
{
    public interface IContaCorrenteService
    {
        Task<GetSaldoContaCorrenteQueryResponse> Saldo(string idContaCorrente);
    }
}
