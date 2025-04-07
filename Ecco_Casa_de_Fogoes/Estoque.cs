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
            dataGridView1.DefaultCellStyle.Font = new Font("Montserrat", 15);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 22, FontStyle.Bold);
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
                    string query = "SELECT * FROM produto"; // ou sua tabela desejada

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados: " + ex.Message);
                }
            }
        }

    }
}
