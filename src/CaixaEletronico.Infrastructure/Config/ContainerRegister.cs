using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CaixaEletronico.Core.Domain.Repositories;
using CaixaEletronico.Infrastructure.Data.Repositories;
using SimpleInjector;

namespace CaixaEletronico.Infrastructure.Config
{
    public class ContainerRegister
    {
        public void Register(Container container)
        {
            container.Register<IDbConnection>(() =>
                new SqlConnection(Path.Combine(Directory.GetCurrentDirectory(),
                    "../../CaixaEletronico.Infrastructure.Data/CaixaEletronico")), Lifestyle.Scoped);
            container.Register<IContaRepository, ContaRepository>(Lifestyle.Singleton);
            
            container.Verify();
        }
    }
}