namespace CaixaEletronico.Core.Domain.Entities
{
    public class Pessoa
    {
        public Pessoa(long id, string nome, string cpf)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
        }

        public long Id { get; }
        public string Nome { get; }
        public string Cpf { get; }
    }
}