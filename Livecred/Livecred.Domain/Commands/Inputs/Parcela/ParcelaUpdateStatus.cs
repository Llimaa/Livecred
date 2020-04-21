using Flunt.Notifications;
using Livecred.Domain.Shared.Commands;
using System;
using Flunt.Validations;
using Livecred.Domain.Enuns;

namespace Livecred.Domain.Commands.Inputs.Parcela
{
    public class ParcelaUpdateStatus : Notifiable, IcommandInput
    {
        public Guid Id { get; set; }
        public EStatusParcela Status { get; set; }
        public void Validate()
        {
            new Contract()
                .IsNotNullOrEmpty(Id.ToString(), "Id", "O Id precisa ter um valor preenchido")
                .IsNotNullOrEmpty("Status", "Status", "O campo Status tem que ser preenchido");
        }
    }
}
