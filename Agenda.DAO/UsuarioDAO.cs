using Agenda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Agenda.DAO
{
    public class UsuarioDAO
    {
        public UsuarioDTO VerificarLogin(UsuarioDTO dto)
        {
            using (MySqlConnection conexao = new MySqlConnection())
            {
                conexao.ConnectionString = Properties.Settings.Default.banco;

                MySqlCommand comando = new MySqlCommand();

                comando.CommandType = CommandType.Text;

                conexao.Open();

                comando.CommandText = "SELECT login_usuario, senha_usuario FROM usuario WHERE login_usuario = ? and senha_usuario = ?";
                comando.Connection = conexao;

                comando.Parameters.Add("login_usuario", MySqlDbType.VarChar).Value = dto.Login;
                comando.Parameters.Add("senha_usuario", MySqlDbType.VarChar).Value = dto.Senha;

                MySqlDataReader dr;
                dr = comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UsuarioDTO dados = new UsuarioDTO();
                        dados.Login = Convert.ToString(dr["login_usuario"]);
                        dados.Senha = Convert.ToString(dr["senha_usuario"]);
                    }
                }

                else
                {
                    dto.Login = null;
                    dto.Senha = null;
                }

                return dto;
            }
        }
    }
}
