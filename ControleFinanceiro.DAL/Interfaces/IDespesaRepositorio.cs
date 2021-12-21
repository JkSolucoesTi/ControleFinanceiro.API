using ControleFinanceiro.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IDespesaRepositorio : IRepositorioGenerico<Despesa>
    {
        IQueryable<Despesa> PegarDespesaPeloUsuarioId(string usuarioID);

        void ExcluirDespesas(IEnumerable<Despesa> despesas);

        Task<IEnumerable<Despesa>> PegarDespesaPeloCartaoId(int usuarioID);


    }
}
