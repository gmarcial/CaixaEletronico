using System;
using System.Runtime.InteropServices;
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
            //Futuro builder de conta
            var numero = Guid.NewGuid();
            
            //Act
            var conta = new Conta(39491, 230103, 0231, numero, 1000);

            //Assert
            Assert.Equal(typeof(Conta), conta.GetType());
            Assert.IsType<Conta>(conta);
        }

        [Fact]
        public void Uma_conta_não_deve_ser_criada_sem_um_numero()
        {
            //Arrange
            //Futuro builder de conta
            var numeroVazio = Guid.Empty;
            
            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Conta(39491, 230103, 0231, numeroVazio, 1000));
        }

        [Fact]
        public void Uma_conta_não_deve_ser_criada_com_saldo_negativo()
        {
            //Arrange
            //Futuro builder de conta
            var numero = Guid.NewGuid();
            const int saldoNegativo = -1;
            
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => new Conta(39491, 230103, 0231, numero, saldoNegativo));

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
            //Futuro builder de conta
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

        [Theory]
        [InlineData(500)]
        [InlineData(375)]
        [InlineData(735)]
        [InlineData(1075)]
        [InlineData(2340)]
        public void Uma_transferencia_é_valida_quando_o_valor_for_maior_que_zero(decimal valor)
        {
            //Arrange
            //Futuro builder de conta
            conta.Depositar(5000);
            
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
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => conta.Transferir(valor));
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
            //Futuro builder de conta
            var numero = Guid.NewGuid();
            var conta = new Conta(1023,3240,1235, numero, valor);
            
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
            //Futuro builder de conta
            
            //Act
            //Asser
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Transferir(valor));
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
            //Futuro builder de conta
            conta.Depositar(5000);
            
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
            //Futuro builder de conta
            conta.Depositar(5000);
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
            //Futuro builder de conta
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
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => conta.Receber(valor));
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
            //Futuro builder de conta
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
            //Futuro builder de conta
            
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => conta.Receber(valor));
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
            //Futuro builder de conta
            var saldoEsperado = conta.Saldo + valor;
            
            //Act
            conta.Receber(valor);
            
            //Assert
            Assert.Equal(saldoEsperado, conta.Saldo);
        }
    }
}