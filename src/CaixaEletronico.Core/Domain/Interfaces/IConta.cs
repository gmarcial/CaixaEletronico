namespace CaixaEletronico.Core.Domain.Interfaces
{
    public interface IConta
    {
        void Depositar(decimal valor);
        void Sacar(decimal valor);
    }
}