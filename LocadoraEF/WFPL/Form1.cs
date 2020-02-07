using BLL;
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

namespace WFPL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataResponse<Cliente> response = new ClienteService().GetByID(3);

            if (response.Sucesso)
            {
                if (response.Data.Count != 0)
                {
                    foreach (Cliente item in response.Data)
                    {
                        MessageBox.Show(item.Nome);
                    }
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado");
                }
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }

        }
    }
}
