using System;

namespace CaixaEletronico.Core.Domain.Entities
{
    public class Conta
    {
        public long Id { get; }
        public long AgenciaId { get; }
        public long PessoaId { get; }
        public Guid Numero { get; }
        public decimal Saldo { get; private set; }

        public Conta(long id, long agenciaId, long pessoaId, Guid numero, decimal saldo = 0)
        {
            if (numero.Equals(null))
                throw new ArgumentException("O numero da conta não foi gerado em sua criação", nameof(numero));
            
            Id = id;
            AgenciaId = agenciaId;
            PessoaId = pessoaId;
            Numero = numero;
            Saldo = saldo;
        }

        public void Depositar(decimal valor)
        {
            if (valor == 0)
                throw new ArgumentException("O valor a ser depositado não pode ser zero", nameof(valor));
            
            if (valor > 5000)
                throw new ArgumentException("O valor a ser depositado excedeu o limite de 5000 reais", nameof(valor));

            Saldo =+ valor;
        }
    }
}