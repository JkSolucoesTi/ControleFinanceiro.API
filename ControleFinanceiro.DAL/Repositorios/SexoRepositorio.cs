using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class SexoRepositorio : RepositorioGenerico<Sexo>, ISexoRepositorio
    {
        private readonly Contexto contexto;
        public SexoRepositorio(Contexto _contexto) : base(_contexto)
        {
            contexto = _contexto;
        }
    }
}
