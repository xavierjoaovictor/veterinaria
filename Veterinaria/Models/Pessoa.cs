using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class Pessoa
    {
        public int Id{get;set;}
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public int? Numero { get; set; }
        public string Bairro { get; set; }
        public Cidade Cidade { get; set; }
        public Cliente Cliente { get; set; }
        public Funcionario Funcionario { get; set; }

    }
}