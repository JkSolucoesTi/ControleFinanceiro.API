﻿using ControleFinanceiro.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IGanhosRepositorio : IRepositorioGenerico<Ganho>
    {
       IQueryable<Ganho> PegarGanhoPeloUsuarioId(string usuarioId);
       IQueryable<Ganho> FiltrarGanhos(string nomeCategoria);
        Task<double> PegarGanhoTotaPeloUsuarioId(string usuarioId);
    }
}
