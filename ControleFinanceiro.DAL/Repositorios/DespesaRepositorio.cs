using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class DespesaRepositorio : RepositorioGenerico<Despesa>, IDespesaRepositorio
    {
        private readonly Contexto _contexto;
        public DespesaRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public void ExcluirDespesas(IEnumerable<Despesa> despesas)
        {
            try
            {
                _contexto.Despesas.RemoveRange(despesas);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<Despesa> FiltrarDespesa(string nomeCategoria)
        {
            try
            {
                return _contexto.Despesas
                    .Include(c => c.Cartao)
                    .Include(d => d.Categoria)
                    .ThenInclude(d => d.Tipo)
                    .Include(m => m.Mes)
                    .Where(c => c.Categoria.Nome.Contains(nomeCategoria));                   
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Despesa>> PegarDespesaPeloCartaoId(int usuarioID)
        {
            try
            {
              return await _contexto.Despesas.Where(c => c.CartaoId == usuarioID).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<Despesa> PegarDespesaPeloId(int usuarioID)
        {
            try
            {
                return _contexto.Despesas.Include(d => d.Cartao)
                    .Include(d => d.Categoria)
                    .Include(d => d.Mes)
                    .Where(c => c.DespesaId == usuarioID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Despesa> PegarDespesaPeloUsuarioId(string usuarioID)
        {
            try
            {
                return _contexto.Despesas.Include(d => d.Cartao)
                    .Include(d => d.Categoria)
                    .Include(d => d.Mes)
                    .Where(c => c.UsuarioId == usuarioID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<double> PegarDespesaTotalPeloUsuarioId(string usuarioId)
        {
            try
            {
                return await _contexto.Despesas.Where(d => d.UsuarioId == usuarioId)
                    .SumAsync(a => a.Valor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
