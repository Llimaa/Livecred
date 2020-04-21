using Livecred.Domain.Shared;
using Livecred.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Livecred.Domain.Models
{
    public class Client : EntityBase
    {
        public Client() { }
        public Client(Name name, Document cPF, string telephone, string address)
        {
            Name = name;
            CPF = cPF;
            Telephone = telephone;
            Address = address;
        }

        public Name Name { get; private set; }
        public Document CPF { get; private set; }
        public string Telephone { get; private set; }
        public string Address { get; private set; }

        public List<Loan> Loans { get; private set; } //Emprestimos

        public void Update(string firstName, string lastName, string document, string telephone, string address)
        {
            var name = new Name(firstName, lastName);
            var _document = new Document(document);
            Name = name;
            CPF = _document;
            Telephone = telephone;
            Address = address;
        }

        public void SetName(string name)
        {
            Name = new Name(name, name);
        }

        public void SetDocument(Document document)
        {
            CPF = document;
        }


        public void AddLoan(Loan loan)
        {
            this.Loans.Add(loan);
        }

        public void AddRageLoan(List<Loan> loans)
        {
            this.Loans.AddRange(loans);
        }

        public void UpdateLoan(Loan loan)
        {
            this.RemoverLoan(loan);
            this.AddLoan(loan);
        }

        public void RemoverLoan(Loan loan)
        {
            Loans.RemoveAt(getIndex(loan.Id));
        }

        private int getIndex(Guid id)
        {
            return Loans.IndexOf(Loans.Where(x => x.Id == id).First());
        }
    }
}