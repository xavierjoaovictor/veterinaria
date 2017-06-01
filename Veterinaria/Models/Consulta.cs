using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinaria.Models.Enums;

namespace Veterinaria.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public StatusConsulta Status { get; set; }
        public Pet Pet { get; set; }
        public Pessoa Atendente { get; set; }
        public Pessoa Veterinario { get; set; }
        public Diagnostico Diagnostico { get; set; }
    }
}