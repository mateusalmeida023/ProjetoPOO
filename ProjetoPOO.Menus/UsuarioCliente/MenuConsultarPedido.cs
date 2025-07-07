using ProjetoPOO.Modelos;
using ProjetoPOO.Controllers;
using System.Globalization;

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
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();
            if (opcao == "1")
            {
                ConsultarPorNumero(cliente);
            }
            else if (opcao == "2")
            {
                ConsultarPorData(cliente);
            }
            else if (opcao == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
                Console.ReadKey();
            }
        }
    }

    private void ConsultarPorNumero(Cliente cliente)
    {
        Console.Write("Digite o número do pedido: ");
        if (!int.TryParse(Console.ReadLine(), out int numero))
        {
            Console.WriteLine("Número inválido!");
            Console.ReadKey();
            return;
        }
        var pedido = _pedidoController.ConsultarPorNumero(cliente, numero);
        if (pedido == null)
        {
            Console.WriteLine("Pedido não encontrado!");
            Console.ReadKey();
            return;
        }
        Console.Clear();
        Console.WriteLine(_pedidoController.GerarDetalhesPedido(pedido));
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    private void ConsultarPorData(Cliente cliente)
    {
        Console.Write("Data inicial (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataIni))
        {
            Console.WriteLine("Data inválida!");
            Console.ReadKey();
            return;
        }
        Console.Write("Data final (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataFim))
        {
            Console.WriteLine("Data inválida!");
            Console.ReadKey();
            return;
        }
        var pedidos = _pedidoController.ConsultarPorData(cliente, dataIni, dataFim);
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido encontrado no período.");
            Console.ReadKey();
            return;
        }
        foreach (var pedido in pedidos)
        {
            Console.Clear();
            Console.WriteLine(_pedidoController.GerarDetalhesPedido(pedido));
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}