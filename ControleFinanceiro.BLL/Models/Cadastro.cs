using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.BLL.Models
{
    public class Cadastro
    {
        public int CadastroId { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }            
        public int SexoId { get; set; }
        public Sexo Sexo { get; set; }
        public string DataNascimento { get; set; }
    }
}
