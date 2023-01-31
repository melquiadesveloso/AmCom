namespace Questao5.Domain.Entities
{
    public class ContaCorrenteMovimento
    {
        public string Id { get; set; }

        public string IdContaCorrente { get; set; }

        public DateTime DataMovimento { get; set; }

        public string TipoMovimento { get; set; }

        public double Valor { get; set; }
    }
}
