using System;

namespace ProjetoPOO.Menus;

using ProjetoPOO.Controllers;

public class MenuFornecedor
{
    private readonly FornecedorController _controller;

    public MenuFornecedor()
    {
        _controller = new FornecedorController();
    }

    public void ExibirMenu()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----FORNECEDOR----");
            Console.WriteLine("1 - Incluir fornecedor");
            Console.WriteLine("2 - Alterar fornecedor");
            Console.WriteLine("3 - Excluir fornecedor");
            Console.WriteLine("4 - Buscar fornecedor");
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
                        _controller.IncluirFornecedor();
                        break;
                    case 2:
                        _controller.AlterarFornecedor();
                        break;
                    case 3:
                        _controller.ExcluirFornecedor();
                        break;
                    case 4:
                        _controller.BuscarFornecedor();
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