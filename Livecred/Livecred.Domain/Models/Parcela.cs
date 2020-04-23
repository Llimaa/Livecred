using Livecred.Domain.Enuns;
using Livecred.Domain.Shared;
using System;

namespace Livecred.Domain.Models
{
    public class Parcela : EntityBase
    {
        public Parcela(decimal valor, Guid idLoan)
        {
            IdLoan = idLoan;
            Valor = valor;
            JuroAtraso = 0;
            Status = EStatusParcela.EmDias;
            DataVencimento = DateTime.Now.AddDays(30);
        }
        public Parcela()
        {

        }

        public Guid IdLoan { get; private set; }
        public decimal Valor { get; private set; }
        public decimal ValorComJuro { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public double JuroAtraso { get; private set; }
        public EStatusParcela Status { get; private set; }

        public void UpdateValor(decimal valor)
        {
            Valor = valor;
        }

        public void UpdateJuroAtraso(double juro)
        {
            JuroAtraso = juro;
        }

        private void UpdateValorComJuro(decimal valor)
        {
            ValorComJuro = valor;
        }

        private void UpdateStatus(EStatusParcela status)
        {
            Status = status;
        }

        public void UpdateDataVencimento(DateTime dataVencimento)
        {
            DataVencimento = dataVencimento;
        }

        public bool VerificarSeParcelaEstaEmDias(DateTime dataVencimento)
        {
            var result = DateTime.Compare(DateTime.Now, dataVencimento);
            if (result <= 0)
                return true;
            else
            {
                UpdateStatus(EStatusParcela.Atrasado);
                return false;
            }
        }

        public decimal CalcularPrecoComJuro(decimal valor, DateTime dataVencimento)
        {
            var diasVencidos = DateTime.Compare(DateTime.Now, dataVencimento);
            var valorJuro = ((5 / 100) * valor) * diasVencidos;

            UpdateValorComJuro(valor + valorJuro);
            return valor + valorJuro;
        }
    }
}