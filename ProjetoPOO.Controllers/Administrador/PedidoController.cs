using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO.Controllers.Administrador;

public class PedidoController
{
    private readonly PedidoRepositorio _pedidoRepo;

    public PedidoController()
    {
        _pedidoRepo = new PedidoRepositorio();
    }

    public void ConsultarTodosPedidos()
    {
        Console.Clear();
        Console.WriteLine("----CONSULTAR TODOS OS PEDIDOS----");

        var pedidos = _pedidoRepo.BuscarTodos();
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Não há pedidos cadastrados.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        foreach (var pedido in pedidos)
        {
            Console.WriteLine($"Pedido Nº: {pedido.Numero}");
            Console.WriteLine($"Cliente: {pedido.Cliente?.Email}");
            Console.WriteLine($"Data: {pedido.DataHoraPedido:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Situação: {pedido.Situacao}");
            Console.WriteLine($"Transportadora: {pedido.Transportadora?.Nome ?? "N/A"}");
            Console.WriteLine($"Total: R${pedido.Itens.Sum(i => i.PrecoTotal) + pedido.PrecoFrete:F2}");
            Console.WriteLine(new string('-', 50));
        }

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public void AlterarSituacaoPedido()
    {
        Console.Clear();
        Console.WriteLine("----ALTERAR SITUAÇÃO DO PEDIDO----");

        var pedidos = _pedidoRepo.BuscarTodos();
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Não há pedidos cadastrados.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Pedidos disponíveis:");
        foreach (var pedido in pedidos)
        {
            Console.WriteLine($"Nº {pedido.Numero} - Cliente: {pedido.Cliente?.Email} - Situação: {pedido.Situacao}");
        }

        Console.Write("\nDigite o número do pedido: ");
        if (!int.TryParse(Console.ReadLine(), out int numeroPedido))
        {
            Console.WriteLine("Número inválido!");
            Console.ReadKey();
            return;
        }

        var pedidoSelecionado = pedidos.FirstOrDefault(p => p.Numero == numeroPedido);
        if (pedidoSelecionado == null)
        {
            Console.WriteLine("Pedido não encontrado!");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine($"Pedido Nº {pedidoSelecionado.Numero} - Situação atual: {pedidoSelecionado.Situacao}");
        Console.WriteLine("Situações disponíveis:");
        Console.WriteLine("1 - NOVO");
        Console.WriteLine("2 - TRANSPORTE");
        Console.WriteLine("3 - ENTREGUE");
        Console.WriteLine("4 - CANCELADO");

        Console.Write("Escolha a nova situação (1-4): ");
        if (!int.TryParse(Console.ReadLine(), out int opcaoSituacao) || opcaoSituacao < 1 || opcaoSituacao > 4)
        {
            Console.WriteLine("Opção inválida!");
            Console.ReadKey();
            return;
        }

        Situacao novaSituacao = opcaoSituacao switch
        {
            1 => Situacao.NOVO,
            2 => Situacao.TRANSPORTE,
            3 => Situacao.ENTREGUE,
            4 => Situacao.CANCELADO,
            _ => Situacao.NOVO
        };

        pedidoSelecionado.Situacao = novaSituacao;
        _pedidoRepo.Salvar();

        Console.WriteLine($"Situação do pedido Nº {pedidoSelecionado.Numero} alterada para {novaSituacao}!");
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
} 