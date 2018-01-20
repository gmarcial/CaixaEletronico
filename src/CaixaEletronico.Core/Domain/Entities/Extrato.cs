using System;

namespace CaixaEletronico.Infrastructure.Data
{
    public class Extrato
    {
        public long Id { get; set; }
        public long ContaId { get; set; }
        public string NumeroConta { get; set; }
        public string NumeroOperacao { get; set; }
        public string Operacao { get; set; }
        public DateTime Date { get; set; }
        public decimal Valor { get; set; }
    }
}