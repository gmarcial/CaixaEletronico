using System;

namespace CaixaEletronico.Core.Helpers
{
    public static class Validacoes
    {
        public static void ZeroOuNegativo(decimal valor, string nomeDoParametro)
        {
            if (valor <= 0)
                throw new ArgumentOutOfRangeException("O valor a ser depositado não pode ser zero ou negativo",
                    nameof(nomeDoParametro));
        }
    }
}