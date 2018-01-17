using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using CaixaEletronico.Core.Domain.Repositories;
using CaixaEletronico.Infrastructure.Data.Repositories;
using SimpleInjector;

namespace CaixaEletronico.Infrastructure.Config
{
    public class ContainerConfig
    {
        public void testa()
        {
            var container = new Container();

            container.Register<IDbConnection>(() =>
                new SqlConnection(Path.Combine(Directory.GetCurrentDirectory(), "../../CaixaEletronico.Infrastructure.Data/CaixaEletronico")));
            container.Register<IContaRepository, ContaRepository>(Lifestyle.Singleton);

            Directory.GetCurrentDirectory();
            
            container.Verify();
        }
    }
}