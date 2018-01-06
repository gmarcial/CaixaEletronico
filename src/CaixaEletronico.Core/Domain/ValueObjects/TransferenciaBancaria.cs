using System;
using CaixaEletronico.Core.Domain.Interfaces;

namespace CaixaEletronico.Core.Domain.ValueObjects
{
    public class TransferenciaBancaria
    {
        public IConta Remetente { get; }
        public IConta Favorecido { get; }
        public decimal Valor { get; }
        public Guid Numero { get; }
        
        public TransferenciaBancaria(IConta remetente, IConta favorecido, Guid numero, decimal valor = 0)
        {
            //TODO Encapsular validações basicas/comuns
            if (remetente.Equals(null))
                throw new ArgumentNullException(nameof(remetente), "O remetente esta nullo.");
            
            if (favorecido.Equals(null))
                throw new ArgumentNullException(nameof(favorecido), "O favorecido esta nullo.");
            
            if(valor <= 0)
                throw new ArgumentOutOfRangeException(nameof(valor), "O argumento valor esta negativo");
            
            if (numero.Equals(null))
                throw new ArgumentException("O numero da transferencia não foi gerado em sua criação", nameof(numero));
            
            if(remetente.Saldo <= 0)
                throw new ArgumentOutOfRangeException(nameof(remetente.Saldo),
                    "O saldo do remetente é igual a zero ou negativo");
            
            if(remetente.Saldo < valor)
                throw new ArgumentOutOfRangeException(nameof(remetente.Saldo),
                    "O saldo do remetente é inferior ao valor a ser transferido");

            Remetente = remetente;
            Favorecido = favorecido;
            Valor = valor;
            Numero = numero;
        }

        public void Transferir()
        {
            Remetente.Sacar(Valor);
            
            Favorecido.Depositar(Valor);
        }
        
    }
}