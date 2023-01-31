using AutoMapper;
using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Services;

namespace Questao5.Application.Handlers
{
    public class GetSaldoContaCorrenteHandler : IRequestHandler<GetSaldoContaCorrenteQueryRequest, GetSaldoContaCorrenteQueryResponse>
    {
        private readonly IMapper _mapper;

        private readonly IContaCorrenteService _contaCorrenteService;

        public GetSaldoContaCorrenteHandler(IMapper mapper, IContaCorrenteService contaCorrenteService)
        {
            _mapper = mapper;
            _contaCorrenteService = contaCorrenteService;
        }

        public Task<GetSaldoContaCorrenteQueryResponse> Handle(GetSaldoContaCorrenteQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _contaCorrenteService.Saldo(request.IdContaCorrente);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
