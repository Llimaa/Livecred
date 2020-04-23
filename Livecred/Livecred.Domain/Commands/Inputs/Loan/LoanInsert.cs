using Flunt.Notifications;
using Flunt.Validations;
using Livecred.Domain.Shared.Commands;
using System;

namespace Livecred.Domain.Commands.Inputs.Loan
{
    public class LoanInsert : Notifiable, Icommand
    {
        public Guid IdClient { get; set; }
        public decimal Valor { get; set; }
        public double Juro { get; set; }

        public void Validate()
        {
            new Contract()
                .Requires()
                .HasMinLen(Valor.ToString(), 100, "Valor", "Valor minimo para realizar um emprestimo é R$:100")
                .IsNotNullOrEmpty(IdClient.ToString(), "IdClient", "O campo identificador do cliente não pode ficar em branco")
                .IsNotNullOrEmpty(Juro.ToString(), "Juro", "O campo juro não pode ficar em branco");
        }
    }
}
