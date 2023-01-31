using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repositories
{
    public interface IContaCorrenteMovimentoRepository
    {
        Task<string> Add(ContaCorrenteMovimento ccMovimento);

        void Update(ContaCorrenteMovimento ccMovimento);

        void Delete(ContaCorrenteMovimento ccMovimento);
         
    }
}
