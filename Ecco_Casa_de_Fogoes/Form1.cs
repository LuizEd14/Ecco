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
    public partial class frmCadastrarH : Form
    {
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

        private void btnEstoque_Click(object sender, EventArgs e)
        {
            frmEstoque estoque = new frmEstoque();
            estoque.ShowDialog();
            this.Close();
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            txtID.BackColor = System.Drawing.Color.White;
            txtProduto.BackColor = System.Drawing.Color.White;
            txtTipo.BackColor = System.Drawing.Color.White;
            txtQuantidade.BackColor = System.Drawing.Color.White;
            txtValor.BackColor = System.Drawing.Color.White;

            //Validação dos campos obrigatórios.
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                Erro(txtID);
                MessageBox.Show("O campo ID não pode estar vazio..", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProduto.Text))
            {
                Erro(txtProduto);
                MessageBox.Show("O campo Produto não pode estar vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                Erro(txtTipo);
                MessageBox.Show("O campo Tipo não pode estar vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtQuantidade.Text))
            {
                Erro(txtQuantidade);
                MessageBox.Show("O campo Quantidade não pode estar vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                Erro(txtValor);
                MessageBox.Show("O campo Valor não pode estar vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Validação dos campos preenchidos.
            try
            {
                id = Convert.ToInt32(txtID.Text);
                produto = txtProduto.Text;
                tipo = txtTipo.Text;
                quantidade = Convert.ToInt32(txtQuantidade.Text);
                valor = Convert.ToSingle(txtValor.Text);

                MessageBox.Show("O produto foi salvo com sucesso!", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Verifique se digitou errado em uma das casas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
