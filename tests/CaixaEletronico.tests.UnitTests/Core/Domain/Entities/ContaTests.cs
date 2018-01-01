using System;
using System.Runtime.InteropServices;
using CaixaEletronico.Core.Domain.Entities;
using Xunit;

namespace CaixaEletronico.tests.UnitTests.Core.Domain.Entities
{
    public class ContaTests
    {
        private Conta conta{ get; }
        
        public ContaTests()
        {
            conta = new Conta(83712, 9134, 1301, Guid.NewGuid());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(2500)]
        [InlineData(3000)]
        [InlineData(3500)]
        [InlineData(4000)]
        [InlineData(4500)]
        [InlineData(5000)]
        public void Um_deposito_é_valido_quando_o_valor_for_abaixo_ou_igual_a_5000(decimal valor)
        {
            conta.Depositar(valor);

            Assert.Equal(valor, conta.Saldo);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(5001)]
        public void Um_deposito_é_invalido_quando_o_valor_for_maior_que_5000_ou_igual_a_zero(decimal valor)
        {
            Assert.Throws<ArgumentException>(() => conta.Depositar(valor));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(1500)]
        public void Um_saque_é_valido_quando_o_saldo_for_maior_ou_igual_ao_valor_desejado(decimal valor)
        {
            conta.Depositar(5000);

            var saldoEsperado = conta.Saldo - valor;
            
            conta.Sacar(valor);

            Assert.Equal(saldoEsperado, conta.Saldo);
        }

        [Fact]
        public void Um_saque_é_invalido_quando_o_saldo_for_menor_que_o_valor_desejado()
        {
            Assert.Throws<ArgumentException>(() => conta.Sacar(100));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(1500)]
        public void Um_saque_é_valido_quando_o_valor_a_ser_sacado_for_menor_ou_igual_a_1500(
            decimal valor)
        {
            conta.Depositar(5000);

            var saldoEsperado = conta.Saldo - valor;
            
            conta.Sacar(valor);

            Assert.Equal(saldoEsperado, conta.Saldo);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(2000)]
        [InlineData(2500)]
        [InlineData(3000)]
        [InlineData(3500)]
        [InlineData(4000)]
        [InlineData(4500)]
        [InlineData(5000)]
        public void Um_saque_é_invalido_quando_o_valor_a_ser_sacado_for_maior_que_1500_ou_igual_a_zero(decimal valor)
        {
            Assert.Throws<ArgumentException>(() => conta.Sacar(valor));
        }
    }
}