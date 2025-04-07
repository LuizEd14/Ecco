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
    public partial class frmHisto : Form
    {
        public frmHisto()
        {
            InitializeComponent();
        }

        private void frmHisto_Load(object sender, EventArgs e)
        {
            ArredondarBotao(panel5, 45);
            ArredondarBotao(btnPesquisar, 40);
            dataGridView1.DefaultCellStyle.Font = new Font("Montserrat", 15);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 22, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1723A6");
            dataGridView1.EnableHeadersVisualStyles = false;
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
    }
}
