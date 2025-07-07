using ConsoleApp1.ProjetoPOO.Controllers;

namespace ConsoleApp1.ProjetoPOO.Menus;

public class MenuTransportadora
{
    private readonly TransportadoraController _controller;

    public MenuTransportadora()
    {
        _controller = new TransportadoraController();
    }

    public void ExibirMenu()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----TRANSPORTADORA----");
            Console.WriteLine("1 - Incluir transportadora");
            Console.WriteLine("2 - Alterar transportadora");
            Console.WriteLine("3 - Excluir transportadora");
            Console.WriteLine("4 - Consultar transportadoras");
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
                        _controller.IncluirTransportadora();
                        break;
                    case 2:
                        _controller.AlterarTransportadora();
                        break;
                    case 3:
                        _controller.ExcluirTransportadora();
                        break;
                    case 4:
                        _controller.ConsultarTransportadoras();
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