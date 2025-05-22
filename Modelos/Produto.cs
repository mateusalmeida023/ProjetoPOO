namespace ProjetoPOO.Modelos;

public class Produto
{
    public string Nome { get; set; }
    public double Valor { get; set; }
    public int Quantidade { get; set; }
    public Fornecedor Fornecedor { get; set; }
    
    public static Produto[] produto =  new Produto[100];
    public static int produtoCount = 0;

    public Produto(string nome, double valor, int quantidade)
    {
        Nome = nome;
        Valor = valor;
        Quantidade = quantidade;
    }
    
    public static void CadastroProdutos()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----PRODUTO----");
            Console.WriteLine("1 - Incluir produto");
            Console.WriteLine("2 - Alterar produto");
            Console.WriteLine("3 - Excluir produto");
            Console.WriteLine("4 - Buscar produto");
            Console.WriteLine("99 - Voltar");
            Console.Write("Escolha uma opção: ");

            try
            {
                opcao = int.Parse(Console.ReadLine());
                
                if (opcao == 1)
                {
                    IncluirProduto();
                }
                else if (opcao == 2)
                {
                    AlterarProduto();
                }
                else if (opcao == 3)
                {
                    ExcluirProduto();
                }
                else if (opcao == 4)
                { 
                    ConsultarProdutos();
                }
                else if (opcao == 99)
                {
                    return;
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida! Digite apenas números.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    public static void IncluirProduto()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----INCLUIR PRODUTO----");
            
            Console.Write("Nome do Produto: ");
            string nome = Console.ReadLine();
            
            Console.Write("Valor do Produto: R$");
            double valor = double.Parse(Console.ReadLine());
            
            Console.Write("Quantidade do Produto: ");
            int quantidade = int.Parse(Console.ReadLine());
            
            if (Fornecedor.fornecedoresCount == 0)
            {
                Console.Clear();
                Console.WriteLine("Nenhum fornecedor cadastrado. Cadastre um fornecedor primeiro.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("----SELECIONE O FORNECEDOR----");
            for (int i = 0; i < Fornecedor.fornecedoresCount; i++)
            {
                Console.WriteLine($"{i + 1}. {Fornecedor.fornecedores[i].Nome}");
            }

            Console.Write("Opção: ");
            int opcao = int.Parse(Console.ReadLine());

            int id = produtoCount + 1;
            Produto novo = new Produto(nome, valor, quantidade);
            novo.Fornecedor = Fornecedor.fornecedores[opcao - 1];
            produto[produtoCount++] = novo;
            
            Console.Clear();
            Console.WriteLine("Produto incluído com sucesso!");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Valor: R${valor:F2}");
            Console.WriteLine($"Quantidade: {quantidade}");
            Console.WriteLine($"Fornecedor: {novo.Fornecedor.Nome}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public static void ExcluirProduto()
    {
        try
        {   
            Console.Clear();
            Console.WriteLine("----REMOVER PRODUTO----");
            
            if (produtoCount == 0)
            {
                Console.WriteLine("Não há nenhum produto para remover.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Produtos cadastrados:");
            for (int i = 0; i < produtoCount; i++)
            {
                Console.WriteLine($"{i + 1}. {produto[i].Nome} - R${produto[i].Valor:F2} - Qtd: {produto[i].Quantidade}");
            }

            Console.Write("\nDigite o número do produto que deseja remover: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > produtoCount)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida! Digite um número da lista.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int index = opcao - 1;
            
            for (int i = index; i < produtoCount - 1; i++)
            {
                produto[i] = produto[i + 1];
            }

            produtoCount--;
            produto[produtoCount] = null;

            Console.Clear();
            Console.WriteLine("Produto removido com sucesso!");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public static void ConsultarProdutos()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----CONSULTAR PRODUTOS----");

            if (produtoCount == 0)
            {
                Console.WriteLine("Não há nenhum produto para buscar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();

            for (int i = 0; i < produtoCount; i++)
            {
                Console.WriteLine($"{i + 1}. {produto[i].Nome}");
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        
    }

    public static void AlterarProduto()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----ALTERAR PRODUTO----");
            
            if (produtoCount == 0)
            {
                Console.WriteLine("Não há nenhum produto cadastrado para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Produtos cadastrados:");
            for (int i = 0; i < produtoCount; i++)
            {
                Console.WriteLine($"{i + 1}. {produto[i].Nome} - R${produto[i].Valor:F2} - Qtd: {produto[i].Quantidade}");
            }

            Console.Write("\nDigite o número do produto que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > produtoCount)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Produto produtoAtual = produto[index];

            Console.Clear();
            Console.WriteLine($"Alterando dados do produto: {produtoAtual.Nome}");
            Console.WriteLine("\nDeixe em branco para manter o valor atual");
            
            Console.Write($"Nome do Produto ({produtoAtual.Nome}): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
            {
                produtoAtual.Nome = nome;
            }

            Console.Write($"Valor do Produto (R${produtoAtual.Valor:F2}): R$");
            string valorStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(valorStr))
            {
                if (!double.TryParse(valorStr, out double valor) || valor <= 0)
                {
                    throw new Exception("Valor inválido. Digite um número maior que zero.");
                }
                produtoAtual.Valor = valor;
            }

            Console.Write($"Quantidade do Produto ({produtoAtual.Quantidade}): ");
            string quantidadeStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(quantidadeStr))
            {
                if (!int.TryParse(quantidadeStr, out int quantidade) || quantidade < 0)
                {
                    throw new Exception("Quantidade inválida. Digite um número maior ou igual a zero.");
                }
                produtoAtual.Quantidade = quantidade;
            }

            Console.Write("\nDeseja alterar o fornecedor? (S/N): ");
            string alterarFornecedor = Console.ReadLine()?.ToUpper();
            
            if (alterarFornecedor == "S")
            {
                if (Fornecedor.fornecedoresCount == 0)
                {
                    throw new Exception("Não há fornecedores cadastrados para selecionar.");
                }

                Console.Clear();
                Console.WriteLine("----SELECIONE O NOVO FORNECEDOR----");
                for (int i = 0; i < Fornecedor.fornecedoresCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {Fornecedor.fornecedores[i].Nome}");
                }

                Console.Write("Opção: ");
                if (!int.TryParse(Console.ReadLine(), out int fornecedorOpcao) || 
                    fornecedorOpcao < 1 || fornecedorOpcao > Fornecedor.fornecedoresCount)
                {
                    throw new Exception("Opção inválida! Digite um número da lista.");
                }

                produtoAtual.Fornecedor = Fornecedor.fornecedores[fornecedorOpcao - 1];
            }

            Console.Clear();
            Console.WriteLine("Produto alterado com sucesso!");
            Console.WriteLine($"Nome: {produtoAtual.Nome}");
            Console.WriteLine($"Valor: R${produtoAtual.Valor:F2}");
            Console.WriteLine($"Quantidade: {produtoAtual.Quantidade}");
            Console.WriteLine($"Fornecedor: {produtoAtual.Fornecedor.Nome}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
