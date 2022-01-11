using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriasController(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }
 
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaRepositorio.PegarTodos().ToListAsync();

            return categorias;
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _categoriaRepositorio.PegarPeloId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriaRepositorio.Inserir(categoria);
                return Ok(new
                {
                    mensagem = $"Categoria {categoria.Nome} inserida com sucesso"
                });
            }
            else
            {
                return BadRequest(categoria);
            }

        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _categoriaRepositorio.Atualizar(categoria);
                return Ok(new
                {
                    mensagem = $"Categoria {categoria.Nome} atualizada com sucesso"
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoria = await _categoriaRepositorio.PegarPeloId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            await _categoriaRepositorio.Excluir(id);

            return Ok(new
            {
                mensagem = $"Categoria {categoria.Nome} excluída com sucesso"
            });
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("FiltrarCategoria/{nomeCategoria}")]

        public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategoria(string nomeCategoria)
        {
            return await _categoriaRepositorio.FiltrarCategoria(nomeCategoria).ToListAsync();
        }

        [Authorize]
        [HttpGet("FiltrarCategoriasDepesas")]
        public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategoriasDepesas()
        {
            return await _categoriaRepositorio.PegarCategoriaPeloTipo("Despesa").ToListAsync();
        }

        [Authorize]
        [HttpGet("FiltrarCategoriasGanhos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategoriasGanhos()
        {
            return await _categoriaRepositorio.PegarCategoriaPeloTipo("Ganho").ToListAsync();
        }

    }
}
