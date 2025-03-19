using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ModelBindingAndValidations.model
{
    public class Person
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Price { get; set; }


        public override string ToString()
        {
            return $"Perosn object - \n" +
               $"\nPerson name: {Name}" +
               $"\nE-mail: {Email}" +
               $"\nPhone: {Phone}" +
               $"\nPassword: {Password}" +
               $"\nConfirm Password: {ConfirmPassword}" +
               $"\nPrice: {Price}";
        }

    }
}
