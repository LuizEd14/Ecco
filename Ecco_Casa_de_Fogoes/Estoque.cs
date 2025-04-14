using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Ecco_Casa_de_Fogoes
{
    public partial class frmEstoque : Form
    {
        // String de conexão com o banco de dados MySQL
        string data_source = "datasource=localhost; username=root; password=; database=ecco";

        public frmEstoque()
        {
            InitializeComponent();

            // Arredonda os botões e o painel
            ArredondarBotao(btnCriar, 45);
            ArredondarBotao(panel5, 40);
            ArredondarBotao(btnPesquisar, 45);
        }

        // Estiliza o DataGridView e carrega os dados
        private void EditarTabela(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.Font = new Font("Montserrat", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 18, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FAC100");
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1723A6");
            dataGridView1.EnableHeadersVisualStyles = false;

            CarregarDados(); // Chama função que busca os produtos do banco
        }

        // Fecha a aplicação
        private void pbXis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Navega para o formulário de histórico
        public void Historio(object sender, EventArgs e)
        {
            frmHisto cadastro = new frmHisto();
            cadastro.Show();
            this.Hide();
        }

        // Navega para o formulário do caixa
        public void Caixa(object sender, EventArgs e)
        {
            frmCaixa caixa = new frmCaixa();
            caixa.Show();
            this.Hide();
        }

        // Navega para o formulário de cadastro de produto
        public void CadastrarH(object sender, EventArgs e)
        {
            frmCadastrarH cadastro = new frmCadastrarH();
            cadastro.Show();
            this.Hide();
        }

        // Altera a cor de fundo quando o mouse entra em um botão
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

        // Altera a cor de fundo quando o mouse sai de um botão
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

        // Função que arredonda cantos de botões ou painéis
        private void ArredondarBotao(Control btn, int borderRadius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(btn.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(btn.Width - borderRadius, btn.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, btn.Height - borderRadius, borderRadius, borderRadius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }

        // Carrega todos os produtos do banco e exibe no DataGridView
        private void CarregarDados()
        {
            using (MySqlConnection conexao = new MySqlConnection(data_source))
            {
                try
                {
                    conexao.Open();
                    string query = "SELECT * FROM produto";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                    DataTable tabela = new DataTable();
                    adaptador.Fill(tabela);

                    dataGridView1.DataSource = tabela;

                    // Renomeia os cabeçalhos das colunas
                    dataGridView1.Columns["idproduto"].HeaderText = "ID do Produto";
                    dataGridView1.Columns["produto"].HeaderText = "Produto";
                    dataGridView1.Columns["tipo"].HeaderText = "Tipo";
                    dataGridView1.Columns["quant"].HeaderText = "Quantidade";
                    dataGridView1.Columns["valorunidade"].HeaderText = "Valor";
                    dataGridView1.Columns["data_insercao"].HeaderText = "Data de inserção";
                    dataGridView1.Columns["data_alteracao"].HeaderText = "Data de alteração";

                    // Remove colunas de botões se já existirem
                    if (dataGridView1.Columns.Contains("Editar"))
                        dataGridView1.Columns.Remove("Editar");
                    if (dataGridView1.Columns.Contains("Excluir"))
                        dataGridView1.Columns.Remove("Excluir");

                    // Adiciona botão "Editar"
                    DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                    btnEditar.Name = "Editar";
                    btnEditar.HeaderText = "Atualizar";
                    btnEditar.Text = "✏️ Editar";
                    btnEditar.UseColumnTextForButtonValue = true;
                    btnEditar.DefaultCellStyle.BackColor = Color.LightGreen;
                    dataGridView1.Columns.Add(btnEditar);

                    // Adiciona botão "Excluir"
                    DataGridViewButtonColumn btnExcluir = new DataGridViewButtonColumn();
                    btnExcluir.Name = "Excluir";
                    btnExcluir.HeaderText = "Excluir";
                    btnExcluir.Text = "🗑️ Excluir";
                    btnExcluir.UseColumnTextForButtonValue = true;
                    btnExcluir.DefaultCellStyle.BackColor = Color.LightCoral;
                    dataGridView1.Columns.Add(btnExcluir);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados: " + ex.Message);
                }
            }
        }

        // Trata o clique nos botões dentro do DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string coluna = dataGridView1.Columns[e.ColumnIndex].Name;

                if (coluna == "Editar")
                {
                    // Pega os dados da linha clicada
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    int id = Convert.ToInt32(row.Cells["idproduto"].Value);
                    string nomeProduto = row.Cells["produto"].Value.ToString();
                    string tipo = row.Cells["tipo"].Value.ToString();
                    int quantidade = Convert.ToInt32(row.Cells["quant"].Value);
                    float valor = float.Parse(row.Cells["valorunidade"].Value.ToString());

                    // Abre o formulário de edição com os dados preenchidos
                    frmCadastrarH editarForm = new frmCadastrarH(id, nomeProduto, tipo, quantidade, valor);
                    editarForm.Show();
                    this.Hide();
                }
                else if (coluna == "Excluir")
                {
                    // Confirma antes de excluir
                    DialogResult resultado = MessageBox.Show("Deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo);
                    if (resultado == DialogResult.Yes)
                    {
                        string idProduto = dataGridView1.Rows[e.RowIndex].Cells["idproduto"].Value.ToString();
                        ExcluirProduto(idProduto);
                        CarregarDados(); // Atualiza a tabela após excluir
                    }
                }
            }
        }

        // Exclui um produto do banco de dados pelo ID
        private void ExcluirProduto(string idProduto)
        {
            using (MySqlConnection conexao = new MySqlConnection(data_source))
            {
                try
                {
                    conexao.Open();
                    string query = "DELETE FROM produto WHERE idproduto = @id";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@id", idProduto);
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Produto excluído com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir produto: " + ex.Message);
                }
            }
        }

        // Realiza pesquisa por nome, tipo ou ID do produto
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string termo = txtPesquisar.Text.Trim();

            if (string.IsNullOrWhiteSpace(termo))
            {
                CarregarDados(); // Se vazio, carrega todos os dados
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection(data_source))
            {
                try
                {
                    conexao.Open();
                    string query = "SELECT * FROM produto WHERE produto LIKE @termo OR tipo LIKE @termo OR idproduto LIKE @termo";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@termo", "%" + termo + "%");

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                    DataTable tabela = new DataTable();
                    adaptador.Fill(tabela);

                    dataGridView1.DataSource = tabela;

                    // Renomeia colunas
                    dataGridView1.Columns["idproduto"].HeaderText = "ID do Produto";
                    dataGridView1.Columns["produto"].HeaderText = "Produto";
                    dataGridView1.Columns["tipo"].HeaderText = "Tipo";
                    dataGridView1.Columns["quant"].HeaderText = "Quantidade";
                    dataGridView1.Columns["valorunidade"].HeaderText = "Valor";
                    dataGridView1.Columns["data_insercao"].HeaderText = "Data de inserção";
                    dataGridView1.Columns["data_alteracao"].HeaderText = "Data de alteração";

                    // Adiciona botões se ainda não estiverem na tabela
                    if (!dataGridView1.Columns.Contains("Editar"))
                    {
                        DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                        btnEditar.Name = "Editar";
                        btnEditar.HeaderText = "Editar";
                        btnEditar.Text = "✏️ Editar";
                        btnEditar.UseColumnTextForButtonValue = true;
                        btnEditar.DefaultCellStyle.BackColor = Color.LightBlue;
                        dataGridView1.Columns.Add(btnEditar);
                    }

                    if (!dataGridView1.Columns.Contains("Excluir"))
                    {
                        DataGridViewButtonColumn btnExcluir = new DataGridViewButtonColumn();
                        btnExcluir.Name = "Excluir";
                        btnExcluir.HeaderText = "Excluir";
                        btnExcluir.Text = "🗑️ Excluir";
                        btnExcluir.UseColumnTextForButtonValue = true;
                        btnExcluir.DefaultCellStyle.BackColor = Color.LightCoral;
                        dataGridView1.Columns.Add(btnExcluir);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao pesquisar: " + ex.Message);
                }
            }
        }

        // Permite pesquisar pressionando Enter
        private void txtPesquisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPesquisar.PerformClick(); // Simula clique no botão de pesquisa
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}