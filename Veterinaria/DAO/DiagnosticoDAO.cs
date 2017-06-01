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
    public class DiagnosticoDAO : IDAO<Diagnostico>, IDisposable
    {
        private IConnection connection;
        private MySqlCommand command;

        public DiagnosticoDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int DeleteAll()
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from diagnostico;";

                return this.command.ExecuteNonQuery();
            }
        }

        public int Insert(Diagnostico model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "insert into diagnostico (iddiagnostico, posologia, medicacao, "
                                             + "descricao) values (@id, @posologia, @medicacao, @descricao);";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@posologia", model.Posologia);
                    this.command.Parameters.AddWithValue("@medicacao", model.Medicacao);
                    this.command.Parameters.AddWithValue("@descricao", model.Descricao);

                    if (this.command.ExecuteNonQuery() > 0)
                        return (int)this.command.LastInsertedId;
                    else
                        return this.command.ExecuteNonQuery();
                }
            }
            catch (MySqlException) { return -1; }
        }

        public bool Update(Diagnostico model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "update diagnostico set iddiagnostico=@id, posologia=@posologia, medicacao=@medicacao, " 
                                         + "descricao=@descricao where iddiagnostico=@id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);
                this.command.Parameters.AddWithValue("@posologia", model.Posologia);
                this.command.Parameters.AddWithValue("@medicacao", model.Medicacao);
                this.command.Parameters.AddWithValue("@descricao", model.Descricao);

                if (this.command.ExecuteNonQuery() > 0)
                    return true;
            }
            return false;
        }

        public bool Delete(Diagnostico model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from diagnostico where iddiagnostico=@id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                if (this.command.ExecuteNonQuery() > 0)
                    return true;
            }
            return false;
        }

        public Diagnostico Search(Diagnostico model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from diagnostico where iddiagnostico=@id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        model = new Diagnostico();
                        reader.Read();
                        model.Id = reader.GetInt32(0);
                        model.Posologia = reader.GetString(1);
                        model.Medicacao = reader.GetString(2);
                        model.Descricao = reader.GetString(3);
                    }
                    else
                        model = null;
                }
            }
            return model;
        }

        public List<Diagnostico> ListAll()
        {
            var collection = new List<Diagnostico>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from diagnostico order by iddiagnostico;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var diagnostico = new Diagnostico
                        {
                            Id = int.Parse(row["iddiagnostico"].ToString()),
                            Posologia = row["posologia"].ToString(),
                            Medicacao = row["medicacao"].ToString(),
                            Descricao = row["descricao"].ToString()
                        };
                        collection.Add(diagnostico);
                    }
                }
            }
            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}