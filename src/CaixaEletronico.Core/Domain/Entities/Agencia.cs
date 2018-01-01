using System;

namespace CaixaEletronico.Core.Domain.Entities
{
    public class Agencia
    {
        public Agencia(long id, string nome, Guid numero)
        {
            Id = id;
            Nome = nome;
            Numero = numero;
        }

        public long Id { get; }
        public string Nome { get; }
        public Guid Numero { get; }
    }
}