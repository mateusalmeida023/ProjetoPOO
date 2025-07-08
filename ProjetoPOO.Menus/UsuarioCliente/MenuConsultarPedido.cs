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
            Console.WriteLine("3 - Consultar todos pedidos");
            Console.WriteLine("99 - Voltar");
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
            else if (opcao == "3")
            {
                ConsultarTodosPedidos(cliente);
            }
            else if (opcao == "99")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    private void ConsultarPorNumero(Cliente cliente)
    {
        Console.Clear();
        var pedidos = _pedidoController.ConsultarPorCliente(cliente);
        if (pedidos == null || pedidos.Count == 0)
        {
            Console.WriteLine("Você não possui nenhum pedido.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        
        Console.WriteLine("Seus pedidos:");
        foreach (var p in pedidos)
        {
            Console.WriteLine($"- Pedido Nº {p.Numero} - {p.DataHoraPedido:dd/MM/yyyy HH:mm}");
        }
        Console.WriteLine();
        
        Console.Write("Digite o número do pedido: ");
        if (!int.TryParse(Console.ReadLine(), out int numero))
        {
            Console.WriteLine("Número inválido!");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        var pedido = _pedidoController.ConsultarPorNumero(cliente, numero);
        if (pedido == null)
        {
            Console.WriteLine("Pedido não encontrado!");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        Console.Clear();
        Console.WriteLine(_pedidoController.GerarDetalhesPedido(pedido));
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }

    private void ConsultarPorData(Cliente cliente)
    {
        Console.Clear();
        Console.Write("Data inicial (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataIni))
        {
            Console.WriteLine("Data inválida!");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        Console.Write("Data final (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataFim))
        {
            Console.WriteLine("Data inválida!");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        var pedidos = _pedidoController.ConsultarPorData(cliente, dataIni, dataFim);
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido encontrado no período.");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        foreach (var pedido in pedidos)
        {
            Console.Clear();
            Console.WriteLine(_pedidoController.GerarDetalhesPedido(pedido));
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void ConsultarTodosPedidos(Cliente cliente)
    {
        Console.Clear();
        var pedidos = _pedidoController.ConsultarPorCliente(cliente);
        if (pedidos == null || pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido encontrado.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        
        Console.WriteLine($"----TODOS OS PEDIDOS DE {cliente.Nome.ToUpper()}----");
        Console.WriteLine($"Total de pedidos: {pedidos.Count}");
        Console.WriteLine();
        
        foreach (var pedido in pedidos)
        {
            Console.WriteLine(_pedidoController.GerarDetalhesPedido(pedido));
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();
        }
        
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }
}