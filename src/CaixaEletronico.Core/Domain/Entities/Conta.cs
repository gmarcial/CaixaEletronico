using System;
using CaixaEletronico.Infrastructure.Validacoes;

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
            Validando.ZeroOuNegativo(id, nameof(id));
            Validando.ZeroOuNegativo(agenciaId, nameof(agenciaId));
            Validando.ZeroOuNegativo(pessoaId, nameof(pessoaId));
            Validando.ZeroOuNegativo(pessoaId, nameof(pessoaId));
            
            if (numero.Equals(Guid.Empty))
                throw new ArgumentNullException("O numero da conta não foi gerado em sua criação", nameof(numero));
            
            if(saldo < 0)
                throw new ArgumentException("O saldo esta com valor negativo");
            
            Id = id;
            AgenciaId = agenciaId;
            PessoaId = pessoaId;
            Numero = numero;
            Saldo = saldo;
        }

        public void Depositar(decimal valor)
        {
            GarantirQuePodeDepositar(valor);
            
            Saldo += valor;
        }

        public void Sacar(decimal valor)
        {
            GarantirQuePodeSacar(valor);

            Saldo -= valor;
        }

        public decimal Transferir(decimal valor)
        {
            GarantirQuePodeTransferir(valor);
            
            Saldo -= valor;

            return valor;
        }

        public void Receber(decimal valor)
        {
            GarantirQuePodeReceber(valor);

            Saldo += valor;
        }
        
        private void GarantirQuePodeDepositar(decimal valor)
        {
            Validando.ZeroOuNegativo(valor, nameof(valor));
            
            if (valor > 5000)
                throw new ArgumentOutOfRangeException("O valor a ser depositado excedeu o limite de 5000 reais", nameof(valor));
        }
        
        private void GarantirQuePodeSacar(decimal valor)
        {
            Validando.ZeroOuNegativo(valor, nameof(valor));
            
            if(valor > 1500)
                throw new ArgumentOutOfRangeException("O valor a ser sacado excedeu o limite de 1500 reais", nameof(valor));
            
            if (Saldo < valor)
                throw new ArgumentOutOfRangeException("Saldo insuficiente para o saque desejado", nameof(valor));
        }
        
        private void GarantirQuePodeTransferir(decimal valor)
        {
            Validando.ZeroOuNegativo(valor, nameof(valor));
            
            if (valor > 10000m)
                throw new ArgumentOutOfRangeException("O valor a ser transferido excedeu o limite de 10000 reais",
                    nameof(valor));
            
            if(Saldo < valor)
                throw new ArgumentOutOfRangeException("Saldo insuficiente para a transferencia desejada", nameof(valor));
        }
        
        private void GarantirQuePodeReceber(decimal valor)
        {
            Validando.ZeroOuNegativo(valor, nameof(valor));
            
            if (valor > 10000m)
                throw new ArgumentOutOfRangeException("O valor a ser recebido excedeu o limite de 10000 reais",
                    nameof(valor));
        }
    }
}