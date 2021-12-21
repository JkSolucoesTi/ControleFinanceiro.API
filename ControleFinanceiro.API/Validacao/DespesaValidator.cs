using ControleFinanceiro.BLL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Validacao
{
    public class DespesaValidator : AbstractValidator<Despesa>
    {
        public DespesaValidator()
        {
            RuleFor(d => d.CartaoId)
                .NotEmpty().WithMessage("Escolha o cartão")
                .NotNull().WithMessage("Escolha o cartão");

            RuleFor(d => d.Descricao)
                .NotEmpty().WithMessage("Preencha a descrição")
                .NotNull().WithMessage("Preencha a descrição")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use menos caracteres");

            RuleFor(d => d.Valor)
                .NotEmpty().WithMessage("Preencha o valor")
                .NotNull().WithMessage("Preencha o valor")
                .InclusiveBetween(0, double.MaxValue).WithMessage("Valor inválido");

            RuleFor(d => d.MesId)
                .NotEmpty().WithMessage("Escolha o mês")
                .NotNull().WithMessage("Escolha o mês");

            RuleFor(d => d.Ano)
                .NotEmpty().WithMessage("Escolha o ano")
                .NotNull().WithMessage("Escolha o ano")
                .InclusiveBetween(2016, 2021).WithMessage("Escolha o ano");

        }
    }
}
