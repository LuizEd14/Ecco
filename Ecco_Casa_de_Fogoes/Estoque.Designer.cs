
namespace Ecco_Casa_de_Fogoes
{
    partial class frmEstoque
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbXis = new System.Windows.Forms.PictureBox();
            this.btnHisto = new System.Windows.Forms.Button();
            this.btnCaixa = new System.Windows.Forms.Button();
            this.btnEstoque = new System.Windows.Forms.Button();
            this.btnCadastrarP = new System.Windows.Forms.Button();
            this.txtPesquisar = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCriar = new System.Windows.Forms.Button();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbXis)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(193)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.pbXis);
            this.panel1.Controls.Add(this.btnHisto);
            this.panel1.Controls.Add(this.btnCaixa);
            this.panel1.Controls.Add(this.btnEstoque);
            this.panel1.Controls.Add(this.btnCadastrarP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1413, 55);
            this.panel1.TabIndex = 0;
            // 
            // pbXis
            // 
            this.pbXis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbXis.BackColor = System.Drawing.Color.Transparent;
            this.pbXis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbXis.Image = global::Ecco_Casa_de_Fogoes.Properties.Resources.xis1;
            this.pbXis.Location = new System.Drawing.Point(1360, 0);
            this.pbXis.Name = "pbXis";
            this.pbXis.Size = new System.Drawing.Size(53, 55);
            this.pbXis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbXis.TabIndex = 5;
            this.pbXis.TabStop = false;
            this.pbXis.Click += new System.EventHandler(this.pbXis_Click);
            this.pbXis.MouseEnter += new System.EventHandler(this.Entrou);
            this.pbXis.MouseLeave += new System.EventHandler(this.Saiu);
            // 
            // btnHisto
            // 
            this.btnHisto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHisto.FlatAppearance.BorderSize = 0;
            this.btnHisto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHisto.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHisto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(35)))), ((int)(((byte)(166)))));
            this.btnHisto.Image = global::Ecco_Casa_de_Fogoes.Properties.Resources.registro_de_vendas;
            this.btnHisto.Location = new System.Drawing.Point(511, 0);
            this.btnHisto.Name = "btnHisto";
            this.btnHisto.Size = new System.Drawing.Size(267, 55);
            this.btnHisto.TabIndex = 4;
            this.btnHisto.Text = "Histórico de Vendas";
            this.btnHisto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHisto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHisto.UseVisualStyleBackColor = true;
            this.btnHisto.Click += new System.EventHandler(this.Historio);
            this.btnHisto.MouseEnter += new System.EventHandler(this.Entrou);
            this.btnHisto.MouseLeave += new System.EventHandler(this.Saiu);
            // 
            // btnCaixa
            // 
            this.btnCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaixa.FlatAppearance.BorderSize = 0;
            this.btnCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaixa.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaixa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(35)))), ((int)(((byte)(166)))));
            this.btnCaixa.Image = global::Ecco_Casa_de_Fogoes.Properties.Resources.carte;
            this.btnCaixa.Location = new System.Drawing.Point(403, 0);
            this.btnCaixa.Name = "btnCaixa";
            this.btnCaixa.Size = new System.Drawing.Size(102, 55);
            this.btnCaixa.TabIndex = 3;
            this.btnCaixa.Text = "Caixa";
            this.btnCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCaixa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCaixa.UseVisualStyleBackColor = true;
            this.btnCaixa.MouseEnter += new System.EventHandler(this.Entrou);
            this.btnCaixa.MouseLeave += new System.EventHandler(this.Saiu);
            // 
            // btnEstoque
            // 
            this.btnEstoque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(136)))), ((int)(((byte)(19)))));
            this.btnEstoque.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEstoque.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstoque.Enabled = false;
            this.btnEstoque.FlatAppearance.BorderSize = 0;
            this.btnEstoque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstoque.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstoque.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(35)))), ((int)(((byte)(166)))));
            this.btnEstoque.Image = global::Ecco_Casa_de_Fogoes.Properties.Resources.estoque1;
            this.btnEstoque.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstoque.Location = new System.Drawing.Point(3, 0);
            this.btnEstoque.Name = "btnEstoque";
            this.btnEstoque.Size = new System.Drawing.Size(142, 55);
            this.btnEstoque.TabIndex = 1;
            this.btnEstoque.Text = "Estoque";
            this.btnEstoque.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEstoque.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEstoque.UseVisualStyleBackColor = false;
            // 
            // btnCadastrarP
            // 
            this.btnCadastrarP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastrarP.FlatAppearance.BorderSize = 0;
            this.btnCadastrarP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastrarP.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastrarP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(35)))), ((int)(((byte)(166)))));
            this.btnCadastrarP.Image = global::Ecco_Casa_de_Fogoes.Properties.Resources.Cadastrar;
            this.btnCadastrarP.Location = new System.Drawing.Point(151, 0);
            this.btnCadastrarP.Name = "btnCadastrarP";
            this.btnCadastrarP.Size = new System.Drawing.Size(246, 55);
            this.btnCadastrarP.TabIndex = 2;
            this.btnCadastrarP.Text = "Cadastrar Produto";
            this.btnCadastrarP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCadastrarP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCadastrarP.UseVisualStyleBackColor = true;
            this.btnCadastrarP.Click += new System.EventHandler(this.CadastrarH);
            this.btnCadastrarP.MouseEnter += new System.EventHandler(this.Entrou);
            this.btnCadastrarP.MouseLeave += new System.EventHandler(this.Saiu);
            // 
            // txtPesquisar
            // 
            this.txtPesquisar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(184)))), ((int)(((byte)(192)))));
            this.txtPesquisar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPesquisar.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisar.Location = new System.Drawing.Point(42, 4);
            this.txtPesquisar.Name = "txtPesquisar";
            this.txtPesquisar.Size = new System.Drawing.Size(1079, 36);
            this.txtPesquisar.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.btnCriar);
            this.panel4.Controls.Add(this.pbLogo);
            this.panel4.Location = new System.Drawing.Point(109, 695);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1209, 87);
            this.panel4.TabIndex = 3;
            // 
            // btnCriar
            // 
            this.btnCriar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(35)))), ((int)(((byte)(166)))));
            this.btnCriar.FlatAppearance.BorderSize = 0;
            this.btnCriar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCriar.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCriar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(193)))), ((int)(((byte)(0)))));
            this.btnCriar.Location = new System.Drawing.Point(1035, 22);
            this.btnCriar.Name = "btnCriar";
            this.btnCriar.Size = new System.Drawing.Size(136, 44);
            this.btnCriar.TabIndex = 1;
            this.btnCriar.Text = "Criar";
            this.btnCriar.UseVisualStyleBackColor = false;
            this.btnCriar.Click += new System.EventHandler(this.CadastrarH);
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLogo.Enabled = false;
            this.pbLogo.Image = global::Ecco_Casa_de_Fogoes.Properties.Resources.Logo;
            this.pbLogo.Location = new System.Drawing.Point(22, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 81);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(193)))), ((int)(((byte)(0)))));
            this.btnPesquisar.BackgroundImage = global::Ecco_Casa_de_Fogoes.Properties.Resources.achar;
            this.btnPesquisar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPesquisar.FlatAppearance.BorderSize = 0;
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Location = new System.Drawing.Point(1273, 125);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(45, 45);
            this.btnPesquisar.TabIndex = 4;
            this.btnPesquisar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPesquisar.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(184)))), ((int)(((byte)(192)))));
            this.panel5.Controls.Add(this.txtPesquisar);
            this.panel5.Location = new System.Drawing.Point(109, 127);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1141, 43);
            this.panel5.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(98)))), ((int)(((byte)(148)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(184)))), ((int)(((byte)(192)))));
            this.dataGridView1.Location = new System.Drawing.Point(109, 187);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1209, 514);
            this.dataGridView1.TabIndex = 7;
            // 
            // frmEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(35)))), ((int)(((byte)(166)))));
            this.ClientSize = new System.Drawing.Size(1413, 817);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEstoque";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ecco´s Casa de Fogões";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EditarTabela);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbXis)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnHisto;
        private System.Windows.Forms.Button btnCaixa;
        private System.Windows.Forms.Button btnCadastrarP;
        private System.Windows.Forms.Button btnEstoque;
        private System.Windows.Forms.PictureBox pbXis;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.TextBox txtPesquisar;
        private System.Windows.Forms.Button btnCriar;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}