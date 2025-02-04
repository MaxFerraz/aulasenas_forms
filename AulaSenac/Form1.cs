using AulaSenac.Banco;
using AulaSenac.Controllers;
using AulaSenac.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AulaSenac
{
    public partial class Form1 : Form
    {
        CadGeral cadGeral = new CadGeral();
        CadGeralControllers cadGeralCon = new CadGeralControllers();
        GravaLog gravaLogAqui = new GravaLog();

        public Form1()
        {
            InitializeComponent();
        }

        private void cmdCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (ValidaCadastroGeral() == false)
                {
                    return;
                }
                cadGeral.RazaoSocial = txtRazaoSocial.Text;
                cadGeral.Fantasia = txtFantasia.Text;
                cadGeral.Cnpj = mskCNPJ.Text;
                cadGeral.Email = txtEmail.Text;
                cadGeral.Rua = txtLogradouro.Text;
                cadGeral.Numero = txtNum.Text;
                cadGeral.Complemento = txtCompl.Text;
                cadGeral.Bairro = txtBairro.Text;
                cadGeral.NomeMunicipio = txtNomeMunicipio.Text;
                cadGeral.Cep = txtCEP.Text;
                cadGeral.Uf = txtUF.Text;
                cadGeralCon.Inserir(cadGeral);
                MessageBox.Show("Cliente Cadastrado com Sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCadGeral();
            }
            catch (Exception ex)
            {
                gravaLogAqui.grava("Erro no Evento cmdCadastrar_Click : " + ex);
                throw new Exception("Erro: " + ex.Message);
            }
        }


        public Boolean ValidaCadastroGeral()
        {
            try
            {
                if (txtRazaoSocial.Text == "")
                {
                    MessageBox.Show("Informe o Nome do Cliente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRazaoSocial.Focus();
                    return false;
                }

                if (mskCNPJ.Text == "")
                {
                    MessageBox.Show("Informe o CNPJ do Cliente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCNPJ.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                gravaLogAqui.grava("Erro no Metodo ValidaCadastroGeral : " + ex);
                throw new Exception("Erro: " + ex.Message);
            }
        }

        public void LimparCadGeral()
        {
            txtRazaoSocial.Clear();
            txtFantasia.Clear();
            mskCNPJ.Clear();
            txtEmail.Clear();
            txtLogradouro.Clear();
            txtNum.Clear();
            txtCompl.Clear();
            txtBairro.Clear();
            txtNomeMunicipio.Clear();
            txtCEP.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cmdConsultar_Click(object sender, EventArgs e)
        {
            var codigo = txtConsultaCodigoUsuario.Text;
            dataGridView1.DataSource = cadGeralCon.ConsultaCadGeral(codigo);
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
        }

     
    }
}
