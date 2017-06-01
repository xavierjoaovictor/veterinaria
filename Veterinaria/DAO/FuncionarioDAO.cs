using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using Veterinaria.Models;
using Veterinaria.Models.Enums;

namespace Veterinaria.DAO
{
    public class FuncionarioDAO : IDAO<Funcionario>, IDisposable
    {
        private IConnection connection;
        private MySqlCommand command;

        public FuncionarioDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int DeleteAll()
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from funcionario;";

                return this.command.ExecuteNonQuery();
            }
        }

        public int Insert(Funcionario model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "insert into funcionario (idfuncionario, numero_contrato, salario, setor, data_admissao, numero_crmv) " +
                                               "values (@idfuncionario, @numero_contrato, @salario, @setor, @data_admissao, @numero_crmv);";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@idfuncionario", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@idfuncionario", null);

                    this.command.Parameters.AddWithValue("@numero_contrato", model.NumeroContrato);
                    this.command.Parameters.AddWithValue("@salario", model.Salario);
                    this.command.Parameters.AddWithValue("@setor", (int)model.Funcao);
                    this.command.Parameters.AddWithValue("@data_admissao", model.DataAdmisao);
                    this.command.Parameters.AddWithValue("@numero_crmv", model.NumeroCRMV);

                    if (this.command.ExecuteNonQuery() > 0)
                        return (int)this.command.LastInsertedId;
                    else
                        return this.command.ExecuteNonQuery();
                }
            }            
            catch (MySqlException) { return -1; }
        }

        public bool Update(Funcionario model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "update funcionario SET numero_contrato=@numero_contrato, salario=@salario, "+
                                               "setor=@setor, data_admissao=@data_admissao, numero_crmv=@numero_crmv " +
                                               "where idfuncionario=@id;";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@numero_contrato", model.NumeroContrato);
                    this.command.Parameters.AddWithValue("@salario", model.Salario);
                    this.command.Parameters.AddWithValue("@setor", (int)model.Funcao);
                    this.command.Parameters.AddWithValue("@data_admissao", model.DataAdmisao);
                    this.command.Parameters.AddWithValue("@numero_crmv", model.NumeroCRMV);

                    if (this.command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException) { return false; }
        }

        public bool Delete(Funcionario model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "delete from funcionario where idfuncionario=@id;";

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

        public Funcionario Search(Funcionario model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from funcionario where idfuncionario=@id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                       model = new Funcionario();
                       reader.Read();
                       if (reader.GetValue(0) != null) model.Id = reader.GetInt32(0);
                       if (reader.GetValue(1) != null) model.NumeroContrato = reader.GetString(1);
                       if (reader.GetValue(2) != null) model.Salario = reader.GetDouble(2);
                       if (reader.GetValue(3) != null) model.Funcao = (FuncaoFuncionario)reader.GetInt16(3);
                       if (reader.GetValue(4) != null) model.DataAdmisao = reader.GetDateTime(4);
                       if (reader.GetValue(5) != null) model.NumeroCRMV = reader.GetString(5);
                    }
                    else
                        model = null;
                }
            }
            return model;
        }

        public List<Funcionario> ListAll()
        {
            var collection = new List<Funcionario>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select idfuncionario, numero_contrato, salario, " 
                                         + "setor, data_admissao, numero_crmv from funcionario order by idfuncionario;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var funcionario = new Funcionario
                        {
                            Id = int.Parse(row["idfuncionario"].ToString()),
                            NumeroContrato = row["numero_contrato"].ToString(),
                            Salario = double.Parse(row["salario"].ToString()),
                            Funcao = (FuncaoFuncionario)row["setor"],
                            DataAdmisao = (DateTime)row["data_admissao"],
                            NumeroCRMV = row["numero_crmv"].ToString()
                        };
                        collection.Add(funcionario);
                    }
                }
            }
            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}