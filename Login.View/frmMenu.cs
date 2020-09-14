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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*frmCadastroCliente view = new frmCadastroCliente();
            this.Hide();
            view.Closed += (s, args) => this.Close();
            view.Show();*/
            frmCadastroCliente view = new frmCadastroCliente();
            view.Show();

        }

        
    }
}
