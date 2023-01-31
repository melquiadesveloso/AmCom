using Questao5.Domain.Entities;

namespace Questao5.Domain.Services
{
    public interface IContaCorrenteMovimentoService
    {
        Task<string> Movimentar(ContaCorrenteMovimento ccMovimento);

    }
}
