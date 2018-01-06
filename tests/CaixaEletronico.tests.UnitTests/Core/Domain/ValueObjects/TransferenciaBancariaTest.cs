using System;
using CaixaEletronico.Core.Domain.Entities;
using CaixaEletronico.Core.Domain.Interfaces;
using CaixaEletronico.Core.Domain.ValueObjects;
using Moq;
using Xunit;

namespace CaixaEletronico.tests.UnitTests.Core.Domain.ValueObjects
{
    //TODO Mapear builders necessarios
    public class TransferenciaBancariaTest
    {
        [Fact]
        public void Uma_transferencia_somente_deve_ser_criada_em_um_estado_valido()
        {
            var remetente = new Mock<IConta>();
            remetente.SetupGet(s => s.Saldo).Returns(200);
            
            var favorecido = new Mock<IConta>();
            favorecido.SetupGet(s => s.Saldo).Returns(100);
            
            var transferencia = new TransferenciaBancaria(remetente.Object, favorecido.Object, Guid.NewGuid(), 100);

            Assert.Equal(typeof(TransferenciaBancaria), transferencia.GetType());
            Assert.IsType<TransferenciaBancaria>(transferencia);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Uma_transferencia_não_deve_ser_criada_se_o_valor_transferido_for_zero_ou_negativo(decimal valor)
        {
            var remetente = new Mock<IConta>();
            remetente.SetupGet(s => s.Saldo).Returns(200);
            
            var favorecido = new Mock<IConta>();
            favorecido.SetupGet(s => s.Saldo).Returns(100);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new TransferenciaBancaria(remetente.Object, favorecido.Object, Guid.NewGuid(), valor));
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Uma_transferencia_não_deve_ser_criada_se_o_saldo_do_remetente_for_zero_ou_negativo(decimal valor)
        {
            var remetente = new Mock<IConta>();
            remetente.SetupGet(s => s.Saldo).Returns(valor);
            
            var favorecido = new Mock<IConta>();
            favorecido.SetupGet(s => s.Saldo).Returns(100);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new TransferenciaBancaria(remetente.Object, favorecido.Object, Guid.NewGuid(), 500));
        }
        
        [Fact]
        public void Uma_transferencia_não_deve_ser_criada_se_o_valor_transferido_for_maior_que_o_saldo_do_remetente()
        {
            var remetente = new Mock<IConta>();
            remetente.SetupGet(s => s.Saldo).Returns(200);
            
            var favorecido = new Mock<IConta>();
            favorecido.SetupGet(s => s.Saldo).Returns(100);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new TransferenciaBancaria(remetente.Object, favorecido.Object, Guid.NewGuid(), 500));
        }

        //TODO Refatorar de Fact para Theory
        [Fact]
        public void 
            Uma_transferencia_de_sucesso_subtrai_o_saldo_do_remetente_e_atribuido_no_favorecido_o_valor_transferido()
        {
            var remetente = new Conta(101010, 202020, 30301, Guid.NewGuid(), 250);
            var saldoAnteriorRemetente = remetente.Saldo;
            
            var favorecido = new Conta(1201, 10231, 100, Guid.NewGuid(), 100);
            var saldoAnteriorFavorecido = favorecido.Saldo;
            
            var transferencia = new TransferenciaBancaria(remetente, favorecido, Guid.NewGuid(), 100);
            transferencia.Transferir();
            
            Assert.Equal(saldoAnteriorRemetente - 100, remetente.Saldo);
            Assert.Equal(saldoAnteriorFavorecido + 100, favorecido.Saldo);
        }
    }
}