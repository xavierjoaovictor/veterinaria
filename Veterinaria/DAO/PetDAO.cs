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
    public class PetDAO : IDAO<Pet>, IDisposable
    {
        private IConnection connection;
        private MySqlCommand command;

        public PetDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int DeleteAll()
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from pet;";

                return this.command.ExecuteNonQuery();
            }
        }

        public int Insert(Pet model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "insert into pet (idpet, nome, data_nascimento, raca, sexo, tipo, " 
                                             + " cliente_idcliente) values (@id, @nome, @data_nascimento, "
                                             + " @raca, @sexo, @tipo, @idcliente);";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@nome", model.Nome);
                    this.command.Parameters.AddWithValue("@data_nascimento", model.DataNascimento);
                    this.command.Parameters.AddWithValue("@raca", model.Raca);
                    this.command.Parameters.AddWithValue("@sexo", model.Sexo);
                    this.command.Parameters.AddWithValue("@tipo", model.Tipo);
                    this.command.Parameters.AddWithValue("@idcliente", model.Cliente?.Cliente?.Id);

                    if (this.command.ExecuteNonQuery() > 0)
                        return (int)this.command.LastInsertedId;
                    else
                        return this.command.ExecuteNonQuery();
                }
            }
            catch (MySqlException) { return -1; }
        }

        public bool Update(Pet model)
        {
            try
            {
                using (this.command = connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "update pet set nome=@nome, data_nascimento=@data_nascimento, "
                                             + "raca=@raca, sexo=@sexo, tipo=@tipo, cliente_idcliente=@idcliente where idpet=@id;";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@nome", model.Nome);
                    this.command.Parameters.AddWithValue("@data_nascimento", model.DataNascimento);
                    this.command.Parameters.AddWithValue("@raca", model.Raca);
                    this.command.Parameters.AddWithValue("@sexo", model.Sexo);
                    this.command.Parameters.AddWithValue("@tipo", model.Tipo);
                    this.command.Parameters.AddWithValue("@idcliente", model.Cliente?.Cliente?.Id);

                    if (this.command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException) { return false; }
        }

        public bool Delete(Pet model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "delete from pet where idpet=@id;";

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

        public Pet Search(Pet model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from pet where idpet=@id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        model = new Pet();
                        reader.Read();
                        if (reader[0] != DBNull.Value) model.Id = reader.GetInt32(0);
                        if (reader[1] != DBNull.Value) model.Nome = reader.GetString(1);
                        if (reader[2] != DBNull.Value) model.DataNascimento = reader.GetDateTime(2);
                        if (reader[3] != DBNull.Value) model.Raca = reader.GetString(3);
                        if (reader[4] != DBNull.Value) model.Sexo = reader.GetInt32(4);
                        if (reader[5] != DBNull.Value) model.Tipo = reader.GetInt32(5);
                        if (reader[6] != DBNull.Value) model.Cliente = new PessoaDAO(new Connection())
                            .ListAllClientes()
                            .Where(x => x.Cliente.Id == reader.GetInt32(6))
                            .First();
                    }
                    else
                        model = null;
                }
            }
            return model;
        }

        public List<Pet> ListAll()
        {
            var collection = new List<Pet>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from pet order by idpet;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var pet = new Pet
                        {
                            Id = int.Parse(row["idpet"].ToString()),
                            Nome = row["nome"].ToString(),
                            DataNascimento = DateTime.Parse(row["data_nascimento"].ToString()),
                            Raca = row["raca"].ToString(),
                            Sexo = int.Parse(row["sexo"].ToString()),
                            Tipo = int.Parse(row["tipo"].ToString())
                        };

                        if (!String.IsNullOrEmpty(row["cliente_idcliente"].ToString()))
                        {
                            pet.Cliente = new PessoaDAO(new Connection())
                                .ListAllClientes()
                                .Where(pessoa => pessoa.Cliente.Id == (int)row["cliente_idcliente"])
                                .First(); 
                        }

                        collection.Add(pet);
                    }
                }
            }
            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}