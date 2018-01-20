using System.Data;
using System.Data.SqlClient;
using System.IO;
using CaixaEletronico.Core.Domain.Repositories;
using CaixaEletronico.Infrastructure.Data.Repositories;
using SimpleInjector;

namespace CaixaEletronico.Infrastructure.Config
{
    public class ContainerRegister
    {
        public Container Register()
        {
            var container = new Container();
            
            container.Register<IDbConnection>(() =>
                new SqlConnection(Path.Combine(Directory.GetCurrentDirectory(),
                    "../../CaixaEletronico.Infrastructure.Data/CaixaEletronico")), Lifestyle.Scoped);
            container.Register<IContaRepository, ContaRepository>(Lifestyle.Singleton);
            
            container.Verify();

            return container;
        }
    }
}