﻿using BLL;
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
    public partial class FormGenero : Form
    {
        public FormGenero()
        {
            InitializeComponent();
            dataGridView1.DataSource = bll.GetData().Data;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }
        GeneroService bll = new GeneroService();
        int idGeneroASerAtualizadoExcluido = 0;

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Genero result = (Genero)dataGridView1.SelectedRows[0].DataBoundItem;
            DataResponse<Genero> response = bll.GetByID(result.ID);
            if (response.Sucesso)
            {
                Genero genero = response.Data[0];
                idGeneroASerAtualizadoExcluido = genero.ID;
                txtGenero.Text = genero.Nome;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //É aqui que a entidade é criada!Ela será validada e 
            //formatada no Service e inserida no DAL
            Genero genero = new Genero();
            genero.Nome = txtGenero.Text;

            bll = new GeneroService();
            //Invoca a sequência de operações de inserção (bll depois dal)
            //e recebe a resposta destas operaçoes!
            Response response = bll.Insert(genero);
            if (response.Sucesso)
            {
                MessageBox.Show("Cadastrado com sucesso!");
                dataGridView1.DataSource = bll.GetData().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Genero genero = new Genero();
            genero.ID = idGeneroASerAtualizadoExcluido;
            genero.Nome = txtGenero.Text;

            Response response = bll.Update(genero);
            if (response.Sucesso)
            {
                MessageBox.Show("Gênero atualizado com sucesso!");
                dataGridView1.DataSource = bll.GetData().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Response response = bll.Delete(idGeneroASerAtualizadoExcluido);
            if (response.Sucesso)
            {
                MessageBox.Show("Gênero excluído com sucesso!");
                dataGridView1.DataSource = bll.GetData().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }
    }
}
