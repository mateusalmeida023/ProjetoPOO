using ProjetoPOO.Controllers;
using ProjetoPOO.Repository.Lists;
using ProjetoPOO.Menus;
using ProjetoPOO.Menus.UsuarioCliente;

namespace ProjetoPOO.Menus;

public class MenuPrincipal
{
    
    public readonly ClienteController _clienteController;

    public MenuPrincipal()
    {
        _clienteController = new ClienteController();
    }
    public void ExibirMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Bem-vindo ao sistema!");
            Console.WriteLine("1 - Registrar");
            Console.WriteLine("2 - Fazer login");
            Console.WriteLine("99 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();
            if (opcao == "1")
            {
                _clienteController.IncluirCliente();
            }
            else if (opcao == "2")
            {
                FazerLogin();
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

    private void FazerLogin()
    {
        Console.Clear();
        Console.WriteLine("----LOGIN----");
        Console.Write("Usuário (email ou 'administrador'): ");
        string usuario = Console.ReadLine();
        Console.Write("Senha: ");
        string senha = Console.ReadLine();
        if (usuario == "administrador" && senha == "1234")
        {
            var menuAdmin = new MenuAdministrador();
            menuAdmin.ExibirMenu();
            return;
        }
        var clienteRepo = new ClienteRepositorio();
        var clientes = clienteRepo.BuscarTodos();
        var cliente = clientes.FirstOrDefault(c => c.Email == usuario && c.Senha == senha);
        if (cliente != null)
        {
            Console.Clear();
            Console.WriteLine($"Bem-vindo, {cliente.Nome}!");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            var menuCliente = new MenuUsuarioCliente(cliente);
            menuCliente.ExibirMenu();
            return;
        }
        Console.Clear();
        Console.WriteLine("Login ou senha inválidos! Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}