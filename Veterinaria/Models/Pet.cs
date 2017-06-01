using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veterinaria.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Raca { get; set; }
        public int Sexo { get; set; }
        public int Tipo { get; set; }
        public Pessoa Cliente { get; set; }
        public IEnumerable<SelectListItem> ListaClientes { get; set; }
    }
}