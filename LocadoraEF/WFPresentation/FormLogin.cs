using BLL;
using BLL.Security;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPresentation
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private FuncionarioService funcionarioBLL = new FuncionarioService();

        private void button1_Click(object sender, EventArgs e)
        {
            DataResponse<Funcionario> response = funcionarioBLL.Autenticar(txtEmail.Text, txtSenha.Text);
            if (response.Sucesso)
            {
                User.FuncionarioLogado = response.Data[0];
                FormMenu frmMenu = new FormMenu();
                this.Hide();
                frmMenu.ShowDialog();
                //Esta linha só executa quando o FormMenu for fechado
                this.Show();
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }
    }
}
