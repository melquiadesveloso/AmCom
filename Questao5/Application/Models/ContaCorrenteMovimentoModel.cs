namespace Questao5.Application.Models
{
    public class ContaCorrenteMovimentoModel
    {
        public string Id { get; set; }

        public int IdContaCorrente { get; set; }

        public DateTime DataMoivmento { get; set; }

        public string TipoMovimento { get; set; }

        public double Valor { get; set; }
    }
}
