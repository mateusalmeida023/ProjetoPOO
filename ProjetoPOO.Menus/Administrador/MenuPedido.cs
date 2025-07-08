using ProjetoPOO.Controllers.Administrador;

namespace ProjetoPOO.Menus;

public class MenuPedido
{
    private readonly PedidoController _controller;

    public MenuPedido()
    {
        _controller = new PedidoController();
    }

    public void ExibirMenu()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----GERENCIAR PEDIDOS----");
            Console.WriteLine("1 - Consultar todos os pedidos");
            Console.WriteLine("2 - Alterar situação do pedido");
            Console.WriteLine("99 - Voltar");
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
                        _controller.ConsultarTodosPedidos();
                        break;
                    case 2:
                        _controller.AlterarSituacaoPedido();
                        break;
                    case 99:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida!");
                        Console.WriteLine("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
} 