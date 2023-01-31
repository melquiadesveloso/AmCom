 
using Questao5.Domain.Entities;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.Repositories;
using System.Linq.Expressions;

namespace Questao5.Application.Services
{
    public class ContaCorrenteMovimentoService : IContaCorrenteMovimentoService
    { 
        private readonly IContaCorrenteMovimentoRepository _contaCorrenteMovimentoRepository ;
         
        public ContaCorrenteMovimentoService(IContaCorrenteMovimentoRepository contaCorrenteMovimentoRepository)
        {
            _contaCorrenteMovimentoRepository = contaCorrenteMovimentoRepository;
        }

        public async Task<string> Movimentar(ContaCorrenteMovimento model)
        {
            try
            {
                return await _contaCorrenteMovimentoRepository.Add(model);

            }
            catch
            { 
                return string.Empty;
            }

        }

    }
}
