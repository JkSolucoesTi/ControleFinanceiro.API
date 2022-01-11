using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {

        private readonly ICartaoRepositorio _cartaoRepositorio;
        private readonly IGanhosRepositorio _ganhoRepositorio;
        private readonly IDespesaRepositorio _despesaRepositorio;
        private readonly IMesRepositorio _mesRepositorio;
        private readonly IGraficoRepositorio _graficoRepositorio;

        public DashBoardController(ICartaoRepositorio cartaoRepositorio, IGanhosRepositorio ganhoRepositorio, IDespesaRepositorio despesaRepositorio, IMesRepositorio mesRepositorio, IGraficoRepositorio graficoRepositorio)
        {
            _cartaoRepositorio = cartaoRepositorio;
            _ganhoRepositorio = ganhoRepositorio;
            _despesaRepositorio = despesaRepositorio;
            _mesRepositorio = mesRepositorio;
            _graficoRepositorio = graficoRepositorio;
        }

        [HttpGet("PegarDadosCardsDashboard/{usuarioId}")]
        public async Task<ActionResult<DadosCardsDashBoardViewModel>> PegarDadosCardsDashboard(string usuarioId)
        {
            int qtdCartoes = await _cartaoRepositorio.PegarQuantidadeCartoesPeloUsuarioId(usuarioId);
            double ganhoTotal = Math.Round(await _ganhoRepositorio.PegarGanhoTotaPeloUsuarioId(usuarioId),2);
            double despesaTotal = Math.Round(await _despesaRepositorio.PegarDespesaTotalPeloUsuarioId(usuarioId), 2);

            double saldo = Math.Round(ganhoTotal - despesaTotal, 2);

            DadosCardsDashBoardViewModel model = new DadosCardsDashBoardViewModel
            {
                QtdCartoes = qtdCartoes,
                DespesaTotal = despesaTotal,
                GanhoTotal = ganhoTotal,
                Saldo=saldo
            };

            return model;

        }

        [HttpGet("PegarDadosAnuaisPeloUsuarioId/{usuarioId}/{ano}")]
        public object PegarDadosAnuaisPeloUsuarioId(string usuarioId, int ano)
        {
            return (new
            {
                ganhos = _graficoRepositorio.PegarGanhosAnuaisPeloUsuarioId(usuarioId, ano),
                despesas = _graficoRepositorio.PegarDespesasAnuaisPeloUsuarioId(usuarioId,ano),
                meses = _mesRepositorio.PegarTodos()
            });
        }
    }
}
