using Dapper;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Questao5.Application.Queries.Requests; 
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Validation
{
    public class GetContaCorrenteValidation : AbstractValidator<GetSaldoContaCorrenteQueryRequest>
    {
        private readonly DatabaseConfig _databaseConfig;

        public GetContaCorrenteValidation(DatabaseConfig databaseConfig)
        {
            this.CascadeMode = CascadeMode.Stop;

            _databaseConfig = databaseConfig;

            using var conn = new SqliteConnection(_databaseConfig.Name);

            RuleFor(a => a.IdContaCorrente)
            .Must(b =>
            {
                try
                {
                    var sql = "SELECT 1 FROM contacorrente where idcontacorrente = @idcontacorrente";

                    var result = conn.ExecuteScalar<int>(sql, new { idcontacorrente = b });

                    return result.Equals(1);
                }
                catch
                {
                    return false;
                }

            })
            .OverridePropertyName("INVALID_ACCOUNT")
            .WithMessage("Apenas contas correntes cadastradas podem consultar o saldo;")
            .WithErrorCode("1");


            RuleFor(a => a.IdContaCorrente)
             .Must(b =>
             {
                 try
                 {
                     var sql = "SELECT 1 FROM contacorrente where idcontacorrente = @idcontacorrente and Ativo = 1";

                     var result = conn.ExecuteScalar<int>(sql, new { idcontacorrente = b });

                     return result.Equals(1);
                 }
                 catch
                 {
                     return false;
                 }

             })
             .OverridePropertyName("INACTIVE_ACCOUNT")
             .WithMessage("Apenas contas correntes ativas podem consultar o saldo;")
             .WithErrorCode("2");

        }
            
    }
}
