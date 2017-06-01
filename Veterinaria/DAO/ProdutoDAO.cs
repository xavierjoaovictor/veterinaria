using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Veterinaria.Models;

namespace Veterinaria.DAO
{
    public class ProdutoDAO
    {
        private IConnection connection;
        private MySqlCommand command;

        public ProdutoDAO(IConnection connection)
        {
            this.connection = connection;
        }

        public int DeleteAll()
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "delete from produto;";

                return this.command.ExecuteNonQuery();
            }
        }

        public int Insert(Produto model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "insert into produto (idproduto, nome, descricao, valor, qtd_estoque) " 
                                             + "values (@id, @nome, @descricao, @valor, @qtd_estoque);";

                    if (model.IdProduto > 0)
                        this.command.Parameters.AddWithValue("@id", model.IdProduto);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@nome", model.Nome);
                    this.command.Parameters.AddWithValue("@descricao", model.Descricao);
                    this.command.Parameters.AddWithValue("@valor", model.Valor);
                    this.command.Parameters.AddWithValue("@qtd_estoque", model.Qtd_Estoque);

                    if (this.command.ExecuteNonQuery() > 0)
                        return (int)this.command.LastInsertedId;
                    else
                        return this.command.ExecuteNonQuery();
                }
            }
            catch (MySqlException) { return -1; }
        }

        public bool Update(Produto model)
        {
            try
            {
                using (this.command = connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "update produto set idproduto=@id, nome=@nome, descricao=@descricao, " 
                                             + "valor=@valor, qtd_estoque=@qtd_estoque where idproduto=@id;";

                    if (model.IdProduto > 0)
                        this.command.Parameters.AddWithValue("@id", model.IdProduto);
                    else
                        this.command.Parameters.AddWithValue("@id", null);
                    this.command.Parameters.AddWithValue("@nome", model.Nome);
                    this.command.Parameters.AddWithValue("@descricao", model.Descricao);
                    this.command.Parameters.AddWithValue("@valor", model.Valor);
                    this.command.Parameters.AddWithValue("@qtd_estoque", model.Qtd_Estoque);

                    if (this.command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException) { return false; }
        }

        public bool Delete(Produto model)
        {
            try
            {
                using (this.command = this.connection.Search().CreateCommand())
                {
                    this.command.CommandType = CommandType.Text;
                    this.command.CommandText = "delete from produto where idproduto=@id;";

                    if (model.IdProduto > 0)
                        this.command.Parameters.AddWithValue("@id", model.IdProduto);
                    else
                        this.command.Parameters.AddWithValue("@id", null);

                    if (this.command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException) { return false; }
        }

        public Produto Search(Produto model)
        {
            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from produto where idproduto=@id;";

                if (model.IdProduto > 0)
                    this.command.Parameters.AddWithValue("@id", model.IdProduto);
                else
                    this.command.Parameters.AddWithValue("@id", null);

                using (MySqlDataReader reader = this.command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        model = new Produto();
                        reader.Read();
                        model.IdProduto = reader.GetInt32(0);
                        model.Nome = reader.GetString(1);
                        model.Descricao = reader.GetString(2);
                        model.Valor = reader.GetDouble(3);
                        model.Qtd_Estoque = reader.GetInt32(4);
                    }
                    else
                        model = null;
                }
            }
            return model;
        }

        public List<Produto> ListAll()
        {
            var collection = new List<Produto>();

            using (this.command = this.connection.Search().CreateCommand())
            {
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = "select * from produto order by idproduto;";

                using (var adapter = new MySqlDataAdapter(this.command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var Produto = new Produto
                        {
                            IdProduto = int.Parse(row["idproduto"].ToString()),
                            Nome = row["nome"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Valor = double.Parse(row["valor"].ToString()),
                            Qtd_Estoque = int.Parse(row["qtd_estoque"].ToString())
                        };
                        collection.Add(Produto);
                    }
                }
            }
            return collection;
        }

        public void Dispose() { GC.SuppressFinalize(this); }
    }
}