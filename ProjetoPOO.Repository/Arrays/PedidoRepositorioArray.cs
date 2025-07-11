using ProjetoPOO.Modelos;
using ProjetoPOO.Repository;
using ProjetoPOO.Repository.Arrays;
using System.Globalization;

namespace ProjetoPOO.Repository.Arrays;

public class PedidoRepositorioArray : RepositorioBaseArray<Pedido>
{
    public PedidoRepositorioArray() : base("pedidos_array.csv") { }

    public override string ToCsv(Pedido p)
    {
        string itens = string.Join("|", p.Itens.Select(i => $"{i.Quantidade}:{i.PrecoTotal}"));
        return $"{p.Numero},{p.DataHoraPedido.ToString("o", CultureInfo.InvariantCulture)},{p.Cliente?.Email},{p.Itens.Sum(i => i.PrecoTotal)},{p.Transportadora?.Nome},{p.Situacao},{itens}";
    }

    public override Pedido FromCsv(string linha)
    {
        var partes = linha.Split(',');
        var pedido = new Pedido
        {
            Numero = int.Parse(partes[0]),
            DataHoraPedido = DateTime.Parse(partes[1], null, DateTimeStyles.RoundtripKind),
            Cliente = new Cliente { Email = partes[2] }, 
            Itens = new List<PedidoItem>()
        };
        
        if (partes.Length > 4 && !string.IsNullOrEmpty(partes[4]))
        {
            pedido.Transportadora = new Transportadora { Nome = partes[4] };
        }
        
        if (partes.Length > 5 && !string.IsNullOrEmpty(partes[5]))
        {
            if (Enum.TryParse<Situacao>(partes[5], out var situacao))
            {
                pedido.Situacao = situacao;
            }
            else
            {
                pedido.Situacao = Situacao.NOVO; 
            }
        }
        else
        {
            pedido.Situacao = Situacao.NOVO;
        }
        
        if (partes.Length > 6)
        {
            var itensStr = partes[6].Split('|');
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