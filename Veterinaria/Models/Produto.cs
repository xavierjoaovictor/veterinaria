using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class Produto
    {
        public int IdProduto { get; set; } 
        public String Nome { get; set; } 
        public String Descricao { get; set; } 
        public Double Valor { get; set; }
        public int Qtd_Estoque { get; set; }
    }
}