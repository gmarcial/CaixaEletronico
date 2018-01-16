using System;

namespace CaixaEletronico.Infrastructure.UserManager
{
    public class User
    {
        public User(string numeroAgencia, string numeroConta)
        {
            NumeroAgencia = numeroAgencia;
            NumeroConta = numeroConta;
        }

        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
    }
}