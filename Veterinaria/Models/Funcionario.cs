using System;
using Veterinaria.Models.Enums;

namespace Veterinaria.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string NumeroContrato { get; set; }
        public double Salario { get; set; }
        public DateTime DataAdmisao { get; set; }
        public FuncaoFuncionario Funcao { get; set; }
        public string NumeroCRMV { get; set;}
    }
}