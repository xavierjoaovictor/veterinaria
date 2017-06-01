using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using Veterinaria.Models;

namespace Veterinaria.DAO
{
    public class EstadoDAO : IDAO<Estado>, IDisposable
    {
        private IConnection connection;
        private MySqlCommand command;

        public EstadoDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int Insert(Estado model)
        {
            return 0;
        }

        public bool Update(Estado model)
        {
            return false;
        }

        public bool Delete(Estado model)
        {      
            return false;
        }

        public Estado Search(Estado model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandTimeout = int.MaxValue;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from estado where idestado = @id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        model = new Estado();
                        reader.Read();
                        model.Id = reader.GetInt32(0);
                        model.Nome = reader.GetString(1);
                        model.UF = reader.GetString(2);
                    }
                    else
                        model = null;
                }
            }
            return model;
        }

        public List<Estado> ListAll()
        {
            var collection = new List<Estado>();

            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}