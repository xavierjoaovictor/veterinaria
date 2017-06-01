using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;
using System.Data;

namespace Veterinaria.DAO
{
    public class Connection : IConnection, IDisposable
    {
        private MySqlConnection connection;

        public Connection()
        {
            this.connection = new MySqlConnection("Persist Security Info=False; server=localhost; database=db_psi; uid=root; server=localhost; database=db_psi; uid=root; pwd=root;");
        }

        public void Close()
        {
            if (this.connection.State == ConnectionState.Open)
                this.connection.Clone();
        }

        public void Dispose()
        {
            this.Close();
            GC.SuppressFinalize(this);
        }

        public MySqlConnection Open()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();  
            return connection;
        }

        public MySqlConnection Search()
        {
            return this.Open();
        }
    }
}