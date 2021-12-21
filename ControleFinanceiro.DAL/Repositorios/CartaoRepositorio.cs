using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class CartaoRepositorio : RepositorioGenerico<Cartao>, ICartaoRepositorio
    {
        private readonly Contexto _context;
        public CartaoRepositorio(Contexto contexto) : base(contexto)
        {
            _context = contexto;
        }

        public IQueryable<Cartao> FiltrarCartoes(string numeroCartao)
        {
            try
            {
                return _context.Cartoes.Where(c => c.Numero.Contains(numeroCartao));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<Cartao> PegarCartaoPeloUsuarioId(string usuarioId)
        {
            try
            {
               return _context.Cartoes.Where(c => c.UsuarioId == usuarioId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}