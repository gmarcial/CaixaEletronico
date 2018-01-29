using System;
using System.Threading.Tasks;
using CaixaEletronico.Infrastructure.Config;
using CaixaEletronico.Infrastructure.UserManager;
using SimpleInjector.Lifestyles;

namespace CaixaEletronico.Presenter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var containerRegister = new ContainerRegister();
            var container = containerRegister.Register();

            User user;
            
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var manager = container.GetInstance<Manager>();
                user = await manager.LogInAsync("", "", "");
            }

            if(user != null) Console.WriteLine("Fala garoto");
            
            Console.WriteLine("Jubireba");
        }
    }
}
