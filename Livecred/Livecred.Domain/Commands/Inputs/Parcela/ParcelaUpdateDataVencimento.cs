using Flunt.Notifications;
using Flunt.Validations;
using Livecred.Domain.Shared.Commands;
using System;

namespace Livecred.Domain.Commands.Inputs.Parcela
{
    public class ParcelaUpdateDataVencimento : Notifiable, IcommandInput
    {
        public Guid Id { get; set; }
        public DateTime DataVencimento { get; set; }
        public void Validate()
        {
            new Contract()
            .IsNotNullOrEmpty(Id.ToString(), "Id", "O Id precisa ter um valor preenchido")
            .IsGreaterThan(DateTime.Now, DataVencimento, "DataVencimento", "A data de vencimento não pode ser menor que hoje");
        }
    }
}
