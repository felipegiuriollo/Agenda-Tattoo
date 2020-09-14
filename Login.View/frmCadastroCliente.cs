using Agenda.DTO;
using Agenda.Model;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.View
{
    public partial class frmCadastroCliente : Form
    {
        public frmCadastroCliente()
        {
            InitializeComponent();

        }


        // Variáveis Locais ####################################################################################

        ClienteDTO dto = new ClienteDTO();

        private string opcao = ""; //variável para usar no método IniciaModo()

        // Botões ###############################################################################################

        private void btnNovo_Click(object sender, EventArgs e)
        {
            opcao = "Novo";
            IniciaModo();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            opcao = "Salvar";
            IniciaModo();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            opcao = "Cancelar";
            IniciaModo();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            opcao = "Editar";
            IniciaModo();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            opcao = "Excluir";
            IniciaModo();
        }

        private void dgClientes_CellClick(object sender, DataGridViewCellEventArgs e) // Preenchendo campos com click no grid
        {
            txtCodigo.Text = dgClientes.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgClientes.CurrentRow.Cells[1].Value.ToString();
            txtEmail.Text = dgClientes.CurrentRow.Cells[2].Value.ToString();
            txtIdade.Text = dgClientes.CurrentRow.Cells[3].Value.ToString();
            txtTelefone.Text = dgClientes.CurrentRow.Cells[4].Value.ToString();
            mskCadastro.Text = dgClientes.CurrentRow.Cells[5].Value.ToString();
            GridHabilitarCampos();

        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))) // o textbox só aceita números e o backspace
            {
                e.Handled = true;
            }
        }

        private void txtIdade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))) // o textbox só aceita números e o backspace
            {
                e.Handled = true;
            }
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e) // Evento para executar a opção "Pesquisar" quando digitado algo
        {
            if (txtPesquisar.Text == "")
            {
                ListarGrid();
                return;
            }
            opcao = "Pesquisar";
            IniciaModo();

        }

        // Métodos e Funções #################################################################

        private void IniciaModo()
        {
            switch (opcao) // Variável opcao, para manipular o case
            {
                case "Novo":
                    NovoHabilitaCampos();
                    LimparCampos();
                    mskCadastro.Text = Convert.ToString(DateTime.Now);
                    txtNome.Focus();
                    break;

                case "Salvar":
                    try
                    {
                        dto.Nome = txtNome.Text;
                        dto.Email = txtEmail.Text;
                        dto.Idade = int.Parse(txtIdade.Text);
                        dto.Telefone = int.Parse(txtTelefone.Text);
                        dto.DataCadastro = Convert.ToDateTime(mskCadastro.Text);

                        int x = ClienteModel.Incluir(dto);
                        
                        if (x > 0)
                        {
                            MessageBox.Show(string.Format("Cliente {1} cadastrado com sucesso!", "Mensagem do Sistema", txtNome.Text));

                        }
                        else
                        {
                            MessageBox.Show("Cliente não foi cadastrado!", "Mensagem do Sistema");
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Problema para salvar as informações " + ex.Message);
                    }

                    LimparCampos();
                    ListarGrid();
                    DesabilitarCampos();
                    break;

                case "Editar":
                    
                    try
                    {
                        dto.Id = int.Parse(txtCodigo.Text);
                        dto.Nome = txtNome.Text;
                        dto.Email = txtEmail.Text;
                        dto.Idade = Convert.ToInt32(txtIdade.Text);
                        dto.Telefone = Convert.ToInt32(txtTelefone.Text);

                        //int x = ClienteModel.EditarUsuario(dto);

                        if (txtNome.Text != "")
                        {
                            var resultado = MessageBox.Show("Deseja atualizar os dados?", "Mensagem do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (resultado == System.Windows.Forms.DialogResult.Yes)
                            {
                                ClienteModel.EditarUsuario(dto);
                                MessageBox.Show(string.Format("Dados do cliente {1}, atualizados com sucesso!", "Mensagem do Sistema", txtNome.Text));
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Erro ao editar! " + ex);
                    }
                    BotaoEditarGrid();
                    ListarGrid();
                    LimparCampos();

                    break;

                case "Excluir":

                    try
                    {
                        dto.Id = Convert.ToInt32(txtCodigo.Text);
                        if (txtCodigo.Text != "")
                        {
                            var resultado = MessageBox.Show("Deseja excluir este cliente?", "Mensagem do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (resultado == System.Windows.Forms.DialogResult.Yes)
                            {
                                ClienteModel.Excluir(dto);
                                MessageBox.Show(string.Format("Cliente {0} excluido com sucesso!", txtNome.Text));
                            }

                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Erro ao excluir! " + ex.Message);
                    }

                    BotaoExcluir();

                    ListarGrid();
                    break;


                case "Cancelar":

                    BotaoCancelar();
                    LimparCampos();
                    break;

                case "Pesquisar":
                    dto.Nome = txtPesquisar.Text;
                    List<ClienteDTO> lista = new List<ClienteDTO>();
                    lista = new ClienteModel().PesquisaRapida(dto);
                    dgClientes.AutoGenerateColumns = false;
                    dgClientes.DataSource = lista;
                    break;   
                    
                default:
                    break;
            }
        }


        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            ListarGrid();
        }
        public void GridHabilitarCampos()
        {
            btnNovo.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;
            txtTelefone.Enabled = true;
            txtNome.Enabled = true;
            txtEmail.Enabled = true;
            txtIdade.Enabled = true;

        }
        private void NovoHabilitaCampos()
        {

            txtNome.Enabled = true;
            txtEmail.Enabled = true;
            txtIdade.Enabled = true;
            txtTelefone.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;


        }

        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtEmail.Enabled = false;
            txtIdade.Enabled = false;
            txtTelefone.Enabled = false;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtIdade.Clear();
            txtTelefone.Clear();
            mskCadastro.Clear();
        }

        private void BotaoEditarGrid()
        {
            txtNome.Enabled = false;
            txtEmail.Enabled = false;
            txtIdade.Enabled = false;
            txtTelefone.Enabled = false;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;

        }
        private void BotaoCancelar()
        {
            txtNome.Enabled = false;
            txtEmail.Enabled = false;
            txtIdade.Enabled = false;
            txtTelefone.Enabled = false;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void BotaoExcluir()
        {
            txtNome.Enabled = false;
            txtEmail.Enabled = false;
            txtIdade.Enabled = false;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            txtCodigo.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtIdade.Clear();
            mskCadastro.Clear();
        }

        private void ListarGrid()
        {
            try
            {
                List<ClienteDTO> lista = new List<ClienteDTO>();
                lista = new ClienteModel().ListarDados();
                dgClientes.DataSource = lista; // DATA SOURCE, JOGAR OS DADOS PARA MEU OBJETO(GRID), ele vai receber a lista preenchida
                dgClientes.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao listar dados! " + ex.Message);
            }
        }

        
    }
}
