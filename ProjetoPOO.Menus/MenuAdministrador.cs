using ConsoleApp1.ProjetoPOO.Controllers;

namespace ConsoleApp1.ProjetoPOO.Menus;

public class MenuAdministrador
{
    private readonly AdministradorController _controller;
    private readonly MenuCliente _menuCliente;
    private readonly MenuFornecedor _menuFornecedor;
    private readonly MenuProduto _menuProduto;
    private readonly MenuTransportadora _menuTransportadora;

    public MenuAdministrador()
    {
        _controller = new AdministradorController();
        _menuCliente = new MenuCliente();
        _menuFornecedor = new MenuFornecedor();
        _menuProduto = new MenuProduto();
        _menuTransportadora = new MenuTransportadora();
    }

    public void RealizarLogin()
    {
        bool loginSucesso = false;

        while (!loginSucesso)
        {
            try
            {
                string usuario = _controller.ObterUsuario();
                string senha = _controller.ObterSenha();

                if (_controller.ValidarLogin(usuario, senha))
                {
                    _controller.ExibirMensagemSucesso();
                    loginSucesso = true;
                    ExibirMenu();
                }
                else
                {
                    _controller.ExibirMensagemErro();
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

    private void ExibirMenu()
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