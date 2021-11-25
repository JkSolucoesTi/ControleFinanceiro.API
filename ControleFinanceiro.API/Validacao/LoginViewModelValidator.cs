using ControleFinanceiro.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacao
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(l => l.Email)
                .NotNull().WithMessage("Preencha o email")
                .NotEmpty().WithMessage("Preencha o email")
                .MinimumLength(5).WithMessage("Utilize mais caracteres")
                .MaximumLength(50).WithMessage("Utilize menos caracteres");

            RuleFor(l => l.Senha)
                .NotNull().WithMessage("Preencha a Senha")
                .NotEmpty().WithMessage("Preencha a Senha")
                .MinimumLength(5).WithMessage("Utilize mais caracteres")
                .MaximumLength(50).WithMessage("Utilize menos caracteres");




        }
    }
}
