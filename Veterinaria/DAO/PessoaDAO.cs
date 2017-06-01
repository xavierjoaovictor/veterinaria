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
    public class PessoaDAO : IDAO<Pessoa>, IDisposable
    {
        private IConnection connection;
        private MySqlCommand command;

        public PessoaDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int DeleteAll()
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandTimeout = int.MaxValue;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from pessoa;";

                return this.command.ExecuteNonQuery();
            }
        }

        public int Insert(Pessoa model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandTimeout = int.MaxValue;
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "insert into pessoa (idpessoa, nome, cpf, data_nascimento, telefone_fixo, " +
                                               "telefone_celular, endereco, complemento, numero, bairro, cliente_idcliente, funcionario_idfuncionario, " +
                                               "cidade_idcidade) values (@idpessoa, @nome, @cpf, @data_nascimento, @telefone_fixo, @telefone_celular, @endereco, @complemento, @numero, " +
                                               "@bairro, @cliente_idcliente, @funcionario_idfuncionario, @cidade_idcidade);";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@idpessoa", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@idpessoa", null);

                    this.command.Parameters.AddWithValue("@nome", model.Nome);
                    this.command.Parameters.AddWithValue("@cpf", model.Cpf);
                    this.command.Parameters.AddWithValue("@data_nascimento", model.DataNascimento);
                    this.command.Parameters.AddWithValue("@telefone_fixo", model.TelefoneFixo);
                    this.command.Parameters.AddWithValue("@telefone_celular", model.TelefoneCelular);
                    this.command.Parameters.AddWithValue("@endereco", model.Endereco);
                    this.command.Parameters.AddWithValue("@complemento", model.Complemento);
                    this.command.Parameters.AddWithValue("@numero", model.Numero);
                    this.command.Parameters.AddWithValue("@bairro", model.Bairro);
                    this.command.Parameters.AddWithValue("@funcionario_idfuncionario", model.Funcionario?.Id);
                    this.command.Parameters.AddWithValue("@cliente_idcliente", model.Cliente?.Id);
                    this.command.Parameters.AddWithValue("@cidade_idcidade", model.Cidade.Id);

                    if (this.command.ExecuteNonQuery() > 0)
                        return (int)this.command.LastInsertedId;
                    else
                        return this.command.ExecuteNonQuery();
                }
            }
            catch (MySqlException) { return -1; }
        }

        public bool Update(Pessoa model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandTimeout = int.MaxValue;
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "update pessoa set nome=@nome, cpf=@cpf, " +
                                               "data_nascimento=@data_nascimento, telefone_fixo=@telefone_fixo, telefone_celular=@telefone_celular, " +
                                               "endereco=@endereco, complemento=@complemento, numero=@numero, bairro=@bairro, cliente_idcliente=@cliente_idcliente, " + 
                                               "funcionario_idfuncionario=@funcionario_idfuncionario, cidade_idcidade=@cidade_idcidade " +
                                               "where idpessoa=@id;";

                    if (model.Id > 0)
                        this.command.Parameters.AddWithValue("@id", model.Id);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@nome", model.Nome);
                    this.command.Parameters.AddWithValue("@cpf", model.Cpf);
                    this.command.Parameters.AddWithValue("@data_nascimento", model.DataNascimento);
                    this.command.Parameters.AddWithValue("@telefone_fixo", model.TelefoneFixo);
                    this.command.Parameters.AddWithValue("@telefone_celular", model.TelefoneCelular);
                    this.command.Parameters.AddWithValue("@endereco", model.Endereco);
                    this.command.Parameters.AddWithValue("@complemento", model.Complemento);
                    this.command.Parameters.AddWithValue("@numero", model.Numero);
                    this.command.Parameters.AddWithValue("@bairro", model.Bairro);
                    this.command.Parameters.AddWithValue("@funcionario_idfuncionario", model.Funcionario?.Id);
                    this.command.Parameters.AddWithValue("@cliente_idcliente", model.Cliente?.Id);
                    this.command.Parameters.AddWithValue("@cidade_idcidade", model.Cidade.Id);

                    if (this.command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException) { return false; }
        }

        public bool Delete(Pessoa model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandTimeout = int.MaxValue;
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "delete from pessoa where idpessoa=@id;";

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

        public Pessoa Search(Pessoa model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandTimeout = int.MaxValue;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from pessoa where 1=1 ";
                if (model.Id > 0)
                {
                    this.command.CommandText += "and idpessoa=@id ";
                    this.command.Parameters.AddWithValue("@id", model.Id);
                }

                if(!String.IsNullOrEmpty(model.Cpf))
                {
                    this.command.CommandText += "and cpf=@cpf ";
                    this.command.Parameters.AddWithValue("@cpf", model.Cpf);
                }
                if(model.Funcionario != null)
                {
                    this.command.CommandText += "and funcionario_idfuncionario=@idfunc ";
                    this.command.Parameters.AddWithValue("@idfunc", model.Funcionario.Id);
                }
                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        model = new Pessoa();
                        reader.Read();
                        if (reader[0] != DBNull.Value) model.Id = reader.GetInt32(0);
                        if (reader[1] != DBNull.Value) model.Nome = reader.GetString(1);
                        if (reader[2] != DBNull.Value) model.Cpf = reader.GetString(2);
                        if (reader[3] != DBNull.Value) model.DataNascimento = reader.GetDateTime(3);
                        if (reader[4] != DBNull.Value) model.TelefoneFixo = reader.GetString(4);
                        if (reader[5] != DBNull.Value) model.TelefoneCelular = reader.GetString(5);
                        if (reader[6] != DBNull.Value) model.Endereco = reader.GetString(6);
                        if (reader[7] != DBNull.Value) model.Complemento = reader.GetString(7);
                        if (reader[8] != DBNull.Value) model.Numero = reader.GetInt32(8);
                        if (reader[9] != DBNull.Value) model.Bairro = reader.GetString(9);
                        if (reader[10] != DBNull.Value)
                            model.Cliente = new ClienteDAO(new Connection()).Search(new Cliente() { Id = reader.GetInt32(10)});
                        if (reader[11] != DBNull.Value)
                            model.Funcionario = new FuncionarioDAO(new Connection()).Search(new Funcionario() { Id = reader.GetInt32(11) });
                        if (reader[12] != DBNull.Value)
                            model.Cidade = new CidadeDAO(new Connection()).Search(new Cidade() { Id = reader.GetInt32(12) });
                    }
                    else
                        model = null;
                }
            }
            return model;
        }

        public List<Pessoa> ListAll()
        {
            var collection = new List<Pessoa>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from pessoa order by nome;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var pessoa = new Pessoa
                        {
                            Id = int.Parse(row["idpessoa"].ToString()),
                            Nome = row["nome"].ToString(),
                            Cpf = row["cpf"].ToString(),
                            DataNascimento = DateTime.Parse(row["data_nascimento"].ToString()),
                            TelefoneFixo = row["telefone_fixo"].ToString(),
                            TelefoneCelular = row["telefone_celular"].ToString(),
                            Endereco = row["endereco"].ToString(),
                            Complemento = row["complemento"].ToString(),
                            Bairro = row["bairro"].ToString()
                        };

                        if (!String.IsNullOrEmpty(row["numero"].ToString()))
                            pessoa.Numero = (int?)(row["numero"]);

                        if (!String.IsNullOrEmpty(row["cliente_idcliente"].ToString()))
                            pessoa.Cliente = new ClienteDAO(new Connection()).Search(new Cliente() { Id = (int)row["cliente_idcliente"] });

                        if (!String.IsNullOrEmpty(row["funcionario_idfuncionario"].ToString()))
                            pessoa.Funcionario = new FuncionarioDAO(new Connection()).Search(new Funcionario() { Id = (int)row["funcionario_idfuncionario"] });

                        if (!String.IsNullOrEmpty(row["cidade_idcidade"].ToString()))
                            pessoa.Cidade = new CidadeDAO(new Connection()).Search(new Cidade() { Id = (int)row["cidade_idcidade"] });

                        collection.Add(pessoa);
                    }
                }
            }
            return collection;
        }

        public List<Pessoa> ListAllClientes()
        {
            var collection = new List<Pessoa>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from pessoa where cliente_idcliente is not null order by nome;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var pessoa = new Pessoa
                        {
                            Id = int.Parse(row["idpessoa"].ToString()),
                            Nome = row["nome"].ToString(),
                            Cpf = row["cpf"].ToString(),
                            DataNascimento = DateTime.Parse(row["data_nascimento"].ToString()),
                            TelefoneFixo = row["telefone_fixo"].ToString(),
                            TelefoneCelular = row["telefone_celular"].ToString(),
                            Endereco = row["endereco"].ToString(),
                            Complemento = row["complemento"].ToString(),
                            Bairro = row["bairro"].ToString()
                        };

                        if (!String.IsNullOrEmpty(row["numero"].ToString()))
                            pessoa.Numero = (int?)(row["numero"]);

                        if (!String.IsNullOrEmpty(row["cliente_idcliente"].ToString()))
                            pessoa.Cliente = new ClienteDAO(new Connection()).Search(new Cliente() { Id = (int)row["cliente_idcliente"] });

                        if (!String.IsNullOrEmpty(row["funcionario_idfuncionario"].ToString()))
                            pessoa.Funcionario = new FuncionarioDAO(new Connection()).Search(new Funcionario() { Id = (int)row["funcionario_idfuncionario"] });

                        if (!String.IsNullOrEmpty(row["cidade_idcidade"].ToString()))
                            pessoa.Cidade = new CidadeDAO(new Connection()).Search(new Cidade() { Id = (int)row["cidade_idcidade"] });

                        collection.Add(pessoa);
                    }
                }
            }
            return collection;
        }

        public List<Pessoa> ListAllFuncionarios()
        {
            var collection = new List<Pessoa>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandTimeout = int.MaxValue;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from pessoa where funcionario_idfuncionario is not null order by nome;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var pessoa = new Pessoa
                        {
                            Id = int.Parse(row["idpessoa"].ToString()),
                            Nome = row["nome"].ToString(),
                            Cpf = row["cpf"].ToString(),
                            DataNascimento = DateTime.Parse(row["data_nascimento"].ToString()),
                            TelefoneFixo = row["telefone_fixo"].ToString(),
                            TelefoneCelular = row["telefone_celular"].ToString(),
                            Endereco = row["endereco"].ToString(),
                            Complemento = row["complemento"].ToString(),
                            Bairro = row["bairro"].ToString()
                        };

                        if (!String.IsNullOrEmpty(row["numero"].ToString()))
                            pessoa.Numero = (int?)(row["numero"]);

                        if (!String.IsNullOrEmpty(row["cliente_idcliente"].ToString()))
                            pessoa.Cliente = new ClienteDAO(new Connection()).Search(new Cliente() { Id = (int)row["cliente_idcliente"] });

                        if (!String.IsNullOrEmpty(row["funcionario_idfuncionario"].ToString()))
                            pessoa.Funcionario = new FuncionarioDAO(new Connection()).Search(new Funcionario() { Id = (int)row["funcionario_idfuncionario"] });

                        if (!String.IsNullOrEmpty(row["cidade_idcidade"].ToString()))
                            pessoa.Cidade = new CidadeDAO(new Connection()).Search(new Cidade() { Id = (int)row["cidade_idcidade"] });

                        collection.Add(pessoa);
                    }
                }
            }
            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}