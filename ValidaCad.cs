using System;
using System.Collections;

namespace ConsoleApp1;

public class ValidaCad : Usuario
{
    public String NovoNome;
    public String NovaSenha;

    public void Cadastro()
    {

        Console.WriteLine("1 - Cadastrar");
        Console.WriteLine("2 - Login");

        Console.WriteLine("Insira a opção desejada:");
        var opcao = int.Parse(Console.ReadLine());

        if (opcao == 1)
        {

            Console.WriteLine("Insira o nome a ser cadastrado:");
            NovoNome = Console.ReadLine();

            Console.WriteLine("Insira a senha a ser cadastrada:");
            NovaSenha = Console.ReadLine();

        }
        else
        {
            Console.WriteLine("Insira o nome:");
            NovoNome = Console.ReadLine();

            Console.WriteLine("Insira a senha a ser cadastrada:");
            NovaSenha = Console.ReadLine(); 
        }
    }
}
