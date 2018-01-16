using System.Data;
using Dapper;

namespace CaixaEletronico.Infrastructure.Log
{
    public class Logger
    {
        private readonly IDbConnection _connection;

        public Logger(IDbConnection connection) => _connection = connection;

        public async void TransacaoAsync(Extrato extrato)
        {
            const string insert =
                "insert into Extrato(" +
                "ContaId, " +
                " NumeroConta, " +
                " NumeroOperacao, " +
                " Operacao, " +
                " Date, " +
                " Valor) " +
                "values(" +
                "@ContaId, " +
                " @NumeroConta, " +
                " @NumeroOperacao, " +
                " @Operacao, " +
                " @Date, " +
                " @Valor);";
            
             await _connection.ExecuteAsync(insert, extrato);
        }
    }
}