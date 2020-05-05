using Flunt.Notifications;
using Flunt.Validations;
using Livecred.Domain.Shared.Commands;
using System;

namespace Livecred.Domain.Commands.Inputs.Client
{
    public class ClientUpdate : Notifiable, Icommand
    {
        public ClientUpdate(Guid id, string firstName, string lastName, string nuberDocument, string telephone, string address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            NuberDocument = nuberDocument;
            Telephone = telephone;
            Address = address;
        }

        public ClientUpdate()
        {

        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NuberDocument { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }

        public void Validate()
        {
            new Contract()
                .IsNotNullOrEmpty(FirstName, "Primeiro nome", "O campo do primeiro nome é obrigatório")
                .IsNotNullOrEmpty(LastName, "Ultimo nome", "O campo do Ultimo nome é obrigatório")
                .HasMinLen(NuberDocument, 11, "CPF", "Número de documento inválido")
                .HasMaxLen(NuberDocument, 11, "CPF", "Número de documento inválido");
        }
    }
}
