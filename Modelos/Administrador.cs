namespace ProjetoPOO.Modelos;
using ProjetoPOO.Menus;

public class Administrador
{
    public static void Login()
    {
        bool loginSucesso = false;

        while (!loginSucesso)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("----LOGIN ADMINISTRADOR----");
                Console.Write("Digite o usuário: ");
                string usuario = Console.ReadLine();

                Console.Write("Digite a senha: ");
                string senha = Console.ReadLine();
                
                if (usuario == "administrador" && senha == "123456") 
                { 
                    Console.Clear();
                    Console.WriteLine("Você entrou como administrador!");
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    loginSucesso = true;
                    MenuAdministrador.ExibirMenuAdministrador();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Usuário ou senha incorretos! Tente novamente.");
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
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