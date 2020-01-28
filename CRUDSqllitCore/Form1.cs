using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CRUDSqllitCore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Funcionario fun = new Funcionario();
        

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                fun.nome = txtNome.Text;
                fun.cpf = txtCpf.Text;                
                fun.dataContratacao = txtDataContratacao.Text;
                fun.funcao = txtFunc.Text;
                fun.telefone = txtCelular.Text;

            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao inserir os dados!!!" + erro.Message);
            }

            validaCpf(fun.cpf);

            ConexaoSql.inserir(fun);

            MessageBox.Show("Os dados do funcionario " + fun.nome + " foram incluidos com sucesso!!!");
            exibeDados(fun.cpf);
            limpaDados();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

            try
            {
                fun.nome = txtNome.Text;
                fun.cpf = txtCpf.Text;
                fun.dataContratacao = txtDataContratacao.Text;
                fun.funcao = txtFunc.Text;
                fun.telefone = txtCelular.Text;
            }
            catch (Exception)
            {

                throw;
            }

            ConexaoSql.alterar(fun);
            MessageBox.Show("Os dados do funcionario " + fun.nome + " foram alterados com sucesso!!!");

            limpaDados();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if(txtCpf.Text == null)
            {
                MessageBox.Show("Insira os dados para executar a função deletar.");
            }
            else
            {
                try
                {
                    fun.cpf = txtCpf.Text;
                    
                }
                catch (Exception )
                {

                    throw;
                }

                ConexaoSql.deletar(fun.cpf);

                MessageBox.Show("Funcionario " + fun.nome + " excluido da base de dados com sucesso!!!");

                limpaDados();
            }            

        }


        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {

            try
            {
                fun.cpf = txtCriterio.Text;

            }
            catch (Exception)
            {

                throw;
            }            


            exibeDados(fun.cpf);
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Obrigado por usar os serviços!!!");
            Application.Exit();
        }



        private bool validarEntradas()
        {
            if(string.IsNullOrEmpty(txtCpf.Text) && string.IsNullOrEmpty(txtNome.Text)/* && string.IsNullOrEmpty(txtDataContratacao.Text)
                && string.IsNullOrEmpty(txtCelular.Text) && string.IsNullOrEmpty(txtFunc.Text)*/)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void limpaDados()
        {
            txtNome.Text = "";
            txtCpf.Text = "";
            txtFunc.Text = "";
            txtCelular.Text = "";
            txtDataContratacao.Text = "";
        }
        
        private void exibeDados(string cpf)
        {
            try
            {
                fun.cpf = cpf;
                DataTable dadosGrid = new DataTable();
                dadosGrid = ConexaoSql.pegaFuncionario(fun.cpf);
                dvgFuncionario.DataSource = dadosGrid;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void validaCpf(string cpf) 
        {
            cpf = cpf.Replace("-", "");
            cpf = cpf.Replace(".", "");
            string expreCpf = @"^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$";

            if(Regex.IsMatch(cpf, expreCpf))
            {
                MessageBox.Show("Cpf é válido para a operação!!!");
            }
            else
            {
                MessageBox.Show("Cpf não é válido para a operação!!!");
            }

        }




    }
}
