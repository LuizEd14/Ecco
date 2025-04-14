using MySql.Data.MySqlClient; // Importa a biblioteca para conexão com MySQL
using System; // Importa classes básicas do sistema
using System.Collections.Generic; // Importa classes para coleções genéricas
using System.ComponentModel; // Importa classes para componentes
using System.Data; // Importa classes para manipulação de dados
using System.Drawing; // Importa classes para manipulação de gráficos
using System.Drawing.Drawing2D; // Importa classes para gráficos 2D
using System.Linq; // Importa classes para LINQ
using System.Text; // Importa classes para manipulação de texto
using System.Threading.Tasks; // Importa classes para tarefas assíncronas
using System.Windows.Forms; // Importa classes para formulários do Windows

namespace Ecco_Casa_de_Fogoes // Define o namespace do projeto
{
    public partial class frmHisto : Form // Define a classe do formulário
    {
        // String de conexão com o banco de dados
        private string conexaoString = "server=localhost; user=root; password=; database=ecco";

        // Construtor do formulário
        public frmHisto()
        {
            InitializeComponent(); // Inicializa os componentes do formulário

            // Arredonda os botões e configurações do DataGridView
            ArredondarBotao(panel5, 45);
            ArredondarBotao(btnPesquisar, 40);
            dataGridView1.DefaultCellStyle.Font = new Font("Montserrat", 15); // Define a fonte das células
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 20, FontStyle.Bold); // Define a fonte dos cabeçalhos
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Alinha o conteúdo das células ao centro
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Alinha os cabeçalhos ao centro
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; // Define a cor de fundo dos cabeçalhos
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1723A6"); // Define a cor do texto dos cabeçalhos
            dataGridView1.EnableHeadersVisualStyles = false; // Desabilita estilos visuais padrão
        }

        // Evento que ocorre ao carregar o formulário
        private void frmHisto_Load(object sender, EventArgs e)
        {
            CarregarDadosPagamento(); // Carrega os dados de pagamento ao iniciar
        }

        // Método para abrir o formulário de cadastro
        public void CadastrarH(object sender, EventArgs e)
        {
            frmCadastrarH cadastro = new frmCadastrarH();
            cadastro.Show(); // Exibe o formulário de cadastro
            this.Hide(); // Esconde o formulário atual
        }

        // Método para abrir o formulário de estoque
        private void Estoque(object sender, EventArgs e)
        {
            frmEstoque estoque = new frmEstoque();
            estoque.Show(); // Exibe o formulário de estoque
            this.Hide(); // Esconde o formulário atual
        }

        // Método para abrir o formulário de caixa
        public void Caixa(object sender, EventArgs e)
        {
            frmCaixa caixa = new frmCaixa();
            caixa.Show(); // Exibe o formulário de caixa
            this.Hide(); // Esconde o formulário atual
        }

        // Método para fechar a aplicação
        private void pbXis_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Encerra a aplicação
        }

        // Evento que ocorre quando um botão é pressionado
        private void Entrou(object sender, EventArgs e)
        {
            try
            {
                // Altera a cor de fundo do botão pressionado
                ((Button)sender).BackColor = ColorTranslator.FromHtml("#FF8813");
            }
            catch
            {
                // Altera a cor de fundo do botão de fechar em caso de erro
                pbXis.BackColor = Color.Red;
            }
        }

        // Evento que ocorre quando o mouse sai do botão
        private void Saiu(object sender, EventArgs e)
        {
            try
            {
                // Restaura a cor de fundo original do botão
                ((Button)sender).BackColor = ColorTranslator.FromHtml("#FAC100");
            }
            catch
            {
                // Restaura a cor de fundo original do botão de fechar em caso de erro
                pbXis.BackColor = Color.Transparent;
            }
        }

        // Evento que ocorre ao pressionar a tecla Enter no campo de pesquisa
        private void txtPesquisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Verifica se a tecla pressionada é Enter
            {
                btnPesquisar.PerformClick(); // Simula um clique no botão de pesquisa
            }
        }

        // Método para pesquisar pagamentos com base no termo fornecido
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string termo = txtPesquisar.Text.Trim(); // Obtém o termo de pesquisa

            if (!string.IsNullOrEmpty(termo)) // Verifica se o termo não está vazio
            {
                PesquisarPagamento(termo); // Chama o método de pesquisa
            }
            else
            {
                CarregarDadosPagamento(); // Se estiver vazio, carrega todos os dados
            }
        }

        // Método para carregar dados de pagamento no DataGridView
        private void CarregarDadosPagamento()
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoString)) // Cria uma nova conexão com o banco de dados
            {
                try
                {
                    conn.Open(); // Abre a conexão

                    // Consulta SQL para obter os dados de pagamento
                    string query = @"SELECT 
                    p.idvenda AS 'ID da Venda',
                    p.datacompra AS 'Data da Compra',
                    p.nomeproduto AS 'Produto',
                    p.quant AS 'Quantidade',
                    p.valortotal AS 'Valor Total',
                    p.valorcomdesconto AS 'Com Desconto',
                    p.id_produto AS 'ID Produto'
                    FROM Pagamento p
                    ORDER BY p.datacompra DESC"; // Ordena os resultados pela data da compra

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn); // Cria um adaptador para preencher o DataTable
                    DataTable tabela = new DataTable(); // Cria um novo DataTable
                    adapter.Fill(tabela); // Preenche o DataTable com os dados da consulta

                    if (tabela.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = tabela; // Define a fonte de dados do DataGridView
                    }
                    else
                    {
                        dataGridView1.DataSource = null; // Limpa a fonte de dados se não houver dados
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar o histórico: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); // Mensagem de erro
                }
            }
        }

        // Método para pesquisar pagamentos com base em um termo
        private void PesquisarPagamento(string termo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoString)) // Cria uma nova conexão com o banco de dados
            {
                try
                {
                    conn.Open(); // Abre a conexão

                    // Consulta SQL para pesquisar pagamentos
                    string query = @"SELECT 
                                p.idvenda AS 'ID da Venda',
                                p.datacompra AS 'Data da Compra',
                                p.nomeproduto AS 'Produto',
                                p.quant AS 'Quantidade',
                                p.valortotal AS 'Valor Total',
                                p.valorcomdesconto AS 'Com Desconto',
                                p.id_produto AS 'ID Produto'
                             FROM Pagamento p
                             WHERE p.nomeproduto LIKE @termo
                                OR p.idvenda LIKE @termo
                                OR p.id_produto LIKE @termo
                                OR DATE_FORMAT(p.datacompra, '%d/%m/%Y') LIKE @termo
                             ORDER BY p.datacompra DESC"; // Ordena os resultados pela data da compra

                    MySqlCommand cmd = new MySqlCommand(query, conn); // Cria um comando SQL
                    cmd.Parameters.AddWithValue("@termo", "%" + termo + "%"); // Adiciona o parâmetro da consulta

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd); // Cria um adaptador para preencher o DataTable
                    DataTable tabela = new DataTable(); // Cria um novo DataTable
                    adapter.Fill(tabela); // Preenche o DataTable com os dados da consulta

                    dataGridView1.DataSource = tabela; // Define a fonte de dados do DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao pesquisar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); // Mensagem de erro
                }
            }
        }

        // Método para arredondar os botões
        private void ArredondarBotao(Control btn, int borderRadius)
        {
            GraphicsPath path = new GraphicsPath(); // Cria um novo caminho gráfico
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90); // Adiciona arco no canto superior esquerdo
            path.AddArc(btn.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90); // Adiciona arco no canto superior direito
            path.AddArc(btn.Width - borderRadius, btn.Height - borderRadius, borderRadius, borderRadius, 0, 90); // Adiciona arco no canto inferior direito
            path.AddArc(0, btn.Height - borderRadius, borderRadius, borderRadius, 90, 90); // Adiciona arco no canto inferior esquerdo
            path.CloseFigure(); // Fecha a figura do caminho

            btn.Region = new Region(path); // Define a região do botão com o caminho arredondado
        }
    }
}