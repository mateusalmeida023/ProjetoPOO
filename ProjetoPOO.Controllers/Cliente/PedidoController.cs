using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO.Controllers;

public class PedidoController
{
    private readonly ProdutoRepositorio _produtoRepo;
    private readonly PedidoRepositorio _pedidoRepo;

    public PedidoController()
    {
        _produtoRepo = new ProdutoRepositorio();
        _pedidoRepo = new PedidoRepositorio();
    }

    public void RealizarPedido(Cliente cliente)
    {
        var produtos = _produtoRepo.BuscarProdutosDisponiveis();
        if (produtos.Count == 0)
        {
            Console.WriteLine("Não há produtos disponíveis para pedido.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Produtos disponíveis:");
        for (int i = 0; i < produtos.Count; i++)
        {
            var p = produtos[i];
            Console.WriteLine($"{i + 1} - {p.Nome} | Preço: R${p.Preco:F2} | Estoque: {p.Quantidade}");
        }
        var itens = new List<PedidoItem>();
        while (true)
        {
            Console.Write("Digite o número do produto para adicionar ao pedido (ou 0 para finalizar): ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 0 || opcao > produtos.Count)
            {
                Console.WriteLine("Opção inválida!");
                continue;
            }
            if (opcao == 0) break;
            var produto = produtos[opcao - 1];
            Console.Write($"Quantidade de '{produto.Nome}': ");
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0 || qtd > produto.Quantidade)
            {
                Console.WriteLine("Quantidade inválida!");
                continue;
            }
            itens.Add(new PedidoItem(qtd, produto.Preco * qtd));
            produto.Quantidade -= qtd;
        }
        if (itens.Count == 0)
        {
            Console.WriteLine("Nenhum item adicionado ao pedido.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }
        double total = itens.Sum(i => i.PrecoTotal);
        Console.WriteLine($"Valor total do pedido: R${total:F2}");
        Console.Write("Confirmar pedido? (s/n): ");
        var confirm = Console.ReadLine();
        if (confirm?.ToLower() != "s")
        {
            Console.WriteLine("Pedido cancelado.");
            Console.ReadKey();
            return;
        }
        var pedido = new Pedido
        {
            Numero = new Random().Next(1000, 9999),
            DataHoraPedido = DateTime.Now,
            Situacao = null, 
            PrecoFrete = 0, 
            Itens = itens,
            Cliente = cliente
        };
        cliente.Pedidos.Add(pedido);
        _pedidoRepo.Incluir(pedido);
        Console.WriteLine("Pedido realizado com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
} 