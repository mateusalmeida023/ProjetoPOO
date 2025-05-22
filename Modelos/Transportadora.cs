namespace ProjetoPOO.Modelos;

public class Transportadora
{
    public string Nome { get; set; }
    public double PrecoPorKm { get; set; }

    public static Transportadora[] transportadora = new Transportadora[100];
    public static int transportadoraCount = 0;

    public Transportadora(string nome, double preco)
    {
        Nome = nome;
        PrecoPorKm = preco;
    }
    public static void CadastroTransportadora()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----TRANSPORTADORA----");
            Console.WriteLine("1 - Incluir transportadora");
            Console.WriteLine("2 - Alterar transportadora");
            Console.WriteLine("3 - Excluir transportadora");
            Console.WriteLine("4 - Consultar transportadoras");
            Console.WriteLine("99 - Voltar");
            Console.Write("Escolha uma opção: ");

            try
            {
                opcao = int.Parse(Console.ReadLine());
                
                if (opcao == 1)
                {
                    IncluirTransportadora();
                }
                else if (opcao == 2)
                {
                    AlterarTransportadora();
                }
                else if (opcao == 3)
                {
                    ExcluirTransportadora();
                }
                else if (opcao == 4)
                { 
                    ConsultarTransportadoras();
                }
                else if (opcao == 99)
                {
                    return;
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida! Digite apenas números.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    public static void IncluirTransportadora()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----INCLUIR TRANSPORTADORA----");
            Console.Write("Nome da Transportadora: ");
            string nome = Console.ReadLine();
            Console.Write("Preço por KM cobrado: R$");
            double preco = double.Parse(Console.ReadLine());
            
            Transportadora nova = new Transportadora(nome, preco);
            
            transportadora[transportadoraCount++] = nova;

            Console.WriteLine();
            Console.WriteLine("Transportadora incluida com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public static void ExcluirTransportadora()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----EXCLUIR TRANSPORTADORA----");

            if (transportadoraCount == 0)
            {
                Console.WriteLine("Não há nenhuma transportadora para remover.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < transportadoraCount; i++)
            {
                Console.WriteLine($"{i + 1}.  {transportadora[i].Nome}");
            }
            Console.Write("\nDigite o número do produto que deseja remover: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > transportadoraCount)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida! Digite um número da lista.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int index = opcao - 1;
            
            for (int i = index; i < transportadoraCount - 1; i++)
            {
                transportadora[i] = transportadora[i + 1];
            }

            transportadoraCount--;
            transportadora[transportadoraCount] = null;

            Console.Clear();
            Console.WriteLine("Transportadora removido com sucesso!");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public static void ConsultarTransportadoras()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----CONSULTAR TRANSPORTADORAS----");

            if (transportadoraCount == 0)
            {
                Console.WriteLine("Não há nenhuma transportadora cadastrada.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < transportadoraCount; i++)
            {
                Console.WriteLine($"{i + 1}.  {transportadora[i].Nome}");
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public static void AlterarTransportadora()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----ALTERAR TRANSPORTADORA----");
            
            if (transportadoraCount == 0)
            {
                Console.WriteLine("Não há nenhuma transportadora cadastrada para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Transportadoras cadastradas:");
            for (int i = 0; i < transportadoraCount; i++)
            {
                Console.WriteLine($"{i + 1}. {transportadora[i].Nome} - R${transportadora[i].PrecoPorKm:F2} por Km");
            }

            Console.Write("\nDigite o número da transportadora que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > transportadoraCount)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Transportadora transportadoraAtual = transportadora[index];

            Console.Clear();
            Console.WriteLine($"Alterando dados da transportadora: {transportadoraAtual.Nome}");
            Console.WriteLine("\nDeixe em branco para manter o valor atual");
            
            Console.Write($"Nome da Transportadora ({transportadoraAtual.Nome}): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
            {
                transportadoraAtual.Nome = nome;
            }

            Console.Write($"Preço por KM (R${transportadoraAtual.PrecoPorKm:F2}): R$");
            string precoStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(precoStr))
            {
                if (!double.TryParse(precoStr, out double preco) || preco <= 0)
                {
                    throw new Exception("Preço inválido. Digite um número maior que zero.");
                }
                transportadoraAtual.PrecoPorKm = preco;
            }

            Console.Clear();
            Console.WriteLine("Transportadora alterada com sucesso!");
            Console.WriteLine($"Nome: {transportadoraAtual.Nome}");
            Console.WriteLine($"Preço por KM: R${transportadoraAtual.PrecoPorKm:F2}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
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