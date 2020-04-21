using Flunt.Notifications;
using Flunt.Validations;
using Livecred.Domain.Shared.Commands;
using System;

namespace Livecred.Domain.Commands.Inputs.Parcela
{
    public class ParcelaUpdateJuroAtraso : Notifiable, IcommandInput
    {
        public Guid Id { get; set; }
        public double Juro { get; set; }
        public void Validate()
        {
            new Contract()
                .IsNotNullOrEmpty(Id.ToString(), "Id", "O Id precisa ter um valor preenchido")
                .IsNotNullOrEmpty("Juro", "Juro", "O campo juro precisa ser preenchido");
        }
    }
}
