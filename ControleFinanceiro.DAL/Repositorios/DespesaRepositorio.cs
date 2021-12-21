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

        public IQueryable<Despesa> PegarDespesaPeloUsuarioId(string usuarioID)
        {
            try
            {
                return _contexto.Despesas.Include(d => d.Cartao)
                    .Include(d => d.Categoria)
                    .Include(d => d.Mes)
                    .Where(c => c.UsuarioId == usuarioID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
