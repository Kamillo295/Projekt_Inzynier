using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Projekcik.application.Users
{
    public class UserEditDtoValidatior : AbstractValidator<UserEditDto>
    {
        public UserEditDtoValidatior()
        {
            RuleFor(x => x.Imie)
                .NotEmpty().WithMessage("Imię jest wymagane.")
                .Length(2, 20).WithMessage("Imię musi mieć między 2 a 20 znaków.");

            RuleFor(x => x.Nazwisko)
                .NotEmpty().WithMessage("Nazwisko jest wymagane.")
                .Length(2, 20).WithMessage("Nazwisko musi mieć między 2 a 20 znaków.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany.")
                .EmailAddress().WithMessage("Niepoprawny format adresu email.");

            RuleFor(x => x.NumerTelefonu)
                .NotEmpty().WithMessage("Numer telefonu jest wymagany.")
                .Matches(@"^\d+$").WithMessage("Pole może zawierać tylko cyfry!")
                .Length(9, 9).WithMessage("Telefon musi mieć 9 cyfr");

            RuleFor(x => x.Wiek)
                .InclusiveBetween(1, 120).WithMessage("Wiek musi być realny (1-120).")
                .When(x => x.Wiek.HasValue);

            RuleFor(x => x.KodPocztowy)
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Kod pocztowy musi być w formacie 00-000.")
                .When(x => !string.IsNullOrEmpty(x.KodPocztowy));

            RuleFor(x => x.RozmiarKoszulki);
                //.NotEmpty().WithMessage("Wybierz rozmiar koszulki");
        }
        public enum RozmiarKoszulkiTyp { XS, S, M, L, XL, XXL }
    }


}