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
        var produtosAtualizados = new List<Produto>();
        
        // Validar e processar cada item do pedido
        foreach (var (produto, qtd) in itensPedido)
        {
            // Verificar se a quantidade é válida
            if (qtd <= 0)
            {
                continue;
            }
            
            // Verificar se há estoque suficiente
            if (qtd > produto.Quantidade)
            {
                continue;
            }
            
            // Criar item do pedido
            itens.Add(new PedidoItem(qtd, produto.Preco * qtd));
            
            // Atualizar estoque do produto
            produto.Quantidade -= qtd;
            produtosAtualizados.Add(produto);
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
        
        // Salvar o pedido
        cliente.Pedidos.Add(pedido);
        _pedidoRepo.Incluir(pedido);
        
        // Atualizar estoque dos produtos no repositório
        foreach (var produto in produtosAtualizados)
        {
            _produtoRepo.Salvar();
        }
        
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
} 