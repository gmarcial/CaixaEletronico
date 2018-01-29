using System.Data;
using System.Data.SQLite;
using CaixaEletronico.Core.Domain.Repositories;
using CaixaEletronico.Infrastructure.Data.Repositories;
using CaixaEletronico.Infrastructure.Logg;
using CaixaEletronico.Infrastructure.UserManager;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CaixaEletronico.Infrastructure.Config
{
    public class ContainerRegister
    {
        public Container Register()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<IDbConnection>(() =>
                    new SQLiteConnection(
                        "Data Source=/home/gmarcial/Documentos/Projetos/CaixaEletronico/src/CaixaEletronico.Infrastructure.Data/CaixaEletronico.db"), 
                Lifestyle.Scoped);
            container.Register<Manager>(Lifestyle.Scoped);
            container.Register<IContaRepository, ContaRepository>(Lifestyle.Scoped);
            container.Register<Logger>(Lifestyle.Scoped);
            container.Verify();

            return container;
        }
    }
}