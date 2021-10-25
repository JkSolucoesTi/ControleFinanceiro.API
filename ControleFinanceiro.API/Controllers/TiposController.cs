using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private readonly ITipoRepositorio _tipoRepositorio;

        public TiposController(ITipoRepositorio tipoRepositorio)
        {
            _tipoRepositorio = tipoRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo>>> GetTipos()
        {
            var tipo = await _tipoRepositorio.PegarTodos().ToListAsync();
            return tipo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo>> GetTipo (int id)
        {
            var tipo = await _tipoRepositorio.PegarPeloId(id);

            if(tipo == null)
            {
                return NotFound();
            }

            return tipo;

        }

    }
}
