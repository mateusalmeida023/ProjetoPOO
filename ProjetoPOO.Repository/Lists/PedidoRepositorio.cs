using ProjetoPOO.Modelos;
using ProjetoPOO.Repository;
using System.Globalization;

namespace ProjetoPOO.Repository.Lists;

public class PedidoRepositorio : RepositorioBase<Pedido>
{
    public PedidoRepositorio() : base("pedidos.csv") { }

    public override string ToCsv(Pedido p)
    {
        // Serializa: Numero, DataHoraPedido, Cliente.Email, ValorTotal, Itens (nome:quantidade:preco|...)
        string itens = string.Join("|", p.Itens.Select(i => $"{i.Quantidade}:{i.PrecoTotal}"));
        return $"{p.Numero},{p.DataHoraPedido.ToString("o", CultureInfo.InvariantCulture)},{p.Cliente?.Email},{p.Itens.Sum(i => i.PrecoTotal)},{itens}";
    }

    public override Pedido FromCsv(string linha)
    {
        var partes = linha.Split(',');
        var pedido = new Pedido
        {
            Numero = int.Parse(partes[0]),
            DataHoraPedido = DateTime.Parse(partes[1], null, DateTimeStyles.RoundtripKind),
            // Cliente deve ser associado externamente se necessário
            Itens = new List<PedidoItem>()
        };
        if (partes.Length > 4)
        {
            var itensStr = partes[4].Split('|');
            foreach (var itemStr in itensStr)
            {
                var dados = itemStr.Split(':');
                if (dados.Length == 2)
                {
                    pedido.Itens.Add(new PedidoItem(int.Parse(dados[0]), double.Parse(dados[1])));
                }
            }
        }
        return pedido;
    }
} 