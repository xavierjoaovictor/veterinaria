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
    public class CidadeDAO : IDAO<Cidade>, IDisposable
    {
        private IConnection connection;
        private MySqlCommand command;

        public CidadeDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int Insert(Cidade model)
        {
            return 0;
        }

        public bool Update(Cidade model)
        {
            return false;
        }

        public bool Delete(Cidade model)
        {        
            return false;
        }

        public Cidade Search(Cidade model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandTimeout = int.MaxValue;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from cidade where idcidade = @id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        model = new Cidade();
                        reader.Read();
                        model.Id = reader.GetInt32(0);
                        model.Nome = reader.GetString(1);

                        if (reader[2] != DBNull.Value)
                            model.Estado = new EstadoDAO(new Connection()).Search(new Estado() { Id = reader.GetInt32(2) });
                    }
                    else
                        model = null;
                }
            }
            return model;
        }

        public List<Cidade> ListAll()
        {
            var collection = new List<Cidade>();
  
            return collection;
        }

        public List<Cidade> ListFiltro(string filtro)
        {
            var collection = new List<Cidade>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandTimeout = int.MaxValue;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from cidade where nome_cidade like @nome '%';";
                this.command.Parameters.AddWithValue("@nome", filtro);

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var model = new Cidade();
                        model.Id = int.Parse(row["idcidade"].ToString());
                        model.Nome = row["nome_cidade"].ToString();

                        if (!String.IsNullOrEmpty(row["estado_idestado"].ToString()))
                            model.Estado = new EstadoDAO(new Connection()).Search(new Estado() { Id = int.Parse(row["estado_idestado"].ToString()) });

                        collection.Add(model);
                    }
                }
            }
            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}