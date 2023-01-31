using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Questao1
{
    public class ContaBancaria : IContaBancaria
    {
        private const double TAXA = 3.50;

        public string Titular { get; set; }

        public readonly int Numero;

        public double Saldo { get; set; }

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            Titular = titular;
            Numero = numero;
            Deposito(depositoInicial);
          
        }
        public ContaBancaria(int numero, string titular)
        {
            Titular = titular;
            Numero = numero;
            Saldo = 0;
        }

        public void Deposito(double valor)
        {
            if(valor > 0)
                Saldo += valor;
        }

        public void Saque(double valor)
        {
            if (valor > 0 && Saldo > valor)
                Saldo = (Saldo - valor) - TAXA;

        }
        public override string ToString()
        {
            return $"Conta {this.Numero}, Titular: {this.Titular}, Saldo: $ {this.Saldo.ToString("0.00", new CultureInfo("en-US"))}";
        }
    }
}
