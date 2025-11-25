using FluentValidation;
using Projekcik.Entities; // Jeśli potrzebujesz odwołań do encji, choć tu raczej nie
using System.Linq;

namespace Projekcik.application.Users
{
    public class UsersDtoValidator : AbstractValidator<UsersDto>
    {
        public UsersDtoValidator()
        {
            RuleFor(x => x.Imie)
                .NotEmpty().WithMessage("Imię jest wymagane.")
                .MaximumLength(50).WithMessage("Imię jest zbyt długie.");

            RuleFor(x => x.Nazwisko)
                .NotEmpty().WithMessage("Nazwisko jest wymagane.")
                .MaximumLength(50).WithMessage("Nazwisko jest zbyt długie.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany.")
                .EmailAddress().WithMessage("Niepoprawny format adresu email.");

            RuleFor(x => x.NumerTelefonu)
                .NotEmpty().WithMessage("Numer telefonu jest wymagany.");
            // Opcjonalnie: .Matches(@"^\d{9}$").WithMessage("Telefon musi mieć 9 cyfr");

            // Walidacja hasła (tylko jeśli jest wpisane - ważne przy edycji, jeśli używasz tego samego DTO)
            // Jeśli to DTO tylko do rejestracji, usuń ".When"
            RuleFor(x => x.Haslo)
                .NotEmpty().WithMessage("Hasło jest wymagane.")
                .MinimumLength(8).WithMessage("Hasło musi mieć co najmniej 8 znaków.");

            RuleFor(x => x.Wiek)
                .InclusiveBetween(1, 120).WithMessage("Wiek musi być realny (1-120).")
                .When(x => x.Wiek.HasValue); // Sprawdzaj tylko, gdy ktoś podał wiek

            RuleFor(x => x.KodPocztowy)
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Kod pocztowy musi być w formacie 00-000.")
                .When(x => !string.IsNullOrEmpty(x.KodPocztowy));
        }
    }
}