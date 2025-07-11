using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Arrays;

namespace ProjetoPOO.Repository.Arrays;

public class ExemploUsoArrays
{
    public static void ExecutarExemplo()
    {
        Console.WriteLine("=== Exemplo de Uso dos Repositórios com Arrays ===\n");

        // Criando instâncias dos repositórios com arrays
        var clienteRepo = new ClienteRepositorioArray();
        var produtoRepo = new ProdutoRepositorioArray();
        var fornecedorRepo = new FornecedorRepositorioArray();
        var pedidoRepo = new PedidoRepositorioArray();
        var transportadoraRepo = new TransportadoraRepositorioArray();

        // Exemplo: Adicionando um cliente
        var cliente = new Cliente
        {
            Nome = "João Silva",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "(11) 99999-9999",
            CPF = "123.456.789-00",
            Endereco = new Endereco("Rua das Flores", "123", "Centro", "São Paulo", "SP", "01234-567")
        };

        clienteRepo.Incluir(cliente);
        Console.WriteLine($"Cliente adicionado: {cliente.Nome}");

        // Exemplo: Adicionando um produto
        var produto = new Produto
        {
            Nome = "Notebook Dell",
            Preco = 3500.00,
            Quantidade = 10
        };

        produtoRepo.Incluir(produto);
        Console.WriteLine($"Produto adicionado: {produto.Nome} - R$ {produto.Preco:F2}");

        // Exemplo: Adicionando uma transportadora
        var transportadora = new Transportadora
        {
            Nome = "Correios Express",
            PrecoPorKm = 2.50,
            Endereco = new Endereco("Rua dos Correios", "456", "Centro", "São Paulo", "SP", "01234-000")
        };

        transportadoraRepo.Incluir(transportadora);
        Console.WriteLine($"Transportadora adicionada: {transportadora.Nome}");

        // Exemplo: Listando todos os clientes
        var todosClientes = clienteRepo.BuscarTodos();
        Console.WriteLine($"\nTotal de clientes: {todosClientes.Count}");
        foreach (var c in todosClientes)
        {
            Console.WriteLine($"- {c.Nome} ({c.Email})");
        }

        // Exemplo: Listando produtos disponíveis
        var produtosDisponiveis = produtoRepo.BuscarProdutosDisponiveis();
        Console.WriteLine($"\nProdutos disponíveis: {produtosDisponiveis.Count}");
        foreach (var p in produtosDisponiveis)
        {
            Console.WriteLine($"- {p.Nome}: {p.Quantidade} unidades - R$ {p.Preco:F2}");
        }

        // Exemplo: Alterando um produto
        if (produtosDisponiveis.Count > 0)
        {
            var produtoParaAlterar = produtosDisponiveis[0];
            produtoParaAlterar.Quantidade = 5;
            produtoRepo.Alterar(0, produtoParaAlterar);
            Console.WriteLine($"\nQuantidade do produto '{produtoParaAlterar.Nome}' alterada para {produtoParaAlterar.Quantidade}");
        }

        // Exemplo: Excluindo um item
        if (todosClientes.Count > 0)
        {
            clienteRepo.Excluir(0);
            Console.WriteLine($"\nPrimeiro cliente removido. Total restante: {clienteRepo.Count}");
        }

        Console.WriteLine("\n=== Fim do Exemplo ===");
    }
} 