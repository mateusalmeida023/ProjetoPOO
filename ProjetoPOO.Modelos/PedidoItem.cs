namespace ProjetoPOO.Modelos;

public class PedidoItem
{
    public int Quantidade { get; set; }
    public double PrecoTotal { get; set; }

    public PedidoItem()
    {
    }

    public PedidoItem(int quantidade, double precoTotal)
    {
        Quantidade = quantidade;
        PrecoTotal = precoTotal;
    }
}