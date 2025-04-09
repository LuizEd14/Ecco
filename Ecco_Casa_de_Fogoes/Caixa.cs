using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecco_Casa_de_Fogoes
{
    public partial class frmCaixa : Form
    {

        string conexaoString = "server=localhost; user=root; password=; database=ecco";

        public frmCaixa()
        {

            InitializeComponent();

            dataGridView1.DefaultCellStyle.Font = new Font("Montserrat", 15);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 22, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FAC100");
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1723A6");
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void frmCaixa_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("nomeProduto", "Produto");
            dataGridView1.Columns.Add("quantidade", "Quantidade");
            dataGridView1.Columns.Add("valorUnitario", "Valor Unitário");
            dataGridView1.Columns.Add("valorTotal", "Valor Total");

            DataGridViewButtonColumn btnRemover = new DataGridViewButtonColumn();
            btnRemover.Name = "Remover";
            btnRemover.HeaderText = "Ação";
            btnRemover.Text = "Remover";
            btnRemover.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnRemover);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtID.Text, out int idProduto) && int.TryParse(txtQuantidade.Text, out int quantidade))
            {
                using (MySqlConnection conn = new MySqlConnection(conexaoString))
                {
                    conn.Open();
                    string query = "SELECT produto, valorunidade FROM produto WHERE idproduto = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idProduto);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nomeProduto = reader.GetString("produto");
                            decimal valorUnitario = reader.GetDecimal("valorunidade");
                            decimal valorTotal = valorUnitario * quantidade;

                            dataGridView1.Rows.Add(
                                nomeProduto,
                                quantidade,
                                valorUnitario.ToString("C"),
                                valorTotal.ToString("C")
                            );
                        }
                        else
                        {
                            MessageBox.Show("Produto não encontrado!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("ID ou Quantidade inválida!");
            }

            txtID.Clear();
            txtQuantidade.Clear();
            txtID.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Remover" && e.RowIndex >= 0)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        public void CadastrarH(object sender, EventArgs e)
        {
            frmCadastrarH cadastro = new frmCadastrarH();
            cadastro.Show();
            this.Hide();
        }

        private void Estoque(object sender, EventArgs e)
        {
            frmEstoque estoque = new frmEstoque();
            estoque.Show();
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

    }
}
