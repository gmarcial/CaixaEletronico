using System;
using CaixaEletronico.Core.Domain.Interfaces;

namespace CaixaEletronico.Core.Domain.ValueObjects
{
    public class DepositoBancario
    {
        public IConta Favorecido { get; }
        public decimal Valor { get; }
        public Guid Numero { get; }

        public DepositoBancario(IConta favorecido, Guid numero, decimal valor = 0)
        {
            //TODO Encapsular validações basicas/comuns
            if (favorecido.Equals(null))
                throw new ArgumentNullException(nameof(favorecido), "O favorecido esta nullo.");
            
            if(valor <= 0)
                throw new ArgumentOutOfRangeException(nameof(valor), "O argumento valor esta negativo");
            
            if (numero.Equals(null))
                throw new ArgumentException("O numero do deposito não foi gerado em sua criação", nameof(numero));
            
            Favorecido = favorecido;
            Valor = valor;
            Numero = numero;
        }

        public void Depositar()
        {
            Favorecido.Depositar(Valor);
        }
    }
}