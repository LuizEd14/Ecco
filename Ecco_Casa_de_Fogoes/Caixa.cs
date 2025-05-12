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
    public partial class frmCaixa : Form // Define a classe do formulário
    {
        // Variáveis para controle de valores
        private decimal totalCompra = 0; // Total da compra
        private decimal valorComDesconto = 0; // Valor total com desconto aplicado
        private decimal descontoAplicado = 0; // Valor do desconto aplicado

        // String de conexão com o banco de dados
        string conexaoString = "server=localhost; user=root; password=; database=ecco";

        // Construtor do formulário
        public frmCaixa()
        {
            InitializeComponent(); // Inicializa os componentes do formulário

            // Configurações do DataGridView
            dataGridView1.DefaultCellStyle.Font = new Font("Montserrat", 12); // Fonte das células
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 18, FontStyle.Bold); // Fonte dos cabeçalhos
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Alinhamento das células
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Alinhamento dos cabeçalhos
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FAC100"); // Cor de fundo dos cabeçalhos
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1723A6"); // Cor do texto dos cabeçalhos
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.EnableHeadersVisualStyles = false; // Desabilita estilos visuais padrão

            // Arredonda os botões
            ArredondarBotao(btnAdicionar, 45);
            ArredondarBotao(btnCancelar, 45);
            ArredondarBotao(btnFinalizar, 45);

            cbTipoPag.SelectedIndex = 0;
        }

        // Evento que ocorre ao carregar o formulário
        private void frmCaixa_Load(object sender, EventArgs e)
        {
            // Adiciona colunas ao DataGridView
            dataGridView1.Columns.Add("nomeProduto", "Produto");
            dataGridView1.Columns.Add("quantidade", "Quantidade");
            dataGridView1.Columns.Add("valorUnitario", "Valor Unitário");
            dataGridView1.Columns.Add("valorTotal", "Valor Total");

            // Adiciona coluna de botão para remover itens
            DataGridViewButtonColumn btnRemover = new DataGridViewButtonColumn
            {
                Name = "Remover",
                HeaderText = "Ação",
                Text = "Remover",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(btnRemover);

            dataGridView1.AllowUserToAddRows = false; // Desabilita a adição de novas linhas pelo usuário
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

        // Método para abrir o histórico
        public void Historio(object sender, EventArgs e)
        {
            frmHisto cadastro = new frmHisto();
            cadastro.Show(); // Exibe o formulário de histórico
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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipoPag.SelectedItem.ToString() == "Dinheiro")
            {
                panel5.Visible = true;
                lblDinDin.Visible = true;
                lblMostTroco.Visible = true;
                txtDinheiro.Visible = true;
            }
            else
            {
                panel5.Visible = false;
                lblDinDin.Visible = false;
                lblMostTroco.Visible = false;
                txtDinheiro.Visible = false;
            }
        }


        // Método para adicionar um produto ao carrinho
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // Verifica se o ID do produto e a quantidade são válidos
            if (int.TryParse(txtID.Text, out int idProduto) && int.TryParse(txtQuantidade.Text, out int quantidade))
            {
                using (MySqlConnection conn = new MySqlConnection(conexaoString))
                {
                    try
                    {
                        conn.Open(); // Abre a conexão com o banco de dados
                        string query = "SELECT produto, valorunidade, quant FROM produto WHERE idproduto = @id"; // Consulta para buscar o produto
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", idProduto); // Adiciona o parâmetro da consulta

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Verifica se o produto foi encontrado
                            if (reader.Read())
                            {
                                string nomeProduto = reader.GetString("produto"); // Obtém o nome do produto
                                decimal valorUnitario = reader.GetDecimal("valorunidade"); // Obtém o valor unitário
                                int estoqueAtual = reader.GetInt32("quant"); // Obtém a quantidade em estoque

                                // Verifica se a quantidade solicitada é maior que a disponível
                                if (quantidade > estoqueAtual)
                                {
                                    MessageBox.Show("Estoque insuficiente! Quantidade disponível: " + estoqueAtual, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtQuantidade.Clear();
                                    txtQuantidade.Focus();
                                    return;
                                }
                                else
                                {
                                    decimal valorTotal = valorUnitario * quantidade; // Calcula o valor total

                                    // Adiciona uma nova linha ao DataGridView
                                    dataGridView1.Rows.Add(
                                        nomeProduto,
                                        quantidade,
                                        valorUnitario.ToString("C"), // Formata o valor unitário como moeda
                                        valorTotal.ToString("C") // Formata o valor total como moeda
                                    );

                                    AtualizarTotal(); // Atualiza o total da compra
                                }
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado!"); // Mensagem de erro se o produto não for encontrado
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar dados: " + ex.Message);
                    }
                    finally
                    {
                        if (conn != null && conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("ID ou Quantidade inválida!"); // Mensagem de erro se os dados forem inválidos
            }

            // Limpa os campos de entrada
            txtID.Clear();
            txtQuantidade.Clear();
            txtID.Focus(); // Foca no campo ID
        }

        // Método para cancelar a compra
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Deseja cancelar toda a compra?", // Pergunta de confirmação
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Se o usuário confirmar, limpa os dados
            if (resultado == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear(); // Limpa as linhas do DataGridView
                txtDinheiro.Clear(); // Limpa o campo de dinheiro
                txtDesconto.Clear(); // Limpa o campo de desconto
                lblTroco.Text = "R$0,00"; // Reseta o troco
                lblTotal.Text = "Total: R$0,00"; // Reseta o total
            }
        }

        // Evento que ocorre ao pressionar a tecla Enter no campo de desconto
        private void txtDesconto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AtualizarTotal(); // Atualiza o total ao pressionar Enter

                e.Handled = true; // Evita o som de beep
                e.SuppressKeyPress = true; // Evita o som de beep
            }
        }

        // Evento que ocorre ao pressionar a tecla Enter no campo de dinheiro
        private void txtDinheiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Verifica se o valor recebido é válido
                if (string.IsNullOrWhiteSpace(txtDinheiro.Text) || !decimal.TryParse(txtDinheiro.Text, out decimal dinheiroRecebido) || dinheiroRecebido <= 0)
                {
                    lblTroco.Text = "R$0,00"; // Reseta o troco se o valor for inválido
                    return;
                }

                // Verifica se o dinheiro recebido é suficiente
                if (dinheiroRecebido < valorComDesconto)
                {
                    MessageBox.Show("Dinheiro insuficiente para a compra!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblTroco.Text = $"Faltam: R${(valorComDesconto - dinheiroRecebido):0.00}"; // Mostra quanto falta
                }
                else
                {
                    decimal troco = dinheiroRecebido - valorComDesconto; // Calcula o troco
                    lblTroco.Text = $"Troco: R${troco:0.00}"; // Exibe o troco
                }
            }
        }

        // Evento que ocorre ao clicar em uma célula do DataGridView
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se a coluna clicada é a de remover e se a linha é válida
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Remover" && e.RowIndex >= 0)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex); // Remove a linha selecionada
                AtualizarTotal(); // Atualiza o total após a remoção
            }
        }

        // Método para finalizar a compra
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoString))
            {
                // Verifica se há itens no carrinho
                if (dataGridView1.Rows.Count <= 0)
                {
                    MessageBox.Show("Nenhum item no caixa para finalizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica se o tipo de pagamento foi selecionado
                if (cbTipoPag.SelectedItem == null || cbTipoPag.SelectedItem.ToString() == "Escolha o tipo...")
                {
                    MessageBox.Show("Por favor, selecione um tipo de pagamento válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tipoPagamento = cbTipoPag.SelectedItem.ToString(); // Salva o tipo de pagamento

                try
                {
                    DialogResult resultado = MessageBox.Show(
                        "Deseja Finalizar a compra?",
                        "Confirmação",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (resultado == DialogResult.Yes)
                    {

                        conn.Open();

                        decimal totalCompra = 0;

                        // Calcula o total da compra
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string valorStr = row.Cells["valorTotal"].Value?.ToString()?.Replace("R$", "").Trim();
                            if (decimal.TryParse(valorStr, out decimal valorLinha))
                            {
                                totalCompra += valorLinha;
                            }
                        }

                        // Aplica desconto
                        decimal desconto = 0;
                        decimal valorComDesconto = totalCompra;
                        if (!string.IsNullOrWhiteSpace(txtDesconto.Text) && decimal.TryParse(txtDesconto.Text, out decimal valorDesconto) && valorDesconto > 0)
                        {
                            desconto = (valorDesconto / 100m) * totalCompra;
                            valorComDesconto = totalCompra - desconto;
                            if (valorComDesconto < 0) valorComDesconto = 0;
                        }

                        // Processa cada item do carrinho
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string nomeProduto = row.Cells["nomeProduto"].Value?.ToString();
                            string valorTotalStr = row.Cells["valorTotal"].Value?.ToString()?.Replace("R$", "").Trim();
                            string valorUnitarioStr = row.Cells["valorUnitario"].Value?.ToString()?.Replace("R$", "").Trim();
                            int quantidade = Convert.ToInt32(row.Cells["quantidade"].Value);

                            if (decimal.TryParse(valorTotalStr, out decimal valorTotal) &&
                                decimal.TryParse(valorUnitarioStr, out decimal valorUnitario))
                            {
                                // Busca o ID e o estoque do produto
                                string buscarId = "SELECT idproduto, quant FROM produto WHERE produto = @nome";
                                MySqlCommand cmdBuscar = new MySqlCommand(buscarId, conn);
                                cmdBuscar.Parameters.AddWithValue("@nome", nomeProduto);

                                int idProduto = 0;
                                int estoqueAtual = 0;

                                using (MySqlDataReader reader = cmdBuscar.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        idProduto = reader.GetInt32("idproduto");
                                        estoqueAtual = reader.GetInt32("quant");
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Produto '{nomeProduto}' não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }
                                }

                                // Atualiza o estoque
                                int novoEstoque = estoqueAtual - quantidade;
                                if (novoEstoque < 0) novoEstoque = 0;

                                string atualizarEstoque = "UPDATE produto SET quant = @novaQuant, data_alteracao = CURRENT_DATE WHERE idproduto = @id";
                                MySqlCommand cmdUpdate = new MySqlCommand(atualizarEstoque, conn);
                                cmdUpdate.Parameters.AddWithValue("@novaQuant", novoEstoque);
                                cmdUpdate.Parameters.AddWithValue("@id", idProduto);
                                cmdUpdate.ExecuteNonQuery();

                                // Insere na tabela simulacao
                                string insertSimulacao = "INSERT INTO simulacao (quant, id_produto) VALUES (@quant, @id_produto)";
                                MySqlCommand cmdSimulacao = new MySqlCommand(insertSimulacao, conn);
                                cmdSimulacao.Parameters.AddWithValue("@quant", quantidade);
                                cmdSimulacao.Parameters.AddWithValue("@id_produto", idProduto);
                                cmdSimulacao.ExecuteNonQuery();

                                long idSimulacao = cmdSimulacao.LastInsertedId;

                                decimal descontoProporcional = 0;
                                if (totalCompra > 0)
                                {
                                    descontoProporcional = (valorTotal / totalCompra) * desconto;
                                }

                                decimal valorFinal = valorTotal - descontoProporcional;
                                if (valorFinal < 0) valorFinal = 0;

                                // Insere na tabela Pagamento, agora incluindo o tipo de pagamento
                                string inserirPagamento = @"INSERT INTO Pagamento 
                            (quant, nomeproduto, valortotal, valorcomdesconto, pagtipo, id_produto, id_simulacao, datacompra) 
                            VALUES (@quant, @nomeproduto, @valortotal, @valorcomdesconto, @pagtipo, @idProduto, @idSimulacao, @datacompra)";

                                MySqlCommand cmdInsert = new MySqlCommand(inserirPagamento, conn);
                                cmdInsert.Parameters.AddWithValue("@quant", quantidade);
                                cmdInsert.Parameters.AddWithValue("@nomeproduto", nomeProduto);
                                cmdInsert.Parameters.AddWithValue("@valortotal", valorTotal);
                                cmdInsert.Parameters.AddWithValue("@valorcomdesconto", valorFinal);
                                cmdInsert.Parameters.AddWithValue("@pagtipo", tipoPagamento); // <<< Aqui é o novo parâmetro
                                cmdInsert.Parameters.AddWithValue("@idProduto", idProduto);
                                cmdInsert.Parameters.AddWithValue("@idSimulacao", idSimulacao);
                                cmdInsert.Parameters.AddWithValue("@datacompra", DateTime.Now);
                                cmdInsert.ExecuteNonQuery();
                            }
                        }


                        MessageBox.Show("Venda finalizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpa os dados após a venda
                        dataGridView1.Rows.Clear();
                        lblTotal.Text = "Total: R$0,00";
                        txtDesconto.Text = "";
                        txtDinheiro.Text = "";
                        lblTroco.Text = "Troco: R$0,00";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao finalizar a venda: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        // Método para arredondar os botões
        private void ArredondarBotao(Control btn, int borderRadius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90); // Adiciona arco no canto superior esquerdo
            path.AddArc(btn.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90); // Adiciona arco no canto superior direito
            path.AddArc(btn.Width - borderRadius, btn.Height - borderRadius, borderRadius, borderRadius, 0, 90); // Adiciona arco no canto inferior direito
            path.AddArc(0, btn.Height - borderRadius, borderRadius, borderRadius, 90, 90); // Adiciona arco no canto inferior esquerdo path.CloseFigure(); // Fecha a figura do caminho

            btn.Region = new Region(path); // Define a região do botão com o caminho arredondado
        }

        // Método para atualizar o total da compra
        private void AtualizarTotal()
        {
            totalCompra = 0; // Reseta o total da compra

            // Calcula o total com base nos itens do DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; // Ignora a nova linha

                if (row.Cells["valorTotal"].Value != null)
                {
                    string valorStr = row.Cells["valorTotal"].Value.ToString().Replace("R$", "").Trim(); // Obtém o valor total

                    if (decimal.TryParse(valorStr, out decimal valorLinha))
                    {
                        totalCompra += valorLinha; // Acumula o total
                    }
                }
            }

            descontoAplicado = 0; // Reseta o desconto aplicado
            valorComDesconto = totalCompra; // Inicializa o valor com desconto

            // Aplica desconto percentual se houver
            if (!string.IsNullOrWhiteSpace(txtDesconto.Text) &&
                decimal.TryParse(txtDesconto.Text, out decimal porcentagemDesconto) &&
                porcentagemDesconto > 0)
            {
                if (porcentagemDesconto > 100) porcentagemDesconto = 100; // Limita o desconto a 100%

                descontoAplicado = porcentagemDesconto; // Atualiza o desconto aplicado
                decimal descontoValor = totalCompra * (porcentagemDesconto / 100); // Calcula o valor do desconto
                valorComDesconto = totalCompra - (Math.Truncate(descontoValor * 100) / 100); // Aplica o desconto ao total

                lblTotal.Text = $"Total com {porcentagemDesconto}% de desconto: R${valorComDesconto:0.00}"; // Exibe o total com desconto
            }
            else
            {
                lblTotal.Text = $"Total: R${totalCompra:0.00}"; // Exibe o total sem desconto
            }
        }
    }
}