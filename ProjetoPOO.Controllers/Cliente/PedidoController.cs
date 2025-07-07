using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Lists;
using System.Globalization;

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

    public Pedido? RealizarPedido(Cliente cliente, List<(Produto produto, int quantidade)> itensPedido)
    {
        if (itensPedido == null || itensPedido.Count == 0)
            return null;
        var itens = new List<PedidoItem>();
        foreach (var (produto, qtd) in itensPedido)
        {
            if (qtd <= 0 || qtd > produto.Quantidade)
                continue;
            itens.Add(new PedidoItem(qtd, produto.Preco * qtd));
            produto.Quantidade -= qtd;
        }
        if (itens.Count == 0)
            return null;
        var pedido = new Pedido
        {
            Numero = new Random().Next(1000, 9999),
            DataHoraPedido = DateTime.Now,
            Situacao = Situacao.NOVO,
            PrecoFrete = 0,
            Itens = itens,
            Cliente = cliente
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
        sb.AppendLine($"Total do pedido: R${(totalItens + pedido.PrecoFrete):F2}");
        return sb.ToString();
    }
} 