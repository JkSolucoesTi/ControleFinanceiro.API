using ControleFinanceiro.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacao
{
    public class AtualizarUsuarioViewModelValidator : AbstractValidator<AtualizarUsuarioViewModel>
    {
        public AtualizarUsuarioViewModelValidator()
        {
            RuleFor(g => g.UserName)
                .NotNull().WithMessage("Preencha o nome do usuário")
                .NotEmpty().WithMessage("Preencha o nome do usuário")
                .MinimumLength(6).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use menos caracteres");
        }
    }
}
