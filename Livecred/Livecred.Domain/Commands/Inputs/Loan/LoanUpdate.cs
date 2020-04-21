using Flunt.Notifications;
using Flunt.Validations;
using Livecred.Domain.Enuns;
using Livecred.Domain.Shared.Commands;
using System;

namespace Livecred.Domain.Commands.Inputs.Loan
{
    public class LoanUpdate : Notifiable, Icommand
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public double Juro { get; set; }
        public EStatusEmprestimo Status { get; set; }

        public void Validate()
        {
            new Contract()
               .Requires()
               .HasMinLen(Valor.ToString(), 100, "Valor", "Valor minimo para realizar um emprestimo é R$:100")
               .IsNotNullOrEmpty(Juro.ToString(), "Juro", "O campo juro não pode ficar em branco")
               .IsNotNullOrEmpty(Status.ToString(), "Status", "O campo status não pode ficar em branco");
        }
    }
}
