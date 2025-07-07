namespace ConsoleApp1.ProjetoPOO.Controllers;

public class AdministradorController
{
    private const string USUARIO_PADRAO = "administrador";
    private const string SENHA_PADRAO = "1234";

    public bool ValidarLogin(string usuario, string senha)
    {
        return usuario == USUARIO_PADRAO && senha == SENHA_PADRAO;
    }

    public string ObterUsuario()
    {
        Console.Clear();
        Console.WriteLine("----LOGIN ADMINISTRADOR----");
        Console.Write("Digite o usuário: ");
        return Console.ReadLine() ?? string.Empty;
    }

    public string ObterSenha()
    {
        Console.Write("Digite a senha: ");
        return Console.ReadLine() ?? string.Empty;
    }

    public void ExibirMensagemSucesso()
    {
        Console.Clear();
        Console.WriteLine("Você entrou como administrador!");
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public void ExibirMensagemErro()
    {
        Console.Clear();
        Console.WriteLine("Usuário ou senha incorretos! Tente novamente.");
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
} 