using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veterinaria
{
    public static class Util
    {
        public static string RemoverCaracters(string valor)
        {
            if (!String.IsNullOrEmpty(valor))
            {
                valor = valor.Replace(".", "");
                valor = valor.Replace(",", "");
                valor = valor.Replace("/", "");
                valor = valor.Replace("_", "");
                valor = valor.Replace("-", "");
                valor = valor.Replace("\\", "");
                valor = valor.Replace("|", "");
                valor = valor.Replace("\"", "");
                valor = valor.Replace("\'", "");
                valor = valor.Replace("~", "");
                valor = valor.Replace("^", "");
                valor = valor.Replace(":", "");
                valor = valor.Replace(";", "");
                valor = valor.Replace("(", "");
                valor = valor.Replace(")", "");
                valor = valor.Replace("[", "");
                valor = valor.Replace("]", "");
                valor = valor.Replace("{", "");
                valor = valor.Replace("}", "");
                valor = valor.Replace(" ", "");
            }
            return valor;
        }
    }
}