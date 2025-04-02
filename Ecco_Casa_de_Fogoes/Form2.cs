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

        public frmEstoque()
        {
            InitializeComponent();
            ArredondarBotao(btnCriar, 45);
            ArredondarBotao(panel5, 40);
            ArredondarBotao(btnPesquisar, 45);
        }


        private void pbXis_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public void btnCadastrarP_Click(object sender, EventArgs e)
        {
            frmCadastrarH cadastro = new frmCadastrarH();
            cadastro.ShowDialog();
            this.Close();
        }
    }
}
