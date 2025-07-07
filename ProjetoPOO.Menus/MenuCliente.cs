using ConsoleApp1.ProjetoPOO.Controllers;

namespace ConsoleApp1.ProjetoPOO.Menus;

public class MenuCliente
{
    private readonly ClienteController _controller;

    public MenuCliente()
    {
        _controller = new ClienteController();
    }

    public void ExibirMenu()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----CLIENTE----");
            Console.WriteLine("1 - Incluir cliente");
            Console.WriteLine("2 - Alterar cliente");
            Console.WriteLine("3 - Excluir cliente");
            Console.WriteLine("4 - Consultar clientes");
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
                        _controller.IncluirCliente();
                        break;
                    case 2:
                        _controller.AlterarCliente();
                        break;
                    case 3:
                        _controller.ExcluirCliente();
                        break;
                    case 4:
                        _controller.ConsultarClientes();
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