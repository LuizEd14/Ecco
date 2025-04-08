using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecco_Casa_de_Fogoes
{
    public partial class frmCadastrarH : Form
    {
        //Conexão inicial para o Bancos de Dados
        MySqlConnection Conexao;
        string data_source = "datasource=localhost; username=root; password=; database=ecco";

        int id;
        string produto;
        string tipo;
        int quantidade;
        float valor;

        public frmCadastrarH()
        {
            InitializeComponent();
            ArredondarBotao(btnSalvar, 45);
        }

        public frmCadastrarH(int id, string produto, string tipo, int quantidade, float valor)
        {
            InitializeComponent();
            ArredondarBotao(btnSalvar, 45);

            // Preenche os campos com os dados recebidos
            txtID.Text = id.ToString();
            txtProduto.Text = produto;
            txtTipo.Text = tipo;
            txtQuantidade.Text = quantidade.ToString();
            txtValor.Text = valor.ToString("F2", new CultureInfo("pt-BR"));

            txtID.Enabled = false; // Evita mudar o ID ao editar
        }

        private void Estoque(object sender, EventArgs e)
        {
            frmEstoque estoque = new frmEstoque();
            estoque.Show();
            this.Hide();
        }

        public void Caixa(object sender, EventArgs e)
        {
            frmCaixa caixa = new frmCaixa();
            caixa.Show();
            this.Hide();
        }

        public void Historio(object sender, EventArgs e)
        {
            frmHisto cadastro = new frmHisto();
            cadastro.Show();
            this.Hide();
        }

        private void pbXis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Entrou(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackColor = ColorTranslator.FromHtml("#FF8813");
            }
            catch
            {
                pbXis.BackColor = Color.Red;
            }
        }

        private void Saiu(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).BackColor = ColorTranslator.FromHtml("#FAC100");
            }
            catch
            {
                pbXis.BackColor = Color.Transparent;
            }
        }

        private bool ValidarCampo(TextBox campo, string nomeCampo)
        {
            if (string.IsNullOrWhiteSpace(campo.Text))
            {
                Erro(campo);
                MessageBox.Show($"O campo {nomeCampo} não pode estar vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.BackColor = System.Drawing.Color.White;
                txtProduto.BackColor = System.Drawing.Color.White;
                txtTipo.BackColor = System.Drawing.Color.White;
                txtQuantidade.BackColor = System.Drawing.Color.White;
                txtValor.BackColor = System.Drawing.Color.White;

                //Validação dos campos obrigatórios.
                if (!ValidarCampo(txtID, "ID")) return;
                if (!ValidarCampo(txtProduto, "Produto")) return;
                if (!ValidarCampo(txtTipo, "Tipo")) return;
                if (!ValidarCampo(txtQuantidade, "Quantidade")) return;
                if (!ValidarCampo(txtValor, "Valor")) return;

                //Validação dos campos preenchidos.
                try
                { 
                id = Convert.ToInt32(txtID.Text);
                produto = txtProduto.Text;
                tipo = txtTipo.Text;
                quantidade = Convert.ToInt32(txtQuantidade.Text);
                valor = float.Parse(txtValor.Text, new CultureInfo("pt-BR"));
                }
                catch
                {
                    MessageBox.Show("Alguns dos campos estão preenchidos errado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                //Criando uma conexão com o banco de dados
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                //Comando SQL para inserir um Produto no banco de dados
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = Conexao
                };

                cmd.Prepare();

                cmd.CommandText = @"INSERT INTO produto (idproduto, produto, tipo, quant, valorunidade) 
                VALUES (@id, @produto, @tipo, @quant, @valorunidade)
                ON DUPLICATE KEY UPDATE 
                produto = VALUES(produto), 
                tipo = VALUES(tipo), 
                quant = VALUES(quant), 
                valorunidade = VALUES(valorunidade),
                data_alteracao = NOW()";

                //Adicionar parametros com o dados do formulário
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@produto", txtProduto.Text.Trim());
                cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
                cmd.Parameters.AddWithValue("@quant", quantidade);
                cmd.Parameters.AddWithValue("@valorunidade", valor);

                //execulta o comando acima
                cmd.ExecuteNonQuery();

                //mensagem de sucesso
                MessageBox.Show("O produto foi salvo com sucesso!", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmEstoque estoque = new frmEstoque();
                estoque.Show();
                this.Hide();
            }
            catch (MySqlException er)
            {
                MessageBox.Show("Erro: " + er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                //Garantir que a conexão com o banco será fechada, mesmo se ocorrer um erro
                if(Conexao != null && Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }

        private void ArredondarBotao(Button btn, int borderRadius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(btn.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(btn.Width - borderRadius, btn.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, btn.Height - borderRadius, borderRadius, borderRadius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }

        private void Erro(Control e)
        {
            e.BackColor = System.Drawing.Color.LightPink;
        }
    }
}
