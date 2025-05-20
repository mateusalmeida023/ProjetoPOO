using System;
using System.Runtime.CompilerServices;

namespace ConsoleApp1;

public class Produto
{
    public string nome;

    public double valor;

    public int quantidade;

    public void CadastrarProduto()
    {
        Produto produto = new Produto();

        Console.WriteLine("Informe o nome do produto a ser cadastrado:");
        produto.nome = Console.ReadLine();

        Console.WriteLine("Informe o valor do Produto:");
        produto.valor = double.Parse(Console.ReadLine());

        Console.WriteLine("Informe a quantidade de unidades deste prodruto:");
        produto.quantidade = int.Parse(Console.ReadLine());

    }

}
