using ProjetoPOO.Controllers;

namespace ProjetoPOO.Menus;

public class MenuAdministrador
{
    private readonly MenuCliente _menuCliente;
    private readonly MenuFornecedor _menuFornecedor;
    private readonly MenuProduto _menuProduto;
    private readonly MenuTransportadora _menuTransportadora;
    private readonly MenuPedido _menuPedido;

    public MenuAdministrador()
    {
        _menuCliente = new MenuCliente();
        _menuFornecedor = new MenuFornecedor();
        _menuProduto = new MenuProduto();
        _menuTransportadora = new MenuTransportadora();
        _menuPedido = new MenuPedido();
    }
    
    public void ExibirMenu()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----MENU ADMINISTRADOR----");
            Console.WriteLine("1 - Gerenciar Clientes");
            Console.WriteLine("2 - Gerenciar Fornecedores");
            Console.WriteLine("3 - Gerenciar Produtos");
            Console.WriteLine("4 - Gerenciar Transportadoras");
            Console.WriteLine("5 - Gerenciar Pedidos");
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
                        _menuCliente.ExibirMenu();
                        break;
                    case 2:
                        _menuFornecedor.ExibirMenu();
                        break;
                    case 3:
                        _menuProduto.ExibirMenu();
                        break;
                    case 4:
                        _menuTransportadora.ExibirMenu();
                        break;
                    case 5:
                        _menuPedido.ExibirMenu();
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