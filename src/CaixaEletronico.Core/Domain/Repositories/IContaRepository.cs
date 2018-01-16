using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaixaEletronico.Core.Domain.Entities;
using CaixaEletronico.Infrastructure;
using CaixaEletronico.Infrastructure.Logger;

namespace CaixaEletronico.Core.Domain.Repositories
{
    public interface IContaRepository
    {
        ValueTask<Conta> ObterContaPeloNumeroAsync(Guid numero);
        ValueTask<decimal> ObterSaldoAsync(Guid numero);
        ValueTask<IEnumerable<Extrato>> ObterExtrato(Guid numero);
    }
}