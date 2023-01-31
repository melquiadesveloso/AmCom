using Dapper;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Validation
{
    public class CreateContaCorrenteMovimentoValidation : AbstractValidator<CreateContaCorrenteMovimentoRequest>
    {
        private readonly DatabaseConfig _databaseConfig;

        public CreateContaCorrenteMovimentoValidation(DatabaseConfig databaseConfig)
        {
            this.CascadeMode = CascadeMode.Stop;
            _databaseConfig = databaseConfig;

            using var conn = new SqliteConnection(_databaseConfig.Name);
  
            RuleFor(a => a.Valor) 
                .GreaterThan(0)
                .OverridePropertyName("INVALID_ACCOUNT")
                .WithMessage("Apenas valores positivos podem ser recebidos")
                .WithErrorCode("1");

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
                .WithMessage("Apenas contas correntes cadastradas podem receber movimentação.")
                .WithErrorCode("2");

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
                .WithMessage("Apenas contas correntes ativas podem receber movimentação.")
                .WithErrorCode("3");

            RuleFor(a => a.TipoMovimento)
                .NotEmpty()
                .NotNull()
                .Must(v => v == "C" || v == "D")
                .OverridePropertyName("INVALID_TYPE")
                .WithMessage("Apenas os tipos “débito” ou “crédito” podem ser aceitos.")
                .WithErrorCode("4");

        }
    }
}
