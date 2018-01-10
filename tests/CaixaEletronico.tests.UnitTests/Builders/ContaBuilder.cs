using System;
using CaixaEletronico.Core.Domain.Entities;

namespace CaixaEletronico.tests.UnitTests.Builders
{
    public class ContaBuilder
    {
        private long ParametroId;
        private long ParametroAgenciaId;
        private long ParametroPessoaId;
        private Guid ParametroNumero;
        private decimal ParametroSaldo;

        public ContaBuilder()
        {
            ParametroId = 83712;
            ParametroAgenciaId = 9134;
            ParametroPessoaId = 1301;
            ParametroNumero = Guid.NewGuid();
            ParametroSaldo = 0;
        }

        public ContaBuilder Id(long id)
        {
            ParametroId = id;

            return this;
        }

        public ContaBuilder AgenciaId(long agenciaId)
        {
            ParametroAgenciaId = agenciaId;

            return this;
        }

        public ContaBuilder PessoaId(long pessoaId)
        {
            ParametroPessoaId = pessoaId;

            return this;
        }

        public ContaBuilder NumeroEmpty()
        {
            ParametroNumero = Guid.Empty;

            return this;
        }

        public ContaBuilder Saldo(decimal saldo)
        {
            ParametroSaldo = saldo;

            return this;
        }

        public Conta Build()
        {
            return new Conta(ParametroId, ParametroAgenciaId, ParametroPessoaId, ParametroNumero, ParametroSaldo);
        }
    }
}