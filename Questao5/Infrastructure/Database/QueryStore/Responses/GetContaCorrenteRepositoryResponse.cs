namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class GetContaCorrenteRepositoryResponse
    {
        public string IdContaCorrente { get; set; }

        public string Nome { get; set; }

        public DateTime Data { get; set; }

        public double Saldo { get; set; }
    }
}
