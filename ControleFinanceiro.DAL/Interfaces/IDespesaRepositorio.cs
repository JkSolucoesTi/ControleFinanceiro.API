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
        IQueryable<Despesa> FiltrarDespesa(string nomeCategoria);
        IQueryable<Despesa> PegarDespesaPeloUsuarioId(string usuarioID);
        IQueryable<Despesa> PegarDespesaPeloId(int usuarioID);
        void ExcluirDespesas(IEnumerable<Despesa> despesas);
        Task<IEnumerable<Despesa>> PegarDespesaPeloCartaoId(int usuarioID);
        Task<double> PegarDespesaTotalPeloUsuarioId(string usuarioId);

    }
}
