using ProjetoPOO.Controllers.Exceptions;
using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO.Controllers;

public class ProdutoController
{
    private ProdutoRepositorio _repo;
    public ProdutoController()
    {
        _repo = new ProdutoRepositorio();
    }

    private Fornecedor SelecionarFornecedor()
    {
        var fornecedorRepo = new FornecedorRepositorio();
        var fornecedores = fornecedorRepo.BuscarTodos();
        if (fornecedores.Count == 0)
        {
            Console.WriteLine("Nenhum fornecedor cadastrado. Cadastre um fornecedor antes de cadastrar produtos.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return null;
        }
        Console.Clear();
        Console.WriteLine("Selecione o fornecedor:");
        for (int i = 0; i < fornecedores.Count; i++)
        {
            var f = fornecedores[i];
            Console.WriteLine($"{i + 1}. {f.Nome} - {f.Email} - {f.Telefone}");
        }
        Console.Write("\nDigite o número do fornecedor: ");
        if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > fornecedores.Count)
        {
            Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            return null;
        }
        return fornecedores[opcao - 1];
    }

    public void IncluirProduto()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----INCLUIR PRODUTO----");
            
            Fornecedor fornecedor = SelecionarFornecedor();
            if (fornecedor == null)
            {
                return;
            }

            Console.Write("\nNome do Produto: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new NullException("O nome do produto não pode estar vazio.");
            }
            
            Console.Write("Preço do Produto: R$");
            if (!double.TryParse(Console.ReadLine(), out double preco) || preco <= 0)
            {
                throw new ValorException("Preço inválido. Digite um número maior que zero.");
            }

            Console.Write("Quantidade do Produto: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade < 0)
            {
                throw new ValorException("Quantidade inválida. Digite um número maior ou igual a zero.");
            }

            var novo = new Produto
            {
                Nome = nome,
                Preco = preco,
                Quantidade = quantidade,
                Fornecedor = fornecedor
            };
            _repo.Incluir(novo);

            Console.Clear();
            Console.WriteLine("Produto incluído com sucesso!");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Preço: R${preco:F2}");
            Console.WriteLine($"Quantidade: {quantidade}");
            Console.WriteLine($"Fornecedor: {fornecedor?.Nome}");
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

            var produtos = _repo.BuscarTodos();
            if (produtos.Count == 0)
            {
                Console.WriteLine("Não há nenhum produto para remover.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < produtos.Count; i++)
            {
                var produto = produtos[i];
                Console.WriteLine($"{i + 1}.  {produto.Nome} - R${produto.Preco:F2} - Qtd: {produto.Quantidade} - Fornecedor: {produto.Fornecedor?.Nome}");
            }
            Console.Write("\nDigite o número do produto que deseja remover: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > produtos.Count)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida! Digite um número da lista.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int index = opcao - 1;
            _repo.Excluir(index);

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

            var produtos = _repo.BuscarTodos();
            if (produtos.Count == 0)
            {
                Console.WriteLine("Não há nenhum produto cadastrado.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < produtos.Count; i++)
            {
                var produto = produtos[i];
                Console.WriteLine($"{i + 1}.  {produto.Nome} - R${produto.Preco:F2} - Qtd: {produto.Quantidade}");
                Console.WriteLine($"    Fornecedor: {produto.Fornecedor?.Nome}");
                Console.WriteLine();
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
            
            var produtos = _repo.BuscarTodos();
            if (produtos.Count == 0)
            {
                Console.WriteLine("Não há nenhum produto cadastrado para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Produtos cadastrados:");
            for (int i = 0; i < produtos.Count; i++)
            {
                var produto = produtos[i];
                Console.WriteLine($"{i + 1}. {produto.Nome} - R${produto.Preco:F2} - Qtd: {produto.Quantidade} - Fornecedor: {produto.Fornecedor?.Nome}");
            }

            Console.Write("\nDigite o número do produto que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > produtos.Count)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Produto produtoAtual = produtos[index];

            Console.Clear();
            Console.WriteLine($"Alterando dados do produto: {produtoAtual.Nome}");
            Console.WriteLine("\nDeixe em branco para manter o valor atual");
            
            Console.Write($"Nome do Produto ({produtoAtual.Nome}): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
            {
                produtoAtual.Nome = nome;
            }
            
            Console.Write($"Preço do Produto (R${produtoAtual.Preco:F2}): R$");
            string precoStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(precoStr))
            {
                if (!double.TryParse(precoStr, out double preco) || preco <= 0)
                {
                    throw new ValorException("Preço inválido. Digite um número maior que zero.");
                }
                produtoAtual.Preco = preco;
            }

            Console.Write($"Quantidade do Produto ({produtoAtual.Quantidade}): ");
            string quantidadeStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(quantidadeStr))
            {
                if (!int.TryParse(quantidadeStr, out int quantidade) || quantidade < 0)
                {
                    throw new ValorException("Quantidade inválida. Digite um número maior ou igual a zero.");
                }
                produtoAtual.Quantidade = quantidade;
            }

            Console.Write("\nDeseja alterar o fornecedor? (S/N): ");
            string alterarFornecedor = Console.ReadLine()?.ToUpper();
            
            if (alterarFornecedor == "S")
            {
                produtoAtual.Fornecedor = SelecionarFornecedor();
            }

            _repo.Alterar(index, produtoAtual);

            Console.Clear();
            Console.WriteLine("Produto alterado com sucesso!");
            Console.WriteLine($"Nome: {produtoAtual.Nome}");
            Console.WriteLine($"Preço: R${produtoAtual.Preco:F2}");
            Console.WriteLine($"Quantidade: {produtoAtual.Quantidade}");
            Console.WriteLine($"Fornecedor: {produtoAtual.Fornecedor?.Nome}");
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