using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Projekcik.application.Users
{
    internal class LoginDtoValidator : AbstractValidator<UserEditDto>
    {
        public LoginDtoValidator()
        {

        }
    }
}
