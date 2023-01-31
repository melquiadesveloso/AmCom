using AutoMapper;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Services;

namespace Questao5.Application.Handlers
{
    public class CreateContaCorrenteMovimentoHandler : IRequestHandler<CreateContaCorrenteMovimentoRequest, CreateContaCorrenteMovimentoResponse>
    {
        private readonly IMapper _mapper;

        private readonly IContaCorrenteMovimentoService _contaCorrenteMovimentoService;

        public CreateContaCorrenteMovimentoHandler(IMapper mapper, IContaCorrenteMovimentoService contaCorrenteMovimentoService)
        {
            _mapper = mapper;
            _contaCorrenteMovimentoService = contaCorrenteMovimentoService;
        }

        public async Task<CreateContaCorrenteMovimentoResponse> Handle(CreateContaCorrenteMovimentoRequest request, CancellationToken cancellationToken)
        {
              
            var ccMov = this._mapper.Map<Domain.Entities.ContaCorrenteMovimento>(request);

            var idMovimento = await _contaCorrenteMovimentoService.Movimentar(ccMov);
             

            var result = new CreateContaCorrenteMovimentoResponse()
            {
                Id = idMovimento,
                Descricao = "Registro cadastrado com sucesso."

            };

            return  result;
        }
    }
}

