using ProjetoPOO.Controllers;

namespace ProjetoPOO.Menus;

public class MenuProduto
{
    private readonly ProdutoController _controller;

    public MenuProduto()
    {
        _controller = new ProdutoController();
    }

    public void ExibirMenu()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----PRODUTO----");
            Console.WriteLine("1 - Incluir produto");
            Console.WriteLine("2 - Alterar produto");
            Console.WriteLine("3 - Excluir produto");
            Console.WriteLine("4 - Consultar produtos");
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
                        _controller.IncluirProduto();
                        break;
                    case 2:
                        _controller.AlterarProduto();
                        break;
                    case 3:
                        _controller.ExcluirProduto();
                        break;
                    case 4:
                        _controller.ConsultarProdutos();
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