namespace ConsoleApp1.ProjetoPOO.Modelos;

public class Pedido
{
    public int Numero { get; set; }
    public DateTime DataHoraPedido { get; set; }
    public DateTime DataHoraEntrega { get; set; }
    public Situacao Situacao { get; set; }
    public double PrecoFrete { get; set; }
    public List<PedidoItem> Itens { get; set; }

    public Pedido()
    {
    }

    public Pedido(int numero, DateTime dataHoraPedido, DateTime dataHoraEntrega, Situacao situacao, double precoFrete)
    {
        Numero = numero;
        DataHoraPedido = dataHoraPedido;
        DataHoraEntrega = dataHoraEntrega;
        Situacao = situacao;
        PrecoFrete = precoFrete;
    }
}