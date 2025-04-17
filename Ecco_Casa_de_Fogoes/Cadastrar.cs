using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Ecco_Casa_de_Fogoes
{
    public partial class frmCadastrarH : Form
    {
        // Objeto de conexão com o banco de dados
        MySqlConnection Conexao;

        // String de conexão com o banco MySQL
        string data_source = "datasource=localhost; username=root; password=; database=ecco";

        // Variáveis para armazenar dados do produto
        int id;
        string produto;
        string tipo;
        int quantidade;
        float valor;

        // Construtor padrão
        public frmCadastrarH()
        {
            InitializeComponent();
            ArredondarBotao(btnSalvar, 45); // Arredonda o botão "Salvar"
            ArredondarBotao(btnCancelar, 45); // Arredonda o botão "Cancelar"
        }

        // Construtor usado para edição de produto, preenchendo os campos
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

            txtID.Enabled = false; // Impede alteração do ID
        }

        // Abre o formulário de Estoque
        private void Estoque(object sender, EventArgs e)
        {
            frmEstoque estoque = new frmEstoque();
            estoque.Show();
            this.Hide();
        }

        // Abre o formulário do Caixa
        public void Caixa(object sender, EventArgs e)
        {
            frmCaixa caixa = new frmCaixa();
            caixa.Show();
            this.Hide();
        }

        // Abre o formulário de Histórico
        public void Historio(object sender, EventArgs e)
        {
            frmHisto cadastro = new frmHisto();
            cadastro.Show();
            this.Hide();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmEstoque estoque = new frmEstoque();
            estoque.Show();
            this.Hide();
        }

        // Fecha a aplicação ao clicar no "X"
        private void pbXis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Altera cor ao passar o mouse (efeito de hover)
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

        // Restaura cor ao tirar o mouse
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

        // Valida se o campo está vazio e mostra mensagem de aviso
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

        // Evento de clique do botão "Salvar"
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Resetando cor dos campos para branco
                txtID.BackColor = Color.White;
                txtProduto.BackColor = Color.White;
                txtTipo.BackColor = Color.White;
                txtQuantidade.BackColor = Color.White;
                txtValor.BackColor = Color.White;

                // Valida os campos obrigatórios
                if (!ValidarCampo(txtID, "ID")) return;
                if (!ValidarCampo(txtProduto, "Produto")) return;
                if (!ValidarCampo(txtTipo, "Tipo")) return;
                if (!ValidarCampo(txtQuantidade, "Quantidade")) return;
                if (!ValidarCampo(txtValor, "Valor")) return;

                try
                {
                    // Converte os valores dos campos
                    id = Convert.ToInt32(txtID.Text);
                    produto = txtProduto.Text;
                    tipo = txtTipo.Text;
                    quantidade = Convert.ToInt32(txtQuantidade.Text);
                    valor = float.Parse(txtValor.Text, new CultureInfo("pt-BR"));
                }
                catch
                {
                    MessageBox.Show("Alguns dos campos estão preenchidos de forma incorreta.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Abre conexão com o banco
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                // Comando SQL com INSERT ou UPDATE, caso o ID já exista
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = Conexao
                };

                cmd.CommandText = @"
                    INSERT INTO produto (idproduto, produto, tipo, quant, valorunidade) 
                    VALUES (@id, @produto, @tipo, @quant, @valorunidade)
                    ON DUPLICATE KEY UPDATE 
                        produto = VALUES(produto), 
                        tipo = VALUES(tipo), 
                        quant = VALUES(quant), 
                        valorunidade = VALUES(valorunidade),
                        data_alteracao = NOW()";

                // Parâmetros da query
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@produto", produto.Trim());
                cmd.Parameters.AddWithValue("@tipo", tipo.Trim());
                cmd.Parameters.AddWithValue("@quant", quantidade);
                cmd.Parameters.AddWithValue("@valorunidade", valor);

                cmd.Prepare(); // Prepara o comando
                cmd.ExecuteNonQuery(); // Executa no banco

                // Mensagem de sucesso
                MessageBox.Show("O produto foi salvo com sucesso!", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Volta para a tela de estoque
                frmEstoque estoque = new frmEstoque();
                estoque.Show();
                this.Hide();
            }
            catch (MySqlException er)
            {
                MessageBox.Show("Erro: " + er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Fecha a conexão, se estiver aberta
                if (Conexao != null && Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }

        // Arredonda os cantos de um botão
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

        // Deixa o campo em rosa caso tenha erro
        private void Erro(Control e)
        {
            e.BackColor = Color.LightPink;
        }
    }
}
