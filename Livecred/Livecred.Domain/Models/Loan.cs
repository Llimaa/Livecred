using Livecred.Domain.Enuns;
using Livecred.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Livecred.Domain.Models
{
    public class Loan : EntityBase
    {
        public Loan(decimal valor, double juro)
        {
            Valor = valor;
            Juro = juro;
            Status = EStatusEmprestimo.Aberto;
            CataCadastro = DateTime.Now;
        }

        public decimal Valor { get; private set; }
        public double Juro { get; private set; }
        public EStatusEmprestimo Status { get; private set; }
        public DateTime CataCadastro { get; private set; }
        public DateTime DataUpdate { get; private set; }
        public IList<Parcela> Parcelas { get; set; }

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