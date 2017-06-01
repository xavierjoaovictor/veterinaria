using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }
        public string Posologia { get; set; }
        public string Medicacao { get; set; }
        public string Descricao { get; set; }
    }
}