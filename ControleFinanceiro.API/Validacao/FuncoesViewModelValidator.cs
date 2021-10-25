using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.BLL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacao
{
    public class FuncoesViewModelValidator : AbstractValidator<FuncoesViewModel>
    {
        public FuncoesViewModelValidator()
        {
            RuleFor(f => f.Nome)
                .NotNull().WithMessage("Preenhcer o Nome")
                .NotEmpty().WithMessage("O Nome não pode ser vazio")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use menos caracteres");

            RuleFor(f => f.Descricao)
                .NotNull().WithMessage("Preenhcer a descrição")
                .NotEmpty().WithMessage("A Descrição não pode ser vazia")
                .MinimumLength(1).WithMessage("Use mais caracteres para a descrição")
                .MaximumLength(50).WithMessage("Use menos caracteres");
        }
    }
}
