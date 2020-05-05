using Livecred.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livecred.Domain.Queries
{
    public class LoanQuery
    {
        public Guid Id { get; set; }
        public string NameClient { get; set; }
        public decimal Valor { get; set; }
        public decimal Juro { get; set; }
        public EStatusEmprestimo Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public int TotalParcelas { get; set; }
        public int TotalParcelasPagas { get; set; }
    }
}
