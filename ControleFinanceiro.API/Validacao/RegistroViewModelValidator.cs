using ControleFinanceiro.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacao
{
    public class RegistroViewModelValidator : AbstractValidator<RegistroViewModel>
    {
        public RegistroViewModelValidator()
        {
            RuleFor(r => r.NomeUsuario)
                .NotNull().WithMessage("Preencha o nome de usuário")
                .NotEmpty().WithMessage("Preencha o nome de usuário")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use menos caracteres");

            RuleFor(r => r.CPF)
               .NotNull().WithMessage("Preencha o CPF de usuário")
               .NotEmpty().WithMessage("Preencha o CPF de usuário")
               .MinimumLength(1).WithMessage("Use mais caracteres")
               .MaximumLength(20).WithMessage("Use menos caracteres");

            RuleFor(r => r.Profissao)
               .NotNull().WithMessage("Preencha a Profissão")
               .NotEmpty().WithMessage("Preencha a Profissão")
               .MinimumLength(1).WithMessage("Use mais caracteres")
               .MaximumLength(30).WithMessage("Use menos caracteres");

            RuleFor(r => r.Foto)
           .NotNull().WithMessage("Escolha a foto")
           .NotEmpty().WithMessage("Escolha a foto");

            RuleFor(r => r.Email)
               .NotNull().WithMessage("Preencha o email")
               .NotEmpty().WithMessage("Preencha o email")
               .MinimumLength(5).WithMessage("Use mais caracteres")
               .MaximumLength(50).WithMessage("Use menos caracteres")
               .EmailAddress().WithMessage("Email inválido");

            RuleFor(r => r.Senha)
               .NotNull().WithMessage("Preencha a Senha")
               .NotEmpty().WithMessage("Preencha a Senha")
               .MinimumLength(6).WithMessage("Use mais caracteres")
               .MaximumLength(50).WithMessage("Use menos caracteres");



        }
    }
}
