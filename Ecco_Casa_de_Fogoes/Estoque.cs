using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecco_Casa_de_Fogoes
{
    public partial class frmEstoque : Form
    {
        string data_source = "datasource=localhost; username=root; password=; database=ecco";

        public frmEstoque()
        {
            InitializeComponent();
            ArredondarBotao(btnCriar, 45);
            ArredondarBotao(panel5, 40);
            ArredondarBotao(btnPesquisar, 45);
        }

        private void EditarTabela(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.Font = new Font("Montserrat", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 18, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FAC100");
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1723A6");
            dataGridView1.EnableHeadersVisualStyles = false;
            CarregarDados();
        }

        private void pbXis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Historio(object sender, EventArgs e)
        {
            frmHisto cadastro = new frmHisto();
            cadastro.Show();
            this.Hide();
        }

        public void Caixa(object sender, EventArgs e)
        {
            frmCaixa caixa = new frmCaixa();
            caixa.Show();
            this.Hide();
        }

        public void CadastrarH(object sender, EventArgs e)
        {
            frmCadastrarH cadastro = new frmCadastrarH();
            cadastro.Show();
            this.Hide();
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

                    dataGridView1.Columns["idproduto"].HeaderText = "ID do Produto";
                    dataGridView1.Columns["produto"].HeaderText = "Produto";
                    dataGridView1.Columns["tipo"].HeaderText = "Tipo";
                    dataGridView1.Columns["quant"].HeaderText = "Quantidade";
                    dataGridView1.Columns["valorunidade"].HeaderText = "Valor";
                    dataGridView1.Columns["data_insercao"].HeaderText = "Data de inserção";
                    dataGridView1.Columns["data_alteracao"].HeaderText = "Data de alteração";

                    // Remove botões antigos para evitar duplicação
                    if (dataGridView1.Columns.Contains("Editar"))
                        dataGridView1.Columns.Remove("Editar");
                    if (dataGridView1.Columns.Contains("Excluir"))
                        dataGridView1.Columns.Remove("Excluir");

                    // Coluna Editar
                    DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                    btnEditar.Name = "Editar";
                    btnEditar.HeaderText = "Atualizar";
                    btnEditar.Text = "✏️ Editar";
                    btnEditar.UseColumnTextForButtonValue = true;
                    btnEditar.DefaultCellStyle.BackColor = Color.LightGreen;
                    dataGridView1.Columns.Add(btnEditar);

                    // Coluna Excluir
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string idProduto = dataGridView1.Rows[e.RowIndex].Cells["idproduto"].Value.ToString();

                if (dataGridView1.Columns[e.ColumnIndex].Name == "Editar")
                {
                    MessageBox.Show($"Editar produto com ID: {idProduto}");
                    // Aqui você pode abrir um novo formulário com os dados preenchidos para editar
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Excluir")
                {
                    DialogResult resultado = MessageBox.Show("Deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo);
                    if (resultado == DialogResult.Yes)
                    {
                        ExcluirProduto(idProduto);
                        CarregarDados(); // Atualiza a tabela após excluir
                    }
                }
            }
        }

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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string termo = txtPesquisar.Text.Trim();

            if (string.IsNullOrWhiteSpace(termo))
            {
                // Se estiver vazio, carrega tudo
                CarregarDados();
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

                    dataGridView1.Columns["idproduto"].HeaderText = "ID do Produto";
                    dataGridView1.Columns["produto"].HeaderText = "Produto";
                    dataGridView1.Columns["tipo"].HeaderText = "Tipo";
                    dataGridView1.Columns["quant"].HeaderText = "Quantidade";
                    dataGridView1.Columns["valorunidade"].HeaderText = "Valor";
                    dataGridView1.Columns["data_insercao"].HeaderText = "Data de inserção";
                    dataGridView1.Columns["data_alteracao"].HeaderText = "Data de alteração";

                    // Verifica e adiciona coluna "Editar"
                    if (!dataGridView1.Columns.Contains("Editar"))
                    {
                        DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                        btnEditar.HeaderText = "Editar";
                        btnEditar.Name = "Editar";
                        btnEditar.Text = "✏️ Editar";
                        btnEditar.UseColumnTextForButtonValue = true;
                        btnEditar.DefaultCellStyle.BackColor = Color.LightBlue;
                        dataGridView1.Columns.Add(btnEditar);
                    }

                    // Verifica e adiciona coluna "Excluir"
                    if (!dataGridView1.Columns.Contains("Excluir"))
                    {
                        DataGridViewButtonColumn btnExcluir = new DataGridViewButtonColumn();
                        btnExcluir.HeaderText = "Excluir";
                        btnExcluir.Name = "Excluir";
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


        private void txtPesquisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPesquisar.PerformClick(); // Simula clique no botão
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
