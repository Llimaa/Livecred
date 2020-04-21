using Flunt.Notifications;
using Flunt.Validations;
using Livecred.Domain.Shared.Commands;
using System;

namespace Livecred.Domain.Commands.Inputs.Parcela
{
    public class ParcelaUpdateValor : Notifiable, Icommand
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public void Validate()
        {
            new Contract()
                .HasMinLen(Valor.ToString(), 1, "Valor", "A parcela precisa ter um valor maior que 0")
                .IsNotNullOrEmpty(Id.ToString(), "Id", "O Id precisa ter um valor preenchido");
        }
    }
}
