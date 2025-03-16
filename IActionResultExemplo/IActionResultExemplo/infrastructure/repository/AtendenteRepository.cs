using IActionResultExemplo.application.dto;
using System.Data;
using Dapper;
using IActionResultExemplo.domain.repository;
using IActionResultExemplo.domain.models;

namespace IActionResultExemplo.infrastructure.repository
{
    public class AtendenteRepository : IAtendenteRepository
    {
        private readonly IDbConnection _dbConnection;

        public AtendenteRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Atendente?> EncontraPorEmailAsync(string email)
        {
            var query = "SELECT * FROM Atendentes WHERE Email = @Email";
            return await _dbConnection.QueryFirstOrDefaultAsync<Atendente>(query, new { Email = email });
        }

        public async Task<Atendente?> EncontraPorIdAsync(long id)
        {
            var query = "SELECT * FROM Atendentes WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Atendente>(query, new { Id = id });
        }

        public async Task<Atendente> AdicionarAtendenteAsync(Atendente atendente)
        {
            var query = @"
        INSERT INTO Atendentes (Nome, Email, Senha, Telefone, DataNascimento)
        VALUES (@Nome, @Email, @Senha, @Telefone, @DataNascimento)
        RETURNING Id;";

            var id = await _dbConnection.QuerySingleAsync<long>(query, atendente);
            atendente.Id = id;
            return atendente;
        }



        public async Task SalvarAsync(Atendente atendente)
        {
            var query = @"
                UPDATE Atendentes
                SET Nome = @Nome, Email = @Email, Senha = @Senha, Telefone = @Telefone, DataNascimento = @DataNascimento
                WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, atendente);
        }
 
    }

}
