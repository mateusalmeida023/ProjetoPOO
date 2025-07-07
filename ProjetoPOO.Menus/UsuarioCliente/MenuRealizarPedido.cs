using ProjetoPOO.Controllers;
using ProjetoPOO.Modelos;

namespace ProjetoPOO.Menus.UsuarioCliente;

public class MenuRealizarPedido
{
    public void Exibir(Cliente cliente)
    {
        Console.WriteLine("Por favor, utilize o menu principal do cliente para realizar pedidos.");
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}