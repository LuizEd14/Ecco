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
            ArredondarBotao(btnExcluirMes, 40);
            dataGridView1.DefaultCellStyle.Font = new Font("Montserrat", 12); // Define a fonte das células
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 18, FontStyle.Bold); // Define a fonte dos cabeçalhos
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Alinha o conteúdo das células ao centro
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Alinha os cabeçalhos ao centro
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; // Define a cor de fundo dos cabeçalhos
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1723A6"); // Define a cor do texto dos cabeçalhos
            dataGridView1.EnableHeadersVisualStyles = false; // Desabilita estilos visuais padrão
            dataGridView1.AllowUserToAddRows = false; // Impede que o usuário adicione novas linhas
        }

        // Evento que ocorre ao carregar o formulário
        private void frmHisto_Load(object sender, EventArgs e)
        {
            CarregarDados(); // Carrega os dados de pagamento ao iniciar
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
                CarregarDados(); // Se estiver vazio, carrega todos os dados
            }
        }

        private void btnExcluirMes_Click(object sender, EventArgs e)
        {
            DialogResult ok = MessageBox.Show("Você quer deletar os pagamentos do mês passado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ok == DialogResult.Yes)
            {
                ok = MessageBox.Show("Você quer REALMENTE deletar os pagamentos do mês passado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (ok == DialogResult.Yes)
                {
                    ok = MessageBox.Show("Por segurança e pela última vez, você quer deletar os pagamentos do mês passado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (ok == DialogResult.Yes)
                    {
                        ExcluirPagamentosMesPassado();
                    }
                }
            }
        }

        // Método para carregar dados de pagamento no DataGridView
        private void CarregarDados()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();
                    string query = "SELECT * FROM pagamento";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                    DataTable tabela = new DataTable();
                    adaptador.Fill(tabela);

                    dataGridView1.DataSource = tabela;

                    // Renomeia os cabeçalhos das colunas
                    dataGridView1.Columns["idvenda"].HeaderText = "ID da venda";
                    dataGridView1.Columns["id_produto"].HeaderText = "Código do produto";
                    dataGridView1.Columns["nomeproduto"].HeaderText = "Produto";
                    dataGridView1.Columns["quant"].HeaderText = "Quantidade";
                    dataGridView1.Columns["valortotal"].HeaderText = "Valor total";
                    dataGridView1.Columns["valorcomdesconto"].HeaderText = "Valor com desconto";
                    dataGridView1.Columns["pagtipo"].HeaderText = "Tipo de Pagamento";
                    dataGridView1.Columns["datacompra"].HeaderText = "Data da compra";


                    // Formata a coluna datacompra para mostrar data e hora
                    dataGridView1.Columns["datacompra"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                    // Esconde a coluna ID da simulação
                    dataGridView1.Columns["id_simulacao"].Visible = false;

                    // Formata a coluna de valorunidade como moeda (R$)
                    dataGridView1.Columns["valortotal"].DefaultCellStyle.Format = "C2";
                    dataGridView1.Columns["valortotal"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("pt-BR");
                    dataGridView1.Columns["valorcomdesconto"].DefaultCellStyle.Format = "C2";
                    dataGridView1.Columns["valorcomdesconto"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("pt-BR");

                    AtualizarTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados: " + ex.Message);
                }
                finally
                {
                    if (conexao != null && conexao.State == ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                }
            }
        }

        private void ExcluirPagamentosMesPassado()
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString))
            {
                try
                {
                    conexao.Open();

                    // Calcula o primeiro e o último dia do mês passado
                    DateTime primeiroDiaMesPassado = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                    DateTime ultimoDiaMesPassado = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);


                    // Primeiro, conta quantos pagamentos existem do mês passado ou mais antigos
                    string queryContar = "SELECT COUNT(*) FROM pagamento WHERE datacompra <= @fim";
                    MySqlCommand comandoContar = new MySqlCommand(queryContar, conexao);
                    comandoContar.Parameters.AddWithValue("@fim", ultimoDiaMesPassado);

                    int totalPagamentos = Convert.ToInt32(comandoContar.ExecuteScalar());

                    if (totalPagamentos == 0)
                    {
                        MessageBox.Show("Não há pagamentos do mês passado, tudo está atualizado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Busca os id_simulacao relacionados aos pagamentos que serão deletados
                    List<int> idsSimulacaoParaExcluir = new List<int>();
                    string querySelecionarIds = "SELECT DISTINCT id_simulacao FROM pagamento WHERE datacompra <= @fim";
                    using (MySqlCommand cmdSelecionar = new MySqlCommand(querySelecionarIds, conexao))
                    {
                        cmdSelecionar.Parameters.AddWithValue("@fim", ultimoDiaMesPassado);
                        using (MySqlDataReader reader = cmdSelecionar.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                    idsSimulacaoParaExcluir.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    // Se houver registros, deleta
                    string queryExcluir = "DELETE FROM pagamento WHERE datacompra <= @fim";
                    MySqlCommand comandoExcluir = new MySqlCommand(queryExcluir, conexao);
                    comandoExcluir.Parameters.AddWithValue("@fim", ultimoDiaMesPassado);

                    int linhasAfetadas = comandoExcluir.ExecuteNonQuery();

                    MessageBox.Show($"Foram excluídos {linhasAfetadas} pagamentos do mês passado ou anteriores.", "Exclusão Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Deleta os registros correspondentes da tabela simulacao
                    foreach (int idSimulacao in idsSimulacaoParaExcluir)
                    {
                        string queryExcluirSimulacao = "DELETE FROM simulacao WHERE idsimulacao = @id";
                        using (MySqlCommand comandoExcluirSimulacao = new MySqlCommand(queryExcluirSimulacao, conexao))
                        {
                            comandoExcluirSimulacao.Parameters.AddWithValue("@id", idSimulacao);
                            comandoExcluirSimulacao.ExecuteNonQuery();
                        }
                    }

                    // Atualiza os dados na tela
                    CarregarDados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados: " + ex.Message);
                }
                finally
                {
                    if (conexao != null && conexao.State == ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                }
            }
        }

        // Método para pesquisar pagamentos com base em um termo
        private void PesquisarPagamento(string termo)
        {
            using (MySqlConnection conexao = new MySqlConnection(conexaoString)) // Cria uma nova conexão com o banco de dados
            {
                try
                {
                    conexao.Open();

                    string query = "SELECT * FROM pagamento WHERE nomeproduto LIKE @termo OR DATE_FORMAT(datacompra, '%d/%m/%Y %H:%i:%s') LIKE @termo or pagtipo LIKE @termo";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@termo", "%" + termo + "%");


                    MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                    DataTable tabela = new DataTable();
                    adaptador.Fill(tabela);

                    dataGridView1.DataSource = tabela;

                    dataGridView1.Columns["idvenda"].HeaderText = "ID da venda";
                    dataGridView1.Columns["id_produto"].HeaderText = "Código do produto";
                    dataGridView1.Columns["nomeproduto"].HeaderText = "Produto";
                    dataGridView1.Columns["quant"].HeaderText = "Quantidade";
                    dataGridView1.Columns["valortotal"].HeaderText = "Valor total";
                    dataGridView1.Columns["valorcomdesconto"].HeaderText = "Valor com desconto";
                    dataGridView1.Columns["pagtipo"].HeaderText = "Tipo de Pagamento";
                    dataGridView1.Columns["datacompra"].HeaderText = "Data da compra";

                    AtualizarTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao pesquisar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); // Mensagem de erro
                }
                finally
                {
                    if (conexao != null && conexao.State == ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                }
            }
        }

        private void AtualizarTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["valorcomdesconto"].Value != DBNull.Value)
                {
                    total += Convert.ToDecimal(row.Cells["valorcomdesconto"].Value);
                }
            }

            lblTotal.Text = $"Total: {total.ToString("C2", new System.Globalization.CultureInfo("pt-BR"))}";
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