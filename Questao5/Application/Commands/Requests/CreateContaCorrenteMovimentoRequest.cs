using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class CreateContaCorrenteMovimentoRequest : IRequest<CreateContaCorrenteMovimentoResponse>
    {

        public string IdContaCorrente { get; set; }

        public DateTime DataMovimento { get; set; }

        public string TipoMovimento { get; set; }

        public double Valor { get; set; }
    }
}
