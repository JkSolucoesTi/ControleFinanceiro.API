﻿using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
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
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaRepositorio.PegarTodos().ToListAsync();

            return categorias;
        }

        // GET api/<ValuesController>/5
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

        // POST api/<ValuesController>
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

        // PUT api/<ValuesController>/5
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

        // DELETE api/<ValuesController>/5
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

        [HttpGet("FiltrarCategoria/{nomeCategoria}")]

        public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategoria(string nomeCategoria)
        {
            return await _categoriaRepositorio.FiltrarCategoria(nomeCategoria).ToListAsync();
        }
    }
}