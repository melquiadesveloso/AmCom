using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;

namespace Questao5.Application.Queries.Responses
{
    public class GetSaldoContaCorrenteQueryResponse
    {
  
        public string IdContaCorrente { get; set; }

        public string Nome { get; set; }

        public DateTime Data { get; set; }
        
        private double _saldo;

        public double Saldo
        {
            get
            {
                return Math.Round((Double)_saldo, 2);
            }
            set
            {
                _saldo = value;
            }
        }
    }
}
