using ProjetoPOO.Modelos;
using ProjetoPOO.Controllers;

namespace ProjetoPOO.Menus.UsuarioCliente;

public class MenuConsultarPedido
{
    private readonly PedidoController _pedidoController;
    public MenuConsultarPedido()
    {
        _pedidoController = new PedidoController();
    }
    public void Exibir(Cliente cliente)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----CONSULTA DE PEDIDOS----");
            Console.WriteLine("1 - Consultar por número do pedido");
            Console.WriteLine("2 - Consultar por intervalo de datas");
            Console.WriteLine("3 - Consultar todos pedidos");
            Console.WriteLine("99 - Voltar");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    _pedidoController.MenuConsultaPorNumero(cliente);
                    break;
                case "2":
                    _pedidoController.MenuConsultaPorData(cliente);
                    break;
                case "3":
                    _pedidoController.MenuConsultaTodosPedidos(cliente);
                    break;
                case "99":
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }
}