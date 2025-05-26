namespace ProjetoPOO.Controllers;

using ProjetoPOO.Data;
using ProjetoPOO.Modelos;

public class ProdutoController
{
    public void IncluirProduto()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----INCLUIR PRODUTO----");
            Console.Write("Nome do Produto: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("O nome do produto não pode estar vazio.");
            }

            Console.Write("Descrição do Produto: ");
            string descricao = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new Exception("A descrição do produto não pode estar vazia.");
            }

            Console.Write("Preço do Produto: R$");
            if (!double.TryParse(Console.ReadLine(), out double preco) || preco <= 0)
            {
                throw new Exception("Preço inválido. Digite um número maior que zero.");
            }

            Console.Write("Quantidade do Produto: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade < 0)
            {
                throw new Exception("Quantidade inválida. Digite um número maior ou igual a zero.");
            }

            var novo = new Produto
            {
                Nome = nome,
                Descricao = descricao,
                Preco = preco,
                Quantidade = quantidade
            };
            Data.Produtos[Data.ProdutosCount++] = novo;

            Console.Clear();
            Console.WriteLine("Produto incluído com sucesso!");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Descrição: {descricao}");
            Console.WriteLine($"Preço: R${preco:F2}");
            Console.WriteLine($"Quantidade: {quantidade}");
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

    public void ExcluirProduto()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----EXCLUIR PRODUTO----");

            if (Data.ProdutosCount == 0)
            {
                Console.WriteLine("Não há nenhum produto para remover.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < Data.ProdutosCount; i++)
            {
                Console.WriteLine($"{i + 1}.  {Data.Produtos[i].Nome} - R${Data.Produtos[i].Preco:F2} - Qtd: {Data.Produtos[i].Quantidade}");
            }
            Console.Write("\nDigite o número do produto que deseja remover: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > Data.ProdutosCount)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida! Digite um número da lista.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int index = opcao - 1;
            
            for (int i = index; i < Data.ProdutosCount - 1; i++)
            {
                Data.Produtos[i] = Data.Produtos[i + 1];
            }

            Data.ProdutosCount--;
            Data.Produtos[Data.ProdutosCount] = null;

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

    public void ConsultarProdutos()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----CONSULTAR PRODUTOS----");

            if (Data.ProdutosCount == 0)
            {
                Console.WriteLine("Não há nenhum produto cadastrado.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < Data.ProdutosCount; i++)
            {
                Console.WriteLine($"{i + 1}.  {Data.Produtos[i].Nome} - R${Data.Produtos[i].Preco:F2} - Qtd: {Data.Produtos[i].Quantidade}");
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

    public void AlterarProduto()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----ALTERAR PRODUTO----");
            
            if (Data.ProdutosCount == 0)
            {
                Console.WriteLine("Não há nenhum produto cadastrado para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Produtos cadastrados:");
            for (int i = 0; i < Data.ProdutosCount; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.Produtos[i].Nome} - R${Data.Produtos[i].Preco:F2} - Qtd: {Data.Produtos[i].Quantidade}");
            }

            Console.Write("\nDigite o número do produto que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > Data.ProdutosCount)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Produto produtoAtual = Data.Produtos[index];

            Console.Clear();
            Console.WriteLine($"Alterando dados do produto: {produtoAtual.Nome}");
            Console.WriteLine("\nDeixe em branco para manter o valor atual");
            
            Console.Write($"Nome do Produto ({produtoAtual.Nome}): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
            {
                produtoAtual.Nome = nome;
            }

            Console.Write($"Descrição do Produto ({produtoAtual.Descricao}): ");
            string descricao = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(descricao))
            {
                produtoAtual.Descricao = descricao;
            }

            Console.Write($"Preço do Produto (R${produtoAtual.Preco:F2}): R$");
            string precoStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(precoStr))
            {
                if (!double.TryParse(precoStr, out double preco) || preco <= 0)
                {
                    throw new Exception("Preço inválido. Digite um número maior que zero.");
                }
                produtoAtual.Preco = preco;
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

            Console.Clear();
            Console.WriteLine("Produto alterado com sucesso!");
            Console.WriteLine($"Nome: {produtoAtual.Nome}");
            Console.WriteLine($"Descrição: {produtoAtual.Descricao}");
            Console.WriteLine($"Preço: R${produtoAtual.Preco:F2}");
            Console.WriteLine($"Quantidade: {produtoAtual.Quantidade}");
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