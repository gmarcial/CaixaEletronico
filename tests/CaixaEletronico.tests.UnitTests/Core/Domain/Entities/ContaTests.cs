using System;
using System.Runtime.InteropServices;
using CaixaEletronico.Core.Domain.Entities;
using CaixaEletronico.tests.UnitTests.Builders;
using FluentAssertions;
using Xunit;

namespace CaixaEletronico.tests.UnitTests.Core.Domain.Entities
{
    public class ContaTests
    {
        private ContaBuilder ContaBuilder{ get; }
        
        public ContaTests()
        {
            ContaBuilder = new ContaBuilder();
        }

        [Fact]
        public void Uma_conta_deve_ser_criada_somente_com_um_estado_valido()
        {
            //Arrange
            //Act
            var conta = ContaBuilder.Build();

            //Assert
            Assert.Equal(typeof(Conta), conta.GetType());
            Assert.IsType<Conta>(conta);
        }

        [Fact]
        public void Uma_conta_não_deve_ser_criada_sem_um_numero()
        {
            //Arrange
            ContaBuilder.NumeroEmpty();
            
            //Act
            Action conta = () => ContaBuilder.Build();
            
            //Assert
            Assert.Throws<ArgumentNullException>(conta);
        }

        [Fact]
        public void Uma_conta_não_deve_ser_criada_com_saldo_negativo()
        {
            //Arrange
            const int saldoNegativo = -1;
            ContaBuilder.Saldo(saldoNegativo);
            
            //Act
            Action conta = () => ContaBuilder.Build();
            
            //Assert
            Assert.Throws<ArgumentException>(conta);

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
            var conta = ContaBuilder.Build();
            
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
            var conta = ContaBuilder.Build();
            
            //Act
            Action depositar = () => conta.Depositar(valor);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(depositar);
        }
        
        [Fact]
        public void Um_deposito_é_invalido_quando_o_valor_for_igual_a_zero()
        {
            //Arrange
            var conta = ContaBuilder.Build();
            
            //Act
            Action depositar = () => conta.Depositar(0);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(depositar);
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
            var conta = ContaBuilder.Build();
            
            //Act
            Action depositar = () => conta.Depositar(valor);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(depositar);
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
            var conta = ContaBuilder.Saldo(5000).Build();
            var saldoEsperado = conta.Saldo - valor;   
            
            //Act
            conta.Sacar(valor);

            //Assert
            Assert.Equal(saldoEsperado, conta.Saldo);
        }

        [Theory]
        [InlineData(3500)]
        [InlineData(1300)]
        [InlineData(2700)]
        public void Um_saque_é_invalido_quando_o_saldo_for_menor_que_o_valor_desejado(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Build();
            
            //Act
            Action saque = () => conta.Sacar(valor);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(saque);
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
            var conta = ContaBuilder.Saldo(5000).Build();
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
            var conta = ContaBuilder.Build();
            
            //Act
            Action saque = () => conta.Sacar(valor);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(saque);
        }
        
        [Fact]
        public void Um_saque_é_invalido_quando_o_valor_a_ser_sacado_for_igual_a_zero()
        {
            //Arrange
            var conta = ContaBuilder.Build();
            
            //Act
            Action saque = () => conta.Sacar(0);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(saque);
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
            var conta = ContaBuilder.Build();
            
            //Act
            Action saque = () => conta.Sacar(valor);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(saque);
        }

        [Theory]
        [InlineData(500)]
        [InlineData(375)]
        [InlineData(735)]
        [InlineData(1075)]
        [InlineData(2340)]
        public void Uma_transferencia_é_valida_quando_o_valor_for_maior_que_zero(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Saldo(5000).Build();
            
            //Act
            var valorTransferido = conta.Transferir(valor);
            
            //Assert
            Assert.Equal(valor, valorTransferido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Uma_transferencia_é_invalida_quando_o_valor_for_zero_ou_negativo(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Build();
            
            //Act
            Action transferencia = () => conta.Transferir(valor);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(transferencia);
        }

        [Theory]
        [InlineData(500)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(2500)]
        [InlineData(3000)]
        [InlineData(3500)]
        [InlineData(4000)]
        [InlineData(4500)]
        [InlineData(5000)]
        [InlineData(5500)]
        [InlineData(6000)]
        [InlineData(6500)]
        [InlineData(7000)]
        [InlineData(7500)]
        [InlineData(8000)]
        [InlineData(8500)]
        [InlineData(9000)]
        [InlineData(9500)]
        [InlineData(10000)]
        public void Uma_transferencia_é_valida_quando_o_valor_for_igual_ou_menor_que_o_limite_de_10000(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Saldo(10000).Build();
            
            //Act
            var valorTransferido = conta.Transferir(valor);
            
            //Assert
            Assert.Equal(valor, valorTransferido);
        }

        [Theory]
        [InlineData(75000)]
        [InlineData(80000)]
        [InlineData(85000)]
        [InlineData(90000)]
        [InlineData(95000)]
        [InlineData(100000)]
        public void Uma_transferencia_é_invalida_quando_o_valor_for_maior_que_o_limite_de_10000(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Build();
            
            //Act
            Action transferencia = () => conta.Transferir(valor);
            
            //Asser
            Assert.Throws<ArgumentOutOfRangeException>(transferencia);
        }

        [Theory]
        [InlineData(500)]
        [InlineData(375)]
        [InlineData(735)]
        [InlineData(1075)]
        [InlineData(2340)]
        public void Uma_transferencia_é_valida_quando_o_saldo_for_igual_ou_maior_ao_valor(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Saldo(5000).Build();
            
            //Act
            var valorTransferido = conta.Transferir(valor);
            
            //Assert
            Assert.Equal(valorTransferido, valor);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(570)]
        [InlineData(910)]
        [InlineData(813)]
        public void Uma_transferencia_é_valida_quando_o_saldo_é_subtraido_pelo_valor(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Saldo(5000).Build();
            var saldoEsperado = conta.Saldo - valor;
            
            //Act
            conta.Transferir(valor);

            //Assert
            Assert.Equal(saldoEsperado, conta.Saldo);
        }

        [Theory]
        [InlineData(500)]
        [InlineData(302)]
        [InlineData(705)]
        [InlineData(1000)]
        public void Um_recebimento_é_valido_quando_o_valor_for_maior_que_zero(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Build();
            var saldoEsperado = conta.Saldo + valor;
            
            //Act
            conta.Receber(valor);
            
            //Assert
            Assert.Equal(saldoEsperado, conta.Saldo);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Um_recebimento_é_invalido_quando_o_valor_for_zero_ou_negativo(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Build();
            
            //Act
            Action receber = () => conta.Receber(valor);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(receber);
        }

        [Theory]
        [InlineData(500)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(2500)]
        [InlineData(3000)]
        [InlineData(3500)]
        [InlineData(4000)]
        [InlineData(4500)]
        [InlineData(5000)]
        [InlineData(5500)]
        [InlineData(6000)]
        [InlineData(6500)]
        [InlineData(7000)]
        [InlineData(7500)]
        [InlineData(8000)]
        [InlineData(8500)]
        [InlineData(9000)]
        [InlineData(9500)]
        [InlineData(10000)]
        public void Um_recebimento_é_valido_quando_o_valor_for_menor_ou_igual_ao_limite_de_10000(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Build();
            var saldoEsperado = conta.Saldo + valor;
            
            //Act
            conta.Receber(valor);
            
            //Assert
            Assert.Equal(saldoEsperado, conta.Saldo);
        }

        [Theory]
        [InlineData(20000)]
        [InlineData(30000)]
        [InlineData(40000)]
        [InlineData(50000)]
        public void Um_recebimento_é_invalido_quando_o_valor_for_maior_que_o_limite_de_10000(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Build();
            
            //Act
            Action receber = () => conta.Receber(valor);
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(receber);
        }

        [Theory]
        [InlineData(500)]
        [InlineData(1500)]
        [InlineData(5000)]
        [InlineData(3000)]
        [InlineData(5200)]
        [InlineData(10000)]
        public void Um_recebimento_é_valido_quando_o_valor_for_atribuido_ao_saldo(decimal valor)
        {
            //Arrange
            var conta = ContaBuilder.Build();
            var saldoEsperado = conta.Saldo + valor;
            
            //Act
            conta.Receber(valor);
            
            //Assert
            Assert.Equal(saldoEsperado, conta.Saldo);
        }
    }
}