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
    public class ConsultaDAO : IDAO<Consulta>, IDisposable
    {
        private IConnection connection;
        private MySqlCommand command;

        public ConsultaDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int DeleteAll()
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from consulta;";
                return this.command.ExecuteNonQuery();
            }
        }

        public int Insert(Consulta model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "insert into consulta (idconsulta, data, pet_idpet, status, "
                                             + "atendente_idfuncionario, veterinario_idfuncionario1, diagnostico_iddiagnostico) " 
                                             + "values (@id, @data, @pet_id, @status, @ate_id, @vet_id, @diag_id);";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@data", model.Data);
                    this.command.Parameters.AddWithValue("@status", (int)model.Status);
                    this.command.Parameters.AddWithValue("@pet_id", model.Pet?.Id);
                    this.command.Parameters.AddWithValue("@ate_id", model.Atendente?.Funcionario.Id);
                    this.command.Parameters.AddWithValue("@vet_id", model.Veterinario?.Funcionario.Id);
                    this.command.Parameters.AddWithValue("@diag_id", null);

                    if (this.command.ExecuteNonQuery() > 0)
                        return (int)this.command.LastInsertedId;
                    else
                        return this.command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex) { return -1; }
        }

        public bool Update(Consulta model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "update consulta set data=@data, pet_idpet=@idpet, status=@status, "
                                              + "atendente_idfuncionario=@idatendente, veterinario_idfuncionario1=@idveterinario, " 
                                              + "diagnostico_iddiagnostico=@diag_id where idconsulta=@id;";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else 
                        this.command.Parameters.AddWithValue("@id", null);

                    this.command.Parameters.AddWithValue("@data", model.Data);
                    this.command.Parameters.AddWithValue("@status", (int)model.Status);
                    this.command.Parameters.AddWithValue("@idpet", model.Pet?.Id);
                    this.command.Parameters.AddWithValue("@idatendente", model.Atendente?.Funcionario?.Id);
                    this.command.Parameters.AddWithValue("@idveterinario", model.Veterinario?.Funcionario?.Id);

                    if(model.Diagnostico?.Id == -1)
                        model.Diagnostico.Id = new DiagnosticoDAO(new Connection()).Insert(model.Diagnostico);
                    else
                        new DiagnosticoDAO(new Connection()).Update(model.Diagnostico);

                    this.command.Parameters.AddWithValue("@diag_id", model.Diagnostico?.Id);

                    if (this.command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException ex) { return false; }
        }

        public bool Delete(Consulta model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from consulta where idconsulta=@id;";

                if (model.Id > 0)
                    this.command.Parameters.AddWithValue("@id", model.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                if (this.command.ExecuteNonQuery() > 0)
                    return true;
            }
            return false;
        }

        public Consulta Search(Consulta consulta)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from consulta where idconsulta=@id;";

                if (consulta.Id > 0)
                    this.command.Parameters.AddWithValue("@id", consulta.Id);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        consulta = new Consulta();
                        reader.Read();
                        if (reader[0] != DBNull.Value) consulta.Id = reader.GetInt32(0);
                        if (reader[1] != DBNull.Value) consulta.Data = reader.GetDateTime(1);
                        if (reader[2] != DBNull.Value) consulta.Status = (StatusConsulta)reader.GetInt16(2);
                        if (reader[3] != DBNull.Value) consulta.Pet = new PetDAO(new Connection()).Search(new Pet() { Id = reader.GetInt32(3) });
                        if (reader[4] != DBNull.Value) consulta.Atendente = new PessoaDAO(new Connection())
                                                               .Search(new Pessoa() {Funcionario = new Funcionario() { Id = reader.GetInt32(4) } });
                        if (reader[5] != DBNull.Value) consulta.Veterinario = new PessoaDAO(new Connection())
                                                               .Search(new Pessoa() { Funcionario = new Funcionario() { Id = reader.GetInt32(5) } });
                        if (reader[6] != DBNull.Value)
                            consulta.Diagnostico = new DiagnosticoDAO(new Connection())
                                    .Search(new Diagnostico() { Id = reader.GetInt32(6) });
                        else
                            consulta.Diagnostico = new Diagnostico() { Id = -1 };
                    }
                    else
                        consulta = null;
                }
            }
            return consulta;
        }

        public List<Consulta> ListAll()
        {
            var collection = new List<Consulta>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from consulta order by data;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var consulta = new Consulta
                        {
                            Id = int.Parse(row["idconsulta"].ToString()),
                            Data = DateTime.Parse(row["data"].ToString()),
                            Status = (StatusConsulta)int.Parse(row["status"].ToString())
                    };

                        if (!String.IsNullOrEmpty(row["pet_idpet"].ToString()))
                            consulta.Pet = new PetDAO(new Connection()).Search(new Pet() { Id = (int)row["pet_idpet"] });

                        if (!String.IsNullOrEmpty(row["atendente_idfuncionario"].ToString()))
                            consulta.Atendente = new PessoaDAO(new Connection())
                                    .Search(new Pessoa() { Funcionario = new Funcionario() {
                                                           Id = int.Parse(row["atendente_idfuncionario"].ToString()) } });

                        if (!String.IsNullOrEmpty(row["veterinario_idfuncionario1"].ToString()))
                            consulta.Veterinario = new PessoaDAO(new Connection())
                                    .Search(new Pessoa() { Funcionario = new Funcionario() {
                                                           Id = int.Parse(row["veterinario_idfuncionario1"].ToString()) } });

                        if (!String.IsNullOrEmpty(row["diagnostico_iddiagnostico"].ToString()))
                            consulta.Diagnostico = new DiagnosticoDAO(new Connection())
                                    .Search( new Diagnostico() { Id = int.Parse(row["diagnostico_iddiagnostico"].ToString()) });

                        collection.Add(consulta);
                    }
                }
            }
            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}