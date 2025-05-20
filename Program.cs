using System.Security.Cryptography.X509Certificates;
using ConsoleApp1;

internal class Program
{
    public static void Main(string[] args)
    {


        Console.WriteLine("1 - Cadastro");
        Console.WriteLine("2 - Login");
        int opcao = int.Parse(Console.ReadLine());

        if (opcao == 1)
        {

            Console.WriteLine("Insira seu Id de Cadastro:");
            int validaId = int.Parse(Console.ReadLine());
            Fornecedor vetIdFornecedor[] = new validaId;


            Console.WriteLine("Insira sua senha de Cadastro:");
            string validaSenha = Console.ReadLine();

        }

    }
}