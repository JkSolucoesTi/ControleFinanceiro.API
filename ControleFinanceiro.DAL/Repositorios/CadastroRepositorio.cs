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
    public class CadastroRepositorio : RepositorioGenerico<Cadastro>, ICadastroRepositorio
    {
        private readonly Contexto _contexto;
        public CadastroRepositorio(Contexto contexto) :base(contexto)
        {
            _contexto = contexto;
        }

        public new IQueryable<Cadastro> PegarTodos()
        {
            try
            {
                return _contexto.Cadastros.Include(s => s.Sexo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public new async Task<Cadastro> PegarPeloId(int id)
        {
            try
            {
                return await _contexto.Cadastros.Include(c => c.Sexo)
                    .FirstOrDefaultAsync(a => a.CadastroId == id);
                    
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
