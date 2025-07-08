using ProjetoPOO.Modelos;
using ProjetoPOO.Controllers;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO.Menus.UsuarioCliente;

public class MenuUsuarioCliente
{
    private readonly PedidoController _pedidoController;
    private readonly Cliente _clienteLogado;

    public MenuUsuarioCliente(Cliente cliente)
    {
        _clienteLogado = cliente;
        _pedidoController = new PedidoController();
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
                        _pedidoController.MenuFazerPedido(_clienteLogado);
                        break;
                    case 2:
                        var menuConsulta = new MenuConsultarPedido();
                        menuConsulta.Exibir(_clienteLogado);
                        break;
                    case 3:
                        _pedidoController.MenuCancelarPedidos(_clienteLogado);
                        break;
                    case 99:
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}
