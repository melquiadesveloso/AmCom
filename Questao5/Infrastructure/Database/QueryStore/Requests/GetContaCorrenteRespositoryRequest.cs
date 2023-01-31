using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Questao5.Infrastructure.Database.QueryStore.Requests

{ 
    public class GetContaCorrenteRespositoryRequest : IContaCorrenteRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public GetContaCorrenteRespositoryRequest(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<GetContaCorrenteRepositoryResponse> Saldo(string idContaCorrente)
        {
            try
            {
                using var conn = new SqliteConnection(_databaseConfig.Name);
                string sql = @"
 
                     SELECT 
                          (
                             (SELECT  
                                  SUM(m.Valor)  
                                FROM MOVIMENTO m 
                                 where m.idcontacorrente = c.idcontacorrente
                                and m.tipomovimento = 'C' 
                             )  
                            -
                             (SELECT  
                                      SUM(m.Valor) 
                                    FROM MOVIMENTO m 
                                     where m.idcontacorrente = c.idcontacorrente
                                    and m.tipomovimento = 'D'
                             ) 
                          ) AS Saldo,
                         c.Nome
                 FROM ContaCorrente c
                 where c.idcontacorrente = @idcontacorrente";

                var result = await conn.QuerySingleAsync<GetContaCorrenteRepositoryResponse>(sql, new { idcontacorrente = idContaCorrente });

                if (result != null)
                {
                    result.Data = DateTime.Now;
                    result.IdContaCorrente = idContaCorrente;
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
