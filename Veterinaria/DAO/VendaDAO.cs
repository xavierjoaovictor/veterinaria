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
    public class VendaDAO : IDAO<Venda>, IDisposable
    {
        private IConnection connection;

        public int? Insert(Venda model)
        {
            try
            {
                using (MySqlCommand command = connection.Search().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into venda (idvenda, data, valor_total, forma_pgto, cliente_idcliente, funcionario_idfuncionario) VALUES (NULL, @data, @valor_total, @forma_pgto, @cliente_idcliente, @funcionario_idfuncionario);";
                    
                    command.Parameters.AddWithValue("@idvenda", model.IdVenda);
                    command.Parameters.AddWithValue("@data", model.Data);
                    command.Parameters.AddWithValue("@valor_total", model.Valor_Total);
                    command.Parameters.AddWithValue("@forma_pgto", model.Forma_pgto);
                    command.Parameters.AddWithValue("@cliente_idcliente", model.Cliente_IdCliente);
                    command.Parameters.AddWithValue("@funcionario_idfuncionario", model.Funcionario_IdFuncionario);
                    command.Parameters.AddWithValue("@quantidade", model.Quantidade);

                    if (command.ExecuteNonQuery() > 0)
                        return (int)command.LastInsertedId;
                }
                return null;
            }
            catch (MySqlException)
            {
                return null;
            }
        }

        public bool Update(Venda model)
        {
            try
            {
                using (MySqlCommand command = connection.Search().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update venda set data=@data, valor_total=@valor_total, forma_pgto=@forma_pgto, cliente_idcliente=@cliente_idcliente, funcionario_idfuncionario=@funcionario_idfuncionario where idvenda=@idvenda;";
                    
                    command.Parameters.AddWithValue("@idvenda", model.IdVenda);
                    command.Parameters.AddWithValue("@data", model.Data);
                    command.Parameters.AddWithValue("@valor_total", model.Valor_Total);
                    command.Parameters.AddWithValue("@forma_pgto", model.Forma_pgto);
                    command.Parameters.AddWithValue("@cliente_idcliente", model.Cliente_IdCliente);
                    command.Parameters.AddWithValue("@funcionario_idfuncionario", model.Funcionario_IdFuncionario);

                    if (command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public bool Delete(Venda model)
        {
            try
            {
                using (MySqlCommand command = connection.Search().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "delete from venda where idvenda=@idvenda;";

                    command.Parameters.AddWithValue("@idvenda", model.IdVenda);

                    if (command.ExecuteNonQuery() > 0)
                        return true;
                }
                return false;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public Venda Search(Venda venda)
        {
            using (MySqlCommand command = connection.Search().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "select idvenda, data, valor_total, forma_pgto, cliente_idcliente, funcionario_idfuncionario from venda where idvenda=@id;";                
                command.Parameters.AddWithValue("@id",venda.IdVenda);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Venda venda = new Venda();
                        reader.Read();
                        
                        venda.IdVenda = reader.GetInt32(0);
                        venda.Data = reader.GetDateTime(1);
                        venda.Valor_Total = reader.GetDouble(2);
                        venda.Forma_pgto = reader.GetInt32(3);
                        venda.Cliente_IdCliente = reader.GetInt32(4);
                        venda.Funcionario_IdFuncionario = reader.GetInt32(5);
                    }
                    else
                        venda = null;
                }
            }
            return venda;
        }

        public List<Venda> ListAll()
        {
            List<venda> collection = new List<venda>();

            using (MySqlCommand command = connection.Search().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "select idvenda, data, valor_total, forma_pgto, cliente_idcliente, funcionario_idfuncionario from venda order by idvenda;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        Venda venda = new Venda
                        {
                            IdVenda = int.Parse(row["idvenda"].ToString()),
                            Data = row["data"].ToString(),
                            Valor_Total = double.Parse(row["valor_total"]),
                            Forma_pgto = int.Parse(row["forma_pgto"].ToString()),
                            Cliente_IdCliente = int.Parse(row["cliente_idcliente"].ToString()),
                            Funcionario_IdFuncionario = int.Parse(row["funcionario_idfuncionario"].ToString())

                        };
                        collection.Add(vanda);
                    }
                }
            }
            return collection;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}