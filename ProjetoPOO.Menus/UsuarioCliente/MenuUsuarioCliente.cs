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
                        var menuConsulta = new MenuConsultarPedido();
                        menuConsulta.Exibir(_clienteLogado);
                        break;
                    case 3:
                        CancelarPedidos();
                        break;
                    case 99:
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    private void ExibirProdutos(List<Produto> produtos)
    {
        Console.WriteLine("Produtos disponíveis:");
        for (int i = 0; i < produtos.Count; i++)
        {
            var p = produtos[i];
            string status = p.Quantidade > 0 ? $"Estoque: {p.Quantidade}" : "INDISPONÍVEL";
            string disponibilidade = p.Quantidade > 0 ? "" : " (não disponível)";
            Console.WriteLine($"{i + 1} - {p.Nome} | Preço: R${p.Preco:F2} | {status}{disponibilidade}");
        }
    }

    private void FazerPedido()
    {
        Console.Clear();
        var todosProdutos = _produtoRepo.BuscarTodosProdutos();
        var produtosDisponiveis = _produtoRepo.BuscarProdutosDisponiveis();
        
        if (todosProdutos.Count == 0)
        {
            Console.WriteLine("Não há produtos cadastrados no sistema.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        
        if (produtosDisponiveis.Count == 0)
        {
            Console.WriteLine("Não há produtos disponíveis para pedido.");
            Console.WriteLine("Todos os produtos estão com estoque zero.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        
        ExibirProdutos(todosProdutos);
        
        var itensPedido = new List<(Produto produto, int quantidade)>();
        double totalPedido = 0;
        
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"Total atual do pedido: R${totalPedido:F2}");
            Console.Write("Digite o número do produto para adicionar ao pedido (ou 0 para finalizar): ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 0 || opcao > todosProdutos.Count)
            {
                Console.WriteLine("Opção inválida!");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                ExibirProdutos(todosProdutos);
                continue;
            }
            if (opcao == 0) break;
            
            var produto = todosProdutos[opcao - 1];
            
            // Verificar se o produto está disponível
            if (produto.Quantidade <= 0)
            {
                Console.WriteLine($"Produto '{produto.Nome}' não está disponível (estoque: {produto.Quantidade})!");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                ExibirProdutos(todosProdutos);
                continue;
            }
            
            Console.Write($"Quantidade de '{produto.Nome}' (máximo: {produto.Quantidade}): ");
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0)
            {
                Console.WriteLine("Quantidade inválida! Digite um número maior que zero.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                ExibirProdutos(todosProdutos);
                continue;
            }
            
            // Validar se a quantidade solicitada está disponível
            if (qtd > produto.Quantidade)
            {
                Console.WriteLine($"Quantidade solicitada ({qtd}) excede o estoque disponível ({produto.Quantidade}).");
                Console.WriteLine($"Quantidade máxima disponível: {produto.Quantidade}");
                Console.Write("Deseja comprar a quantidade máxima disponível? (s/n): ");
                var confirmarMaximo = Console.ReadLine()?.ToLower();
                if (confirmarMaximo == "s" || confirmarMaximo == "sim")
                {
                    qtd = produto.Quantidade;
                }
                else
                {
                    Console.Clear();
                    ExibirProdutos(todosProdutos);
                    continue;
                }
            }
            
            // Verificar se o produto já foi adicionado ao pedido
            var itemExistente = itensPedido.FirstOrDefault(i => i.produto.Nome == produto.Nome);
            if (itemExistente.produto != null)
            {
                Console.WriteLine($"Produto '{produto.Nome}' já foi adicionado ao pedido.");
                Console.Write("Deseja alterar a quantidade? (s/n): ");
                var alterarQuantidade = Console.ReadLine()?.ToLower();
                if (alterarQuantidade == "s" || alterarQuantidade == "sim")
                {
                    itensPedido.Remove(itemExistente);
                    totalPedido -= itemExistente.produto.Preco * itemExistente.quantidade;
                }
                else
                {
                    Console.Clear();
                    ExibirProdutos(todosProdutos);
                    continue;
                }
            }
            
            itensPedido.Add((produto, qtd));
            totalPedido += produto.Preco * qtd;
            
            Console.WriteLine($"'{produto.Nome}' adicionado ao pedido: {qtd} unidade(s) - R${produto.Preco * qtd:F2}");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            ExibirProdutos(todosProdutos);
        }
        
        if (itensPedido.Count == 0)
        {
            Console.WriteLine("Nenhum item adicionado ao pedido.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        
        Console.WriteLine();
        Console.WriteLine("=== RESUMO DO PEDIDO ===");
        foreach (var item in itensPedido)
        {
            Console.WriteLine($"{item.produto.Nome}: {item.quantidade}x R${item.produto.Preco:F2} = R${item.produto.Preco * item.quantidade:F2}");
        }
        Console.WriteLine($"Total dos itens: R${totalPedido:F2}");
        Console.WriteLine();
        
        Console.Write("Confirmar pedido? (s/n): ");
        var confirm = Console.ReadLine();
        if (confirm?.ToLower() != "s")
        {
            Console.WriteLine("Pedido cancelado.");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        
        var pedido = _pedidoController.RealizarPedido(_clienteLogado, itensPedido);
        if (pedido != null)
        {
            Console.Clear();
            Console.WriteLine("Pedido realizado com sucesso!");
            Console.WriteLine(_pedidoController.GerarDetalhesPedido(pedido));
        }
        else
        {
            Console.WriteLine("Erro ao realizar pedido.");
        }
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
        Console.Clear();
    }

    private void CancelarPedidos()
    {
        Console.Clear();
        Console.WriteLine("----CANCELAR PEDIDOS----");

        var pedidos = _pedidoController.ConsultarPorCliente(_clienteLogado);
        if (pedidos == null || pedidos.Count == 0)
        {
            Console.WriteLine("Você não possui nenhum pedido.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        // Mostrar apenas pedidos que podem ser cancelados (NOVO ou TRANSPORTE)
        var pedidosCancelaveis = pedidos.Where(p => p.Situacao == Situacao.NOVO || p.Situacao == Situacao.TRANSPORTE).ToList();
        
        if (pedidosCancelaveis.Count == 0)
        {
            Console.WriteLine("Não há pedidos que possam ser cancelados.");
            Console.WriteLine("Apenas pedidos com situação NOVO ou TRANSPORTE podem ser cancelados.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        Console.WriteLine("Pedidos que podem ser cancelados:");
        foreach (var pedido in pedidosCancelaveis)
        {
            Console.WriteLine($"Nº {pedido.Numero} - {pedido.DataHoraPedido:dd/MM/yyyy HH:mm} - Situação: {pedido.Situacao}");
        }

        Console.Write("\nDigite o número do pedido que deseja cancelar: ");
        if (!int.TryParse(Console.ReadLine(), out int numeroPedido))
        {
            Console.WriteLine("Número inválido!");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        var pedidoParaCancelar = pedidosCancelaveis.FirstOrDefault(p => p.Numero == numeroPedido);
        if (pedidoParaCancelar == null)
        {
            Console.WriteLine("Pedido não encontrado ou não pode ser cancelado!");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        Console.Clear();
        Console.WriteLine($"Detalhes do pedido Nº {pedidoParaCancelar.Numero}:");
        Console.WriteLine(_pedidoController.GerarDetalhesPedido(pedidoParaCancelar));
        
        Console.Write("\nTem certeza que deseja cancelar este pedido? (s/n): ");
        var confirmacao = Console.ReadLine()?.ToLower();
        
        if (confirmacao == "s" || confirmacao == "sim")
        {
            if (_pedidoController.AlterarSituacaoPedido(pedidoParaCancelar.Numero, Situacao.CANCELADO))
            {
                Console.WriteLine("Pedido cancelado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao cancelar pedido!");
            }
        }
        else
        {
            Console.WriteLine("Cancelamento cancelado.");
        }

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }
}
