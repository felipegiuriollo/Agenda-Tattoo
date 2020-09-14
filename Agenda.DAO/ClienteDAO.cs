using Agenda.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DAO
{
    public class ClienteDAO
    {
        public int Incluir(ClienteDTO dto)
        {
            using (MySqlConnection conexao = new MySqlConnection())
            {
                conexao.ConnectionString = Properties.Settings.Default.banco;
                MySqlCommand comando = new MySqlCommand();
                comando.CommandType = CommandType.Text;
                conexao.Open();

                comando.CommandText = "INSERT INTO cliente (nome_cliente, email_cliente, idade_cliente, telefone_cliente, dataCadastro_cliente) values(?,?,?,?,?)";

                comando.Parameters.Add("nome_cliente", MySqlDbType.VarChar).Value = dto.Nome;
                comando.Parameters.Add("email_cliente", MySqlDbType.VarChar).Value = dto.Email;
                comando.Parameters.Add("idade_cliente", MySqlDbType.Int32).Value = dto.Idade;
                comando.Parameters.Add("telefone_cliente", MySqlDbType.Int32).Value = dto.Telefone;
                comando.Parameters.Add("dataCadastro_cliente", MySqlDbType.DateTime).Value = dto.DataCadastro;

                comando.Connection = conexao;

                int qtd = comando.ExecuteNonQuery();

                return qtd;      
                
            }
        }

        public List<ClienteDTO> PesquisaRapida(ClienteDTO dto)
        {
            using (MySqlConnection conexao = new MySqlConnection())
            {
                conexao.ConnectionString = Properties.Settings.Default.banco;

                MySqlCommand comando = new MySqlCommand();
                comando.CommandType = CommandType.Text;
                conexao.Open();

                comando.CommandText = "SELECT id_cliente, nome_cliente, email_cliente, idade_cliente, telefone_cliente, dataCadastro_cliente FROM cliente WHERE nome_cliente LIKE ?";
                comando.Parameters.Add("nome_cliente", MySqlDbType.VarChar).Value = "%" + dto.Nome + "%";

                comando.Connection = conexao;

                MySqlDataReader dr;
                List<ClienteDTO> lista = new List<ClienteDTO>();

                dr = comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ClienteDTO dados = new ClienteDTO();
                        dados.Id = Convert.ToInt32(dr["id_cliente"]);
                        dados.Nome = Convert.ToString(dr["nome_cliente"]);
                        dados.Email = Convert.ToString(dr["email_cliente"]);
                        dados.Idade = Convert.ToInt32(dr["idade_cliente"]);
                        dados.Telefone = Convert.ToInt32(dr["telefone_cliente"]);
                        dados.DataCadastro = Convert.ToDateTime(dr["dataCadastro_cliente"]);

                        lista.Add(dados);
                    }
                }

                return lista;

            }
        }

        public int Excluir(ClienteDTO dto)
        {
            using (MySqlConnection conexao = new MySqlConnection())
            {
                conexao.ConnectionString = Properties.Settings.Default.banco;
                MySqlCommand comando = new MySqlCommand();
                comando.CommandType = CommandType.Text;
                conexao.Open();

                comando.CommandText = "DELETE FROM cliente WHERE id_cliente =?";

                comando.Parameters.Add("id_cliente", MySqlDbType.Int32).Value = dto.Id;

                comando.Connection = conexao;

                int qtd = comando.ExecuteNonQuery();
                return qtd;
            }
        }

        public int EditarUsuario(ClienteDTO dto)
        {
            using (MySqlConnection conexao = new MySqlConnection())
            {
                conexao.ConnectionString = Properties.Settings.Default.banco;
                MySqlCommand comando = new MySqlCommand();
                comando.CommandType = CommandType.Text;
                conexao.Open();

                comando.CommandText = "UPDATE cliente SET nome_cliente = ? , email_cliente = ? , idade_cliente = ?, telefone_cliente = ? where id_cliente = ?";

                comando.Parameters.Add("nome_cliente", MySqlDbType.VarChar).Value = dto.Nome;
                comando.Parameters.Add("email_cliente", MySqlDbType.VarChar).Value = dto.Email;
                comando.Parameters.Add("idade_cliente", MySqlDbType.Int32).Value = dto.Idade;
                comando.Parameters.Add("telefone_cliente", MySqlDbType.Int32).Value = dto.Telefone;
                comando.Parameters.Add("id_cliente", MySqlDbType.Int32).Value = dto.Id;
                

                comando.Connection = conexao;

                int qtd = comando.ExecuteNonQuery();
                return qtd;

            }
        }

        public List<ClienteDTO> ListarDados()
        {
            using (MySqlConnection conexao = new MySqlConnection())
            {
                conexao.ConnectionString = Properties.Settings.Default.banco;
                MySqlCommand comando = new MySqlCommand();
                comando.CommandType = CommandType.Text;
                conexao.Open();

                comando.CommandText = "SELECT id_cliente, nome_cliente, email_cliente, idade_cliente, telefone_cliente, dataCadastro_cliente FROM cliente ORDER BY id_cliente desc";
                comando.Connection = conexao;

                MySqlDataReader dr;
                List<ClienteDTO> lista = new List<ClienteDTO>();
                dr = comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ClienteDTO dados = new ClienteDTO();
                        dados.Id = Convert.ToInt32(dr["id_cliente"]);
                        dados.Nome = Convert.ToString(dr["nome_cliente"]);
                        dados.Email = Convert.ToString(dr["email_cliente"]);
                        dados.Idade = Convert.ToInt32(dr["idade_cliente"]);
                        dados.Telefone = Convert.ToInt32(dr["telefone_cliente"]);
                        dados.DataCadastro = Convert.ToDateTime(dr["dataCadastro_cliente"]);

                        lista.Add(dados);
                    }
                }

                return lista;
            }
        }
    }
}
