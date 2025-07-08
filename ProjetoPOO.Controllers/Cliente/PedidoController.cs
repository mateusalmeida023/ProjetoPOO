using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Lists;
using System.Globalization;

namespace ProjetoPOO.Controllers;

public class PedidoController
{
    private readonly ProdutoRepositorio _produtoRepo;
    private readonly PedidoRepositorio _pedidoRepo;
    private readonly TransportadoraRepositorio _transportadoraRepo;

    public PedidoController()
    {
        _produtoRepo = new ProdutoRepositorio();
        _pedidoRepo = new PedidoRepositorio();
        _transportadoraRepo = new TransportadoraRepositorio();
    }

    // Método para calcular distância aproximada entre dois endereços
    private double CalcularDistancia(Endereco origem, Endereco destino)
    {
        // Cálculo simplificado baseado em coordenadas aproximadas
        // Em um sistema real, seria usado uma API de geocoding
        if (origem?.Cidade == destino?.Cidade)
        {
            return 10.0; // 10km para mesma cidade
        }
        else if (origem?.Estado == destino?.Estado)
        {
            return 100.0; // 100km para mesmo estado
        }
        else
        {
            return 500.0; // 500km para estados diferentes
        }
    }

    // Método para selecionar transportadora
    private Transportadora SelecionarTransportadora(Endereco origem, Endereco destino)
    {
        var transportadoras = _transportadoraRepo.BuscarTodos();
        if (transportadoras.Count == 0)
        {
            return null;
        }

        Console.WriteLine("Transportadoras disponíveis:");
        for (int i = 0; i < transportadoras.Count; i++)
        {
            var t = transportadoras[i];
            double distancia = CalcularDistancia(t.Endereco, destino);
            double frete = distancia * t.PrecoPorKm;
            Console.WriteLine($"{i + 1} - {t.Nome} | R${t.PrecoPorKm:F2}/km | Frete: R${frete:F2}");
        }

        Console.Write("Escolha a transportadora (número): ");
        if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > transportadoras.Count)
        {
            Console.WriteLine("Opção inválida! Usando primeira transportadora.");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            return transportadoras[0];
        }

        return transportadoras[opcao - 1];
    }

    // Método para calcular frete
    private double CalcularFrete(Transportadora transportadora, Endereco origem, Endereco destino)
    {
        if (transportadora == null) return 0.0;
        
        double distancia = CalcularDistancia(transportadora.Endereco, destino);
        return distancia * transportadora.PrecoPorKm;
    }

    public Pedido? RealizarPedido(Cliente cliente, List<(Produto produto, int quantidade)> itensPedido)
    {
        if (itensPedido == null || itensPedido.Count == 0)
            return null;
        var itens = new List<PedidoItem>();
        var produtoRepo = new ProjetoPOO.Repository.Lists.ProdutoRepositorio();
        // Validar e processar cada item do pedido
        foreach (var (produto, qtd) in itensPedido)
        {
            if (qtd <= 0) continue;
            if (qtd > produto.Quantidade) continue;
            itens.Add(new PedidoItem(qtd, produto.Preco * qtd));
            // Atualizar estoque do produto no repositório
            var todosProdutos = produtoRepo.BuscarTodosProdutos();
            var produtoRepoItem = todosProdutos.FirstOrDefault(p => p.Nome == produto.Nome);
            if (produtoRepoItem != null)
            {
                produtoRepoItem.Quantidade -= qtd;
                int index = todosProdutos.FindIndex(p => p.Nome == produto.Nome);
                if (index >= 0)
                {
                    produtoRepo.Alterar(index, produtoRepoItem);
                }
            }
        }
        if (itens.Count == 0)
        {
            return null;
        }
        // Selecionar transportadora e calcular frete
        Console.Clear();
        var transportadora = SelecionarTransportadora(null, cliente.Endereco);
        double frete = CalcularFrete(transportadora, null, cliente.Endereco);
        var pedido = new Pedido
        {
            Numero = new Random().Next(1000, 9999),
            DataHoraPedido = DateTime.Now,
            Situacao = Situacao.NOVO,
            PrecoFrete = frete,
            Itens = itens,
            Cliente = cliente,
            Transportadora = transportadora
        };
        cliente.Pedidos.Add(pedido);
        _pedidoRepo.Incluir(pedido);
        return pedido;
    }

    public Pedido? ConsultarPorNumero(Cliente cliente, int numero)
    {
        return cliente.Pedidos.FirstOrDefault(p => p.Numero == numero);
    }

    public List<Pedido> ConsultarPorData(Cliente cliente, DateTime dataIni, DateTime dataFim)
    {
        return cliente.Pedidos.Where(p => p.DataHoraPedido.Date >= dataIni.Date && p.DataHoraPedido.Date <= dataFim.Date).ToList();
    }

    public List<Pedido> ConsultarPorCliente(Cliente cliente)
    {
        if (cliente != null && cliente.Pedidos != null)
        {
            return cliente.Pedidos.ToList();
        }
        return new List<Pedido>();
    }   

    public string GerarDetalhesPedido(Pedido pedido)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Pedido Nº: {pedido.Numero}");
        sb.AppendLine($"Data: {pedido.DataHoraPedido:dd/MM/yyyy HH:mm}");
        sb.AppendLine($"Situação: {pedido.Situacao}");
        sb.AppendLine("Itens:");
        foreach (var item in pedido.Itens)
        {
            sb.AppendLine($"- Quantidade: {item.Quantidade} | Preço unitário: R${(item.PrecoTotal/item.Quantidade):F2} | Preço total: R${item.PrecoTotal:F2}");
        }
        double totalItens = pedido.Itens.Sum(i => i.PrecoTotal);
        sb.AppendLine($"Total dos itens: R${totalItens:F2}");
        sb.AppendLine($"Frete: R${pedido.PrecoFrete:F2}");
        if (pedido.Transportadora != null)
        {
            sb.AppendLine($"Transportadora: {pedido.Transportadora.Nome}");
        }
        sb.AppendLine($"Total do pedido: R${(totalItens + pedido.PrecoFrete):F2}");
        return sb.ToString();
    }

    public bool AlterarSituacaoPedido(int numeroPedido, Situacao novaSituacao)
    {
        var pedidos = _pedidoRepo.BuscarTodos();
        var pedido = pedidos.FirstOrDefault(p => p.Numero == numeroPedido);
        
        if (pedido != null)
        {
            pedido.Situacao = novaSituacao;
            _pedidoRepo.Salvar();
            return true;
        }
        return false;
    }

    public List<Pedido> BuscarTodosPedidos()
    {
        return _pedidoRepo.BuscarTodos();
    }

    // Métodos de menu para interação direta com o usuário (antes estavam no menu)
    public void MenuConsultaPorNumero(Cliente cliente)
    {
        Console.Clear();
        var pedidos = ConsultarPorCliente(cliente);
        if (pedidos == null || pedidos.Count == 0)
        {
            Console.WriteLine("Você não possui nenhum pedido.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Seus pedidos:");
        foreach (var p in pedidos)
        {
            Console.WriteLine($"- Pedido Nº {p.Numero} - {p.DataHoraPedido:dd/MM/yyyy HH:mm}");
        }
        Console.WriteLine();
        Console.Write("Digite o número do pedido: ");
        if (!int.TryParse(Console.ReadLine(), out int numero))
        {
            Console.WriteLine("Número inválido!");
            Console.ReadKey();
            return;
        }
        var pedido = ConsultarPorNumero(cliente, numero);
        if (pedido == null)
        {
            Console.WriteLine("Pedido não encontrado!");
            Console.ReadKey();
            return;
        }
        Console.Clear();
        Console.WriteLine(GerarDetalhesPedido(pedido));
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public void MenuConsultaPorData(Cliente cliente)
    {
        Console.Clear();
        Console.Write("Data inicial (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dataIni))
        {
            Console.WriteLine("Data inválida!");
            Console.ReadKey();
            return;
        }
        Console.Write("Data final (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dataFim))
        {
            Console.WriteLine("Data inválida!");
            Console.ReadKey();
            return;
        }
        var pedidos = ConsultarPorData(cliente, dataIni, dataFim);
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido encontrado no período.");
            Console.ReadKey();
            return;
        }
        foreach (var pedido in pedidos)
        {
            Console.Clear();
            Console.WriteLine(GerarDetalhesPedido(pedido));
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public void MenuConsultaTodosPedidos(Cliente cliente)
    {
        Console.Clear();
        var pedidos = ConsultarPorCliente(cliente);
        if (pedidos == null || pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido encontrado.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }
        for (int i = 0; i < pedidos.Count; i++)
        {
            Console.Clear();
            Console.WriteLine($"----PEDIDO {i + 1} DE {pedidos.Count}----");
            Console.WriteLine(GerarDetalhesPedido(pedidos[i]));
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public void MenuFazerPedido(Cliente cliente)
    {
        var produtoRepo = new ProjetoPOO.Repository.Lists.ProdutoRepositorio();
        Console.Clear();
        var todosProdutos = produtoRepo.BuscarTodosProdutos();
        var produtosDisponiveis = produtoRepo.BuscarProdutosDisponiveis();
        if (todosProdutos.Count == 0)
        {
            Console.WriteLine("Não há produtos cadastrados no sistema.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }
        if (produtosDisponiveis.Count == 0)
        {
            Console.WriteLine("Não há produtos disponíveis para pedido.");
            Console.WriteLine("Todos os produtos estão com estoque zero.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
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
            return;
        }
        var pedido = RealizarPedido(cliente, itensPedido);
        if (pedido != null)
        {
            Console.Clear();
            Console.WriteLine("Pedido realizado com sucesso!");
            Console.WriteLine(GerarDetalhesPedido(pedido));
        }
        else
        {
            Console.WriteLine("Erro ao realizar pedido.");
        }
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
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

    public void MenuCancelarPedidos(Cliente cliente)
    {
        Console.Clear();
        Console.WriteLine("----CANCELAR PEDIDOS----");
        var pedidos = ConsultarPorCliente(cliente);
        if (pedidos == null || pedidos.Count == 0)
        {
            Console.WriteLine("Você não possui nenhum pedido.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }
        var pedidosCancelaveis = pedidos.Where(p => p.Situacao == Situacao.NOVO || p.Situacao == Situacao.TRANSPORTE).ToList();
        if (pedidosCancelaveis.Count == 0)
        {
            Console.WriteLine("Não há pedidos que possam ser cancelados.");
            Console.WriteLine("Apenas pedidos com situação NOVO ou TRANSPORTE podem ser cancelados.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
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
            return;
        }
        var pedidoParaCancelar = pedidosCancelaveis.FirstOrDefault(p => p.Numero == numeroPedido);
        if (pedidoParaCancelar == null)
        {
            Console.WriteLine("Pedido não encontrado ou não pode ser cancelado!");
            Console.ReadKey();
            return;
        }
        Console.Clear();
        Console.WriteLine($"Detalhes do pedido Nº {pedidoParaCancelar.Numero}:");
        Console.WriteLine(GerarDetalhesPedido(pedidoParaCancelar));
        Console.Write("\nTem certeza que deseja cancelar este pedido? (s/n): ");
        var confirmacao = Console.ReadLine()?.ToLower();
        if (confirmacao == "s" || confirmacao == "sim")
        {
            if (AlterarSituacaoPedido(pedidoParaCancelar.Numero, Situacao.CANCELADO))
            {
                Console.WriteLine("Pedido cancelado com sucesso!");
                // Recarregar pedidos do cliente após o cancelamento
                var todosPedidos = _pedidoRepo.BuscarTodos();
                cliente.Pedidos = todosPedidos.Where(p => p.Cliente?.Email == cliente.Email).ToList();
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
    }
} 