using ProjetoPOO.Modelos;
using ProjetoPOO.Controllers;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO.Menus.UsuarioCliente;

public class MenuUsuarioCliente
{
    private readonly PedidoController _pedidoController;
    private readonly Cliente _clienteLogado;
    private readonly ProdutoRepositorio _produtoRepo;

    public MenuUsuarioCliente(Cliente cliente)
    {
        _clienteLogado = cliente;
        _pedidoController = new PedidoController();
        _produtoRepo = new ProdutoRepositorio();
    }

    public void ExibirMenu()
    {
        int opcao = -1;
        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----MENU CLIENTE----");
            Console.WriteLine("1 - Fazer Pedido");
            Console.WriteLine("2 - Consultar Pedidos");
            Console.WriteLine("3 - Cancelar Pedidos");
            Console.WriteLine("99 - Sair");
            Console.Write("Escolha uma opção: ");

            try
            {
                if (!int.TryParse(Console.ReadLine(), out opcao))
                    throw new Exception("Opção inválida! Digite apenas números.");

                switch (opcao)
                {
                    case 1:
                        FazerPedido();
                        break;
                    case 2:
                        // Aqui você pode chamar o menu de consulta de pedidos
                        var menuConsulta = new MenuConsultarPedido();
                        menuConsulta.Exibir(_clienteLogado);
                        break;
                    case 3:
                        // Cancelar pedido (implementar conforme padrão)
                        Console.WriteLine("Funcionalidade de cancelamento de pedido ainda não implementada.");
                        Console.ReadKey();
                        break;
                    case 99:
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.ReadKey();
            }
        }
    }

    private void FazerPedido()
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
        var itensPedido = new List<(Produto produto, int quantidade)>();
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
            itensPedido.Add((produto, qtd));
        }
        if (itensPedido.Count == 0)
        {
            Console.WriteLine("Nenhum item adicionado ao pedido.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }
        double total = itensPedido.Sum(i => i.produto.Preco * i.quantidade);
        Console.WriteLine($"Valor total do pedido: R${total:F2}");
        Console.Write("Confirmar pedido? (s/n): ");
        var confirm = Console.ReadLine();
        if (confirm?.ToLower() != "s")
        {
            Console.WriteLine("Pedido cancelado.");
            Console.ReadKey();
            return;
        }
        var pedido = _pedidoController.RealizarPedido(_clienteLogado, itensPedido);
        if (pedido != null)
        {
            Console.WriteLine("Pedido realizado com sucesso!");
            Console.WriteLine(_pedidoController.GerarDetalhesPedido(pedido));
        }
        else
        {
            Console.WriteLine("Erro ao realizar pedido.");
        }
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}
