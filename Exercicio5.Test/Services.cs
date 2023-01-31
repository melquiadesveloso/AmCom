using AutoMapper;
using Moq;
using Questao5.Application.Services;
using Questao5.Domain.Entities;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.Repositories;
using Xunit;
 
namespace Exercicio5.Test
{
    public class Services
    {

         Mock<IContaCorrenteRepository> _contaCorrenteServiceMock;

        Mock<IContaCorrenteMovimentoRepository> _contaCorrenteMovimemntoServiceMock;
        Mock<IMapper> _imapper;

        ContaCorrenteService _contaCorrenteService;
        ContaCorrenteMovimentoService _contaCorrenteMovimentoService;

        public Services()
        {
            _contaCorrenteServiceMock = new Mock<IContaCorrenteRepository>();
            _contaCorrenteMovimemntoServiceMock = new Mock<IContaCorrenteMovimentoRepository>();
            _imapper = new Mock<IMapper>();

            _contaCorrenteService = new ContaCorrenteService(_imapper.Object, _contaCorrenteServiceMock.Object);
            _contaCorrenteMovimentoService = new ContaCorrenteMovimentoService(_contaCorrenteMovimemntoServiceMock.Object);
        }

        [Fact]
        public void GetSaldo()
        {
            string idcontaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";

           var result = _contaCorrenteService.Saldo(idcontaCorrente).GetAwaiter().GetResult();

            Assert.NotNull(result);

        }

        [Fact]
        public void Movimentar()
        {
            string idcontaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";

            ContaCorrenteMovimento contaCorrenteMovimento = new ContaCorrenteMovimento()
            {
                DataMovimento = DateTime.Now,
                IdContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                TipoMovimento = "D",
                Valor = 10

            };


            var result = _contaCorrenteMovimentoService.Movimentar(contaCorrenteMovimento).GetAwaiter().GetResult();

            Assert.NotNull(result);

        }
    }
}