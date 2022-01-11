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
    public class GanhoRepositorio : RepositorioGenerico<Ganho>, IGanhosRepositorio
    {
        private readonly Contexto _contexto;
        public GanhoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<Ganho> FiltrarGanhos(string nomeCategoria)
        {
            try
            {
                return _contexto.Ganhos
                    .Include(c => c.Mes)
                    .Include(c => c.Categoria)
                    .ThenInclude(g => g.Tipo)
                    .Where(c => c.Categoria.Nome.Contains(nomeCategoria) 
                             && c.Categoria.Tipo.Nome.Contains("Ganho"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Ganho> PegarGanhoPeloUsuarioId(string usuarioId)
        {
            try
            {
                return _contexto.Ganhos.Include(g => g.Mes)
                    .Include(g => g.Categoria)
                    .Where(c => c.UsuarioId == usuarioId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<double> PegarGanhoTotaPeloUsuarioId(string usuarioId)
        {
            try
            {
                return await _contexto.Ganhos.Where(c => c.UsuarioId == usuarioId)
                    .SumAsync(c => c.Valor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
