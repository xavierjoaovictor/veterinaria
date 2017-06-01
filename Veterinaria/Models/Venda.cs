using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class Venda
    {
        public int IdVenda { get; set; }
        public DateTime Data { get; set; }
        public Double Valor_Total { get; set; }
        public int Forma_pgto { get; set; }
        public int Cliente_IdCliente { get; set; }
        public int Funcionario_IdFuncionario { get; set; }
        public int Quantidade { get; set; }
    }
}