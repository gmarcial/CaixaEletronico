using System;
using CaixaEletronico.Core.Helpers;

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

            if (saldo < 0)
                throw new ArgumentException("O saldo esta com valor negativo");

            Id = id;
            AgenciaId = agenciaId;
            PessoaId = pessoaId;
            Numero = numero;
            Saldo = saldo;
        }
        
        /// <summary>
        /// Deposita o valor na conta, atribuindo o valor do saldo.
        /// </summary>
        /// <param name="valor">Valor a ser depositado</param>
        public void Depositar(decimal valor)
        {
            GarantirQuePodeDepositar(valor);

            Saldo += valor;
        }

        /// <summary>
        /// Saca o valor da conta, subtraindo o valor ao saldo.
        /// </summary>
        /// <param name="valor">Valor a ser sacado</param>
        public void Sacar(decimal valor)
        {
            GarantirQuePodeSacar(valor);

            Saldo -= valor;
        }

        /// <summary>
        /// Transfere o valor da para para outra conta, subtrai o valor do saldo
        /// e atribui ao saldo de outra conta.
        /// </summary>
        /// <param name="valor">Valor a ser transferido</param>
        /// <param name="favorecido">Conta favorecida, para qual o valor foi transferido</param>
        public void Transferir(decimal valor, Conta favorecido)
        {
            GarantirQuePodeTransferir(valor);

            Saldo -= valor;

            favorecido.Receber(valor);
        }

        /// <summary>
        /// Recebe o valor transferido, atribui o valor ao saldo. 
        /// </summary>
        /// <param name="valor">Valor recebido por transferencia</param>
        private void Receber(decimal valor)
        {
            GarantirQuePodeReceber(valor);

            Saldo += valor;
        }

        /// <summary>
        /// Garante que o valor determinado pode ser depositado
        /// </summary>
        /// <param name="valor">Valor avaliado para deposito</param>
        /// <exception cref="ArgumentOutOfRangeException">Exceção lançada quando o valor não seja valido para deposito</exception>
        private void GarantirQuePodeDepositar(decimal valor)
        {
            Validando.ZeroOuNegativo(valor, nameof(valor));

            if (valor > 5000)
                throw new ArgumentOutOfRangeException("O valor a ser depositado excedeu o limite de 5000 reais",
                    nameof(valor));
        }

        /// <summary>
        /// Garante que o valor determinado pode ser sacado
        /// </summary>
        /// <param name="valor">Valor avaliado para saque</param>
        /// <exception cref="ArgumentOutOfRangeException">Exceção lançada quando o valor não seja valido para saque</exception>
        private void GarantirQuePodeSacar(decimal valor)
        {
            Validando.ZeroOuNegativo(valor, nameof(valor));

            if (valor > 1500)
                throw new ArgumentOutOfRangeException("O valor a ser sacado excedeu o limite de 1500 reais",
                    nameof(valor));

            if (Saldo < valor)
                throw new ArgumentOutOfRangeException("Saldo insuficiente para o saque desejado", nameof(valor));
        }

        /// <summary>
        /// Garante que o valor determinado pode ser transferido
        /// </summary>
        /// <param name="valor">Valor avaliado para transferencia</param>
        /// <exception cref="ArgumentOutOfRangeException">Exceção lançada quando o valor não seja valido para transferencia</exception>
        private void GarantirQuePodeTransferir(decimal valor)
        {
            Validando.ZeroOuNegativo(valor, nameof(valor));

            if (valor > 10000m)
                throw new ArgumentOutOfRangeException("O valor a ser transferido excedeu o limite de 10000 reais",
                    nameof(valor));

            if (Saldo < valor)
                throw new ArgumentOutOfRangeException("Saldo insuficiente para a transferencia desejada",
                    nameof(valor));
        }

        /// <summary>
        /// Garante que o valor determinado pode ser recebido
        /// </summary>
        /// <param name="valor">Valor avaliado para recebimento</param>
        /// <exception cref="ArgumentOutOfRangeException">Exceção lançada quando o valor não seja valido para recebimento</exception>
        private void GarantirQuePodeReceber(decimal valor)
        {
            Validando.ZeroOuNegativo(valor, nameof(valor));

            if (valor > 10000m)
                throw new ArgumentOutOfRangeException("O valor a ser recebido excedeu o limite de 10000 reais",
                    nameof(valor));
        }
    }
}