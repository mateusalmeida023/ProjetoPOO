namespace ProjetoPOO.Menus;
using ProjetoPOO.Modelos;
public class MenuAdministrador
{
    public static void ExibirMenuAdministrador()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("----Menu Administrador----");
                Console.WriteLine("1 - Cadastro de Fornecedores");
                Console.WriteLine("2 - Cadastro de Produtos");
                Console.WriteLine("3 - Cadastro de Transportadora");
                Console.WriteLine("4 - Cadastro de Cliente");
                Console.WriteLine("99 - Sair");
                Console.Write("Selecione uma opção: ");
                
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    throw new Exception("Opção inválida! Digite apenas números.");
                }
                
                switch (opcao)
                {
                    case 1:
                        Fornecedor.CadastroFonecedores();
                        break;
                    case 2:
                        Produto.CadastroProdutos();
                        break;
                    case 3:
                        Transportadora.CadastroTransportadora();
                        break;
                    case 4:
                        Cliente.CadastroClientes();
                        break;
                    case 99:
                        Console.Clear();
                        Console.WriteLine("Você saiu do sistema.");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida! Escolha uma opção do menu.");
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