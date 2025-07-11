using System;
using ProjetoPOO.Menus;
using ProjetoPOO.Repository.Arrays;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Projeto POO - Sistema de Repositórios ===\n");
        
        Console.WriteLine("Escolha o tipo de repositório:");
        Console.WriteLine("1 - Usar Arrays");
        Console.WriteLine("2 - Usar Listas");
        Console.Write("Digite sua escolha (1 ou 2): ");
        
        string escolha = Console.ReadLine();
        
        if (escolha == "1")
        {
            Console.WriteLine("\nInicializando repositórios com Arrays...");
            var clienteRepo = new ClienteRepositorioArray();
            var produtoRepo = new ProdutoRepositorioArray();
            var fornecedorRepo = new FornecedorRepositorioArray();
            var pedidoRepo = new PedidoRepositorioArray();
            var transportadoraRepo = new TransportadoraRepositorioArray();
        }
        else if (escolha == "2")
        {
            Console.WriteLine("\nInicializando repositórios com Listas...");
            var clienteRepo = new ClienteRepositorio();
            var produtoRepo = new ProdutoRepositorio();
            var fornecedorRepo = new FornecedorRepositorio();
            var pedidoRepo = new PedidoRepositorio();
            var transportadoraRepo = new TransportadoraRepositorio();
        }
        else
        {
            Console.WriteLine("Opção inválida! Usando listas por padrão.");
            var clienteRepo = new ClienteRepositorio();
            var produtoRepo = new ProdutoRepositorio();
            var fornecedorRepo = new FornecedorRepositorio();
            var pedidoRepo = new PedidoRepositorio();
            var transportadoraRepo = new TransportadoraRepositorio();
        }
        
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
        
        var menuPrincipal = new MenuPrincipal();
        menuPrincipal.ExibirMenu();
    }
}