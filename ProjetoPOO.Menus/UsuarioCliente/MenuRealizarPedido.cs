using ProjetoPOO.Controllers;
using ProjetoPOO.Modelos;

namespace ProjetoPOO.Menus.UsuarioCliente;

public class MenuRealizarPedido
{
    private readonly PedidoController _pedidoController;
    public MenuRealizarPedido()
    {
        _pedidoController = new PedidoController();
    }
    public void Exibir(Cliente cliente)
    {
        _pedidoController.RealizarPedido(cliente);
    }
}