using ProjetoPOO.Modelos;

namespace ProjetoPOO.Menus.UsuarioCliente;

public class MenuUsuarioCliente
{
    private MenuRealizarPedido _menuRealizarPedido;
    private Cliente _clienteLogado;

    public MenuUsuarioCliente(Cliente cliente)
    {
        _clienteLogado = cliente;
        _menuRealizarPedido = new MenuRealizarPedido();
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
                {
                    throw new Exception("Opção inválida! Digite apenas números.");
                }

                switch (opcao)
                {
                    case 1:
                        _menuRealizarPedido.Exibir(_clienteLogado);
                        break;
                    case 2:
                        //_menuFornecedor.ExibirMenu();
                        break;
                    case 3:
                       // _menuProduto.ExibirMenu();
                        break;
                    case 99:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida!");
                        Console.WriteLine("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
