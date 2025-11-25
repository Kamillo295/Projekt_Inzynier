using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekcik.application.Users
{
    public class UsersDtoValidator : AbstractValidator<UsersDto>
    {
        public UsersDtoValidator(Projekcik.Entities.Users repository)
        {
            RuleFor(c => c.Imie)
               .NotEmpty().WithMessage("Wpisz imię debilu")
               .EmailAddress().WithMessage("aaaaaaaa");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("email")
                .Custom((value, context) =>
                {
                    var existingUser = repository.GetByName(value);
                });

        }


    }
}
