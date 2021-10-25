using System.Collections.Generic;

namespace ControleFinanceiro.BLL.Models
{
    public class Sexo
    {
        public int SexoId { get; set; }
        public string Tipo { get; set; }     
        public virtual ICollection<Cadastro> Cadastros { get; set; }

    }
}