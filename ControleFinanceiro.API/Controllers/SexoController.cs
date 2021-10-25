using ControleFinanceiro.BLL.Models;
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
    public class SexoController : ControllerBase
    {
        private readonly ISexoRepositorio _sexoRepositorio;
        public SexoController(ISexoRepositorio sexoRepositorio)
        {
            _sexoRepositorio = sexoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sexo>>> PegarTodos()
        {
            var sexoTipo = await _sexoRepositorio.PegarTodos().ToListAsync();
            return sexoTipo;
        }     
    }
}
