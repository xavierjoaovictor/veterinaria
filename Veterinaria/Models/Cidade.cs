using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        private string nome;
        public string Nome
        {
            get
            {
                if (this.Estado != null)
                    return this.nome + " - " + this.Estado.UF;
                else
                    return this.nome;
            }
            set { this.nome = value; }
        }
        public Estado Estado { get; set; }
    }
}