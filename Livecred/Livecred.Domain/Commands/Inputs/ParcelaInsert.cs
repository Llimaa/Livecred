using Flunt.Validations;
using Livecred.Domain.Shared.Commands;
namespace Livecred.Domain.Commands.Inputs
{
    public class ParcelaInsert : Icommand
    {
        public decimal Valor { get; set; }
        public void Validate()
        {
            new Contract()
                .HasMinLen(Valor.ToString(), 1, "Valor", "A parcela precisa ter um valor maior que 0");
        }
    }
}
