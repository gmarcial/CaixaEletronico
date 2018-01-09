using System;
using CaixaEletronico.Core.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace CaixaEletronico.tests.UnitTests.Core.Domain.Entities
{
    public class ContaTests
    {
        private Conta conta{ get; }
        
        public ContaTests()
        {
            //Mapear para um builder
            conta = new Conta(83712, 9134, 1301, Guid.NewGuid());
        }

        [Fact]
        public void Uma_conta_deve_ser_criada_somente_com_um_estado_valido()
        {
            //Arrange
            var numero = Guid.NewGuid();
            
            //Act
            var conta = new Conta(39491, 230103, 0231, numero, 1000);

            //Assert
            Assert.Equal(typeof(Conta), conta.GetType());
            Assert.IsType<Conta>(conta);
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
        public void Um_deposito_é_valido_quando_o_valor_estiver_entre_1_e_5000(decimal valor)
        {
            //Arrange
            //Futuro builder de conta
            
            //Act
            conta.Depositar(valor);

            //Assert
            Assert.Equal(valor, conta.Saldo);
        }
        
        [Theory]
        [InlineData(5001)]
        [InlineData(6000)]
        [InlineData(7000)]
        [InlineData(8000)]
        [InlineData(9000)]
        [InlineData(10000)]
        public void Um_deposito_é_invalido_quando_o_valor_for_maior_que_5000(decimal valor)
        {
            //Arrange
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Depositar(valor));
        }
        
        [Fact]
        public void Um_deposito_é_invalido_quando_o_valor_for_igual_a_zero()
        {
            //Arrange
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Depositar(0));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        [InlineData(-500)]
        [InlineData(-1000)]
        [InlineData(-1500)]
        [InlineData(-2000)]
        [InlineData(-2500)]
        [InlineData(-3000)]
        [InlineData(-3500)]
        [InlineData(-4000)]
        [InlineData(-4500)]
        [InlineData(-5000)]
        public void Um_deposito_é_invalido_quando_o_valor_for_negativo(decimal valor)
        {
            //Arrange
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Depositar(valor));
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
            //Arrange
            //Futuro builder de conta
            conta.Depositar(5000);
            var saldoEsperado = conta.Saldo - valor;   
            
            //Act
            conta.Sacar(valor);

            //Assert
            Assert.Equal(saldoEsperado, conta.Saldo);
        }

        [Fact]
        public void Um_saque_é_invalido_quando_o_saldo_for_menor_que_o_valor_desejado()
        {
            //Arrange
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Sacar(100));
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
            //Arrange
            conta.Depositar(5000);
            var saldoEsperado = conta.Saldo - valor;
            
            //Act
            conta.Sacar(valor);

            //Assert
            Assert.Equal(saldoEsperado, conta.Saldo);
        }
        
        [Theory]
        [InlineData(2000)]
        [InlineData(2500)]
        [InlineData(3000)]
        [InlineData(3500)]
        [InlineData(4000)]
        [InlineData(4500)]
        [InlineData(5000)]
        public void Um_saque_é_invalido_quando_o_valor_a_ser_sacado_for_maior_que_1500(decimal valor)
        {
            //Arrange
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Sacar(valor));
        }
        
        [Fact]
        public void Um_saque_é_invalido_quando_o_valor_a_ser_sacado_for_igual_a_zero()
        {
            //Arrange
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Sacar(0));
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        [InlineData(-500)]
        [InlineData(-1000)]
        [InlineData(-1500)]
        [InlineData(-2000)]
        [InlineData(-2500)]
        [InlineData(-3000)]
        [InlineData(-3500)]
        [InlineData(-4000)]
        [InlineData(-4500)]
        [InlineData(-5000)]
        public void Um_saque_é_invalido_quando_o_valor_for_negativo(decimal valor)
        {
            //Arrange
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Sacar(valor));
        }

        [Fact]
        public void Um_deposito_é_valido_quando_o_valor_for_menor_ou_igual_a_5000()
        {
            throw new NotImplementedException();
        }
    }
}