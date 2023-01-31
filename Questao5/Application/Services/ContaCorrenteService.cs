using AutoMapper;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.Repositories;

namespace Questao5.Application.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMapper _mapper;

        public ContaCorrenteService(IMapper mapper, IContaCorrenteRepository contaCorrenteRepository)
        {
            _mapper = mapper;
            _contaCorrenteRepository = contaCorrenteRepository;
        }
        public async Task<GetSaldoContaCorrenteQueryResponse> Saldo(string idContaCorrente)
        {
            var resultSaldo = await _contaCorrenteRepository.Saldo(idContaCorrente);

            var response = _mapper.Map<GetSaldoContaCorrenteQueryResponse>(resultSaldo);
             
            return response;
        }
    }
}
