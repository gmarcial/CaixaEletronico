using System;
using CaixaEletronico.Core.Domain.Interfaces;

namespace CaixaEletronico.Core.Domain.Entities
{
    public class Conta : IConta
    {
        public long Id { get; }
        public long AgenciaId { get; }
        public long PessoaId { get; }
        public Guid Numero { get; }
        public decimal Saldo { get; private set; }

        public Conta(long id, long agenciaId, long pessoaId, Guid numero, decimal saldo = 0)
        {
            //TODO validar dados para consistencia dos objetos.
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O argumento id esta zero ou negativo.");
            
            if(agenciaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(agenciaId), "O argumento agenciaId esta zero ou negativo.");
            
            if(pessoaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(pessoaId), "O argumento pessoaId esta zero ou negativo.");
            
            if (numero.Equals(null))
                throw new ArgumentNullException("O numero da conta não foi gerado em sua criação", nameof(numero));
            
            if(saldo.Equals(null))
                throw new ArgumentException("O saldo esta com valor negativo");
            
            
            Id = id;
            AgenciaId = agenciaId;
            PessoaId = pessoaId;
            Numero = numero;
            Saldo = saldo;
        }

        public void Depositar(decimal valor)
        {
            //TODO Encapsular em um conjunto de validações basicas ou referente a essa entidade.
            if (valor <= 0)
                throw new ArgumentOutOfRangeException("O valor a ser depositado não pode ser zero ou negativo", nameof(valor));
            
            if (valor > 5000)
                throw new ArgumentOutOfRangeException("O valor a ser depositado excedeu o limite de 5000 reais", nameof(valor));

            Saldo += valor;
        }

        public void Sacar(decimal valor)
        {
            //TODO Encapsular em um conjunto de validações basicas ou referente a essa entidade.
            if(valor <= 0)
                throw new ArgumentOutOfRangeException("O valor a ser sacado não pode ser zero ou negativo", nameof(valor));
            
            if(valor > 1500)
                throw new ArgumentOutOfRangeException("O valor a ser sacado excedeu o limite de 1500 reais", nameof(valor));
            
            if (Saldo < valor)
                throw new ArgumentOutOfRangeException("A conta não tem saldo suficiente para o saque desejado", nameof(valor));

            Saldo -= valor;
        }
    }
}