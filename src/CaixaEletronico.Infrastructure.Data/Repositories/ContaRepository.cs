using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CaixaEletronico.Core.Domain.Entities;
using CaixaEletronico.Core.Domain.Repositories;
using Dapper;

namespace CaixaEletronico.Infrastructure.Data.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly IDbConnection _connection;

        public ContaRepository(IDbConnection connection) => _connection = connection;
        
        public async ValueTask<Conta> ObterContaPeloNumeroAsync(Guid numero)
        {
            const string query = "select * from Conta where Numero = @numero;";

            return await _connection.QuerySingleAsync<Conta>(query, numero);
        }

        public async ValueTask<decimal> ObterSaldoAsync(Guid numero)
        {
            const string query = "select Saldo from Conta where Numero = @numero";
            
            return await _connection.QuerySingleAsync<decimal>(query, numero);
        }

        public async ValueTask<IEnumerable<Extrato>> ObterExtrato(Guid numero)
        {
            const string query = "select * from Extrato where Numero = @numero";

            return await _connection.QueryAsync<Extrato>(query, numero);
        }
    }
}