using System;
using CaixaEletronico.Core.Domain.Entities;
using CaixaEletronico.Core.Domain.Interfaces;
using CaixaEletronico.Core.Domain.ValueObjects;
using Moq;
using Xunit;

namespace CaixaEletronico.tests.UnitTests.Core.Domain.ValueObjects
{
    //TODO Mapear builders necessarios
    public class DepositoBancarioTest
    {
        [Fact]
        public void Um_deposito_somente_deve_ser_criado_em_um_estado_valido()
        {
            var favorecido = new Mock<IConta>().Object;

            var depositoBancario = new DepositoBancario(favorecido, Guid.NewGuid(), 100);

            Assert.IsType<DepositoBancario>(depositoBancario);
            Assert.Equal(typeof(DepositoBancario), depositoBancario.GetType());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Um_deposipo_é_invalido_quando_o_valor_a_ser_depositado_é_igual_a_zero_ou_negativo(decimal valor)
        {
            var favorecido = new Mock<IConta>().Object;

            Assert.Throws<ArgumentOutOfRangeException>(() => new DepositoBancario(favorecido, Guid.NewGuid(), valor));
        }

        //TODO Refatorar de Fact para Theory
        [Fact]
        public void Um_deposito_de_sucesso_deposita_o_valor_do_deposito_no_favorecido()
        {
            var favorecido = new Conta(123123, 12341, 2323, Guid.NewGuid(), 100);
            var saldoAnteriorFavorecido = favorecido.Saldo;
            
            var depositoBancario = new DepositoBancario(favorecido, Guid.NewGuid(), 100);
            
            depositoBancario.Depositar();
            
            Assert.Equal(saldoAnteriorFavorecido + 100, favorecido.Saldo);
        }
    }
}