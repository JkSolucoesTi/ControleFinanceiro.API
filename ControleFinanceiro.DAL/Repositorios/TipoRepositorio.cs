using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class TipoRepositorio : RepositorioGenerico<Tipo> , ITipoRepositorio
    {
        private readonly Contexto _context;
        public TipoRepositorio(Contexto contexto) : base(contexto)
        {
            _context = contexto;
        }
    }
}
