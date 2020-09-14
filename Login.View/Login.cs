using Agenda.DTO;
using Agenda.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.View
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //Variáveis locais

        #region Fechar sistema
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Minimizar sistema
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion


        //Botões

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtLogin.Text == "" && txtSenha.Text == "")
                {
                    MessageBox.Show("Campos usuário e senha vazios!");
                    txtLogin.Focus();
                }

                else if (txtLogin.Text == "")
                {
                    MessageBox.Show("Campo Usuário vazio!");
                    txtLogin.Focus();
                }

                else if (txtSenha.Text == "")
                {
                    MessageBox.Show("Campo Senha vazio!");
                    txtSenha.Focus();
                }

                else
                {
                    UsuarioDTO dto = new UsuarioDTO();
                    dto.Login = txtLogin.Text;
                    dto.Senha = txtSenha.Text;

                    dto = new UsuarioModel().VerificarLogin(dto);

                    if (dto.Login == null)
                    {

                        MessageBox.Show("Usuário ou Senha inválido!", "ALERTA DO SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        frmMenu view = new frmMenu();
                        this.Hide();
                        view.Closed += (s, args) => this.Close();
                        view.Show();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao logar! " + ex.Message);
            }

        }

     
        //Métodos e Funções

    }
}
