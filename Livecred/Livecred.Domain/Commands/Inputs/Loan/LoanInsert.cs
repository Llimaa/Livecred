using Flunt.Notifications;
using Flunt.Validations;
using Livecred.Domain.Shared.Commands;
namespace Livecred.Domain.Commands.Inputs.Loan
{
    public class LoanInsert : Notifiable, Icommand
    {
        public decimal Valor { get; set; }
        public double Juro { get; set; }

        public void Validate()
        {
            new Contract()
                .Requires()
                .HasMinLen(Valor.ToString(), 100, "Valor", "Valor minimo para realizar um emprestimo é R$:100")
                .IsNotNullOrEmpty(Juro.ToString(), "Juro", "O campo juro não pode ficar em branco");
        }
    }
}
