using System.Data;
using System.Security.Authentication;
using System.Threading.Tasks;
using Dapper;

namespace CaixaEletronico.Infrastructure.UserManager
{
    public class Manager
    {
        private readonly IDbConnection _connection;

        public Manager(IDbConnection connection) => _connection = connection;

        public async ValueTask<User> LogInAsync(string numeroAgencia, string numeroConta, string senha)
        {
            const string query = "select Senha from Conta where Numero = @numero";

            var hash = await _connection.QuerySingleAsync<string>(query, numeroConta);

            if (Authenticate(senha, hash)) return new User(numeroAgencia, numeroConta);
            
            throw new AuthenticationException("Conta não encontrada.");
        }

        private bool Authenticate(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }

        public void LogOff(User user)
        {
            if (user.Equals(null)) return;
            
            user = null;
        }
    }
}