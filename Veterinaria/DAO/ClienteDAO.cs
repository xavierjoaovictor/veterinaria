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
    public class ClienteDAO : IDAO<Cliente>, IDisposable
    {
        private IConnection connection;
        private MySqlCommand command;

        public ClienteDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int DeleteAll()
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from cliente;";

                return this.command.ExecuteNonQuery();
            }
        }

        public int Insert(Cliente model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandTimeout = int.MaxValue;
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "insert into cliente (idcliente, email) values (@id, @email);";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@email", model.Email);

                    if (this.command.ExecuteNonQuery() > 0)
                        return (int)this.command.LastInsertedId;
                    else
                        return this.command.ExecuteNonQuery();
                }
            }
            catch (MySqlException) { return -1; }
        }

        public bool Update(Cliente model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandTimeout = int.MaxValue;
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "update cliente set idcliente=@id, email=@email where idcliente=@id;";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@email", model.Email);

                    if (this.command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException) { return false; }
        }

        public bool Delete(Cliente model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandTimeout = int.MaxValue;
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "delete from cliente where idcliente=@id";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);

                    if (this.command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException) { return false; }
        }

        public Cliente Search(Cliente model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandTimeout = int.MaxValue;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select idcliente, email from cliente where idcliente=@id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        model = new Cliente();
                        reader.Read();
                        model.Id = reader.GetInt32(0);
                        model.Email = reader.GetString(1);
                    }
                    else
                        model = null;
                }
            }
            return model;
        }


        public List<Cliente> ListAll()
        {
            var collection = new List<Cliente>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandTimeout = int.MaxValue;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select idcliente, email from cliente order by idcliente;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var cliente = new Cliente
                        {
                            Id = int.Parse(row["idcliente"].ToString()),
                            Email = row["email"].ToString()

                        };
                        collection.Add(cliente);
                    }
                }
            }
            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}