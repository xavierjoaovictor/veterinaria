using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace Veterinaria.DAO
{
    public interface IConnection
    {
        MySqlConnection Open();
        MySqlConnection Search();
        void Close();
    }
}
