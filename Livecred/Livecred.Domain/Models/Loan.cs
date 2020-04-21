using Livecred.Domain.Enuns;
using Livecred.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Livecred.Domain.Models
{
    public class Loan : EntityBase
    {
        public Loan(decimal valor, double juro, Guid idClient)
        {
            Valor = valor;
            Juro = juro;
            Status = EStatusEmprestimo.Aberto;
            DataCadastro = DateTime.Now;
            IdClient = idClient;
        }

        public decimal Valor { get; private set; }
        public Guid IdClient { get; private set; }
        public double Juro { get; private set; }
        public EStatusEmprestimo Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataUpdate { get; private set; }
        public List<Parcela> Parcelas { get; set; }

        public void Update(decimal valor, double juro, EStatusEmprestimo status)
        {
            Valor = valor;
            Juro = juro;
            Status = status;
            DataUpdate = DateTime.Now;
        }

        public void AdicionarParcela(Parcela parcela)
        {
            this.Parcelas.Add(parcela);
        }

        public void AddRangeParcela(List<Parcela> parcelas)
        {
            this.Parcelas.AddRange(parcelas);
        }

        public void AtualizarParcela(Parcela parcela)
        {
            this.RemoverParcela(parcela.Id);
            this.AdicionarParcela(parcela);
        }

        public void RemoverParcela(Guid id)
        {
            Parcelas.RemoveAt(GetIndex(id));
        }

        private int GetIndex(Guid id)
        {
            return Parcelas.IndexOf(Parcelas.Where(x => x.Id == id).First());
        }
    }
}