using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaixaEletronico.Core.Domain.Entities;

namespace CaixaEletronico.Core.Domain.Repositories
{
    public interface IContaRepository
    {
        ValueTask<Conta> ObterContaPeloNumeroAsync(Guid numero);
        ValueTask<decimal> ObterSaldoAsync(Guid numero);
        ValueTask<IEnumerable<Extrato>> ObterExtrato(Guid numero);
    }
}