using Flunt.Validations;
using Livecred.Domain.Shared.Commands;

namespace Livecred.Domain.Commands.Inputs
{
    public class ClientInsert : IcommandInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NuberDocument { get; set; }
        public string Telephone { get; set; }
        public string Endereco { get; set; }

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
