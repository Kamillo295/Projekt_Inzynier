using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Projekcik.application.Users
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.NoweHaslo)
                .NotEmpty().WithMessage("Hasło jest wymagane.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
                .WithMessage("Hasło musi mieć min. 8 znaków, zawierać co najmniej jedną dużą literę, jedną małą literę i cyfrę.");

            RuleFor(x => x.PowtorzHaslo)
                .NotEmpty().WithMessage("Musisz powtórzyć hasło.")
                .Equal(x => x.NoweHaslo).WithMessage("Hasła muszą być identyczne.");
        }
    }
}
