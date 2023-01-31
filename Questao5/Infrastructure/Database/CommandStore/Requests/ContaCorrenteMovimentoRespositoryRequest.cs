using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using NSubstitute.Exceptions;
using System.Transactions;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class ContaCorrenteMovimentoRespositoryRequest : IContaCorrenteMovimentoRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContaCorrenteMovimentoRespositoryRequest(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<string> Add(ContaCorrenteMovimento ccMovimento)
        {
            try
            {
                var idmovimento = Guid.NewGuid().ToString();
                var sql = "INSERT INTO movimento VALUES (@idmovimento, @idcontacorrente,@datamovimento, @tipomovimento, @valor);SELECT last_insert_rowid();";

                using var conn = new SqliteConnection(_databaseConfig.Name);
                var rowID = await conn.QueryAsync<int>(sql, new { idmovimento = idmovimento, idcontacorrente = ccMovimento.IdContaCorrente, datamovimento = DateTime.Now.ToShortDateString(), tipomovimento = ccMovimento.TipoMovimento, valor = ccMovimento.Valor});
               
                return  rowID.Count() > 0 ? idmovimento : string.Empty;
               
            }
            catch (Exception)
            {
                return String.Empty;
            } 
        }

        public void Delete(ContaCorrenteMovimento ccMovimento)
        {
            throw new NotImplementedException();
        }

        public void Update(ContaCorrenteMovimento ccMovimento)
        {
            throw new NotImplementedException();
        }
    }
}
