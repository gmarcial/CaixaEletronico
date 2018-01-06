namespace CaixaEletronico.Core.Domain.Interfaces
{
    public interface IConta
    {
        decimal Saldo { get; }

        void Depositar(decimal valor);
        void Sacar(decimal valor);
    }
}