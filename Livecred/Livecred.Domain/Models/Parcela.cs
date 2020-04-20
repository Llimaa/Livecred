using Livecred.Domain.Enuns;
using Livecred.Domain.Shared;
using System;

namespace Livecred.Domain.Models
{
    public class Parcela : EntityBase
    {
        public Parcela(decimal valor, double juroAtraso, EStatusParcela status)
        {
            Valor = valor;
            JuroAtraso = 0;
            Status = EStatusParcela.EmDias;
            DataVencimento = DateTime.Now.AddDays(31);
        }

        public decimal Valor { get; private set; }
        public decimal ValorComJuro { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public double JuroAtraso { get; private set; }
        public EStatusParcela Status { get; private set; }

        public void UpdateValor(decimal valor)
        {
            Valor = valor;
        }

        private void UpdateValorComJuro(decimal valor)
        {
            ValorComJuro = valor;
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

        private void UpdateStatus(EStatusParcela status)
        {
            Status = status;
        }
    }
}