using Flunt.Notifications;
using Flunt.Validations;

namespace Livecred.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "FirstName", "O primeiro nome é obrigátorio")
                .HasMinLen(LastName, 3, "FirstName", "O utimo nome é obrigátorio");


            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
