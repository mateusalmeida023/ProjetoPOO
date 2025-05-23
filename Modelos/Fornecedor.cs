namespace ProjetoPOO.Modelos;

public class Fornecedor : Usuario
{
    public Endereco Endereco { get; set; }
    public string Descricao { get; set; }
    public int Id { get; set; }
    public static Fornecedor[] fornecedores = new  Fornecedor[100];
    public static int fornecedoresCount = 0;

    public Fornecedor(string nome, string email, string telefone, string descricao, int id)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Descricao = descricao;
        Id = id;
    }

    public static void CadastroFonecedores()
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

            opcao = int.Parse(Console.ReadLine());
            
            if (opcao == 1)
            {
                IncluirFornecedor();
            }
            else if (opcao == 2)
            {
                AlterarFornecedor();
            }
            else if (opcao == 3)
            {
                ExcluirFornecedor();
            }
            else if (opcao == 4)
            {
                BuscarFornecedor();
            }
            else if (opcao == 99)
                return;
        }
    }

    public static void IncluirFornecedor()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----INCLUIR FORNECEDOR----");
            
            Console.Write("Nome do Fornecedor: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("O nome do fornecedor não pode estar vazio.");
            }

            Console.Write("Email do Fornecedor: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new Exception("Email inválido.");
            }

            Console.Write("Telefone do Fornecedor: ");
            string telefone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(telefone) || telefone.Length < 10)
            {
                throw new Exception("Telefone inválido. Deve conter pelo menos 10 dígitos.");
            }

            Console.Write("Descrição do Fornecedor: ");
            string descricao = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new Exception("A descrição não pode estar vazia.");
            }
            
            Console.Clear();
            Endereco endereco = Endereco.AdicionarEndereco();
            
            int id = fornecedoresCount + 1;
            Fornecedor novo = new Fornecedor(nome, email, telefone, descricao, id);
            novo.Endereco = endereco;
            
            fornecedores[fornecedoresCount++] = novo;
            
            Console.Clear();
            Console.WriteLine("Fornecedor incluído com sucesso!");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Telefone: {telefone}");
            Console.WriteLine($"Descrição: {descricao}");
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

    public static void ExcluirFornecedor()
    {
        Console.Clear();
        Console.WriteLine("----EXCLUIR FORNECEDOR----");
        
        if (fornecedoresCount == 0)
        {
            Console.WriteLine("Não há nenhum fornecedor para remover.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Fornecedores cadastrados:");
        for (int i = 0; i < fornecedoresCount; i++)
        {
            Console.WriteLine($"{i + 1}. {fornecedores[i].Nome} - {fornecedores[i].Email}");
        }

        Console.Write("\nDigite o número do fornecedor que deseja remover: ");
        if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > fornecedoresCount)
        {
            Console.Clear();
            Console.WriteLine("Opção inválida! Digite um número da lista.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        int index = opcao - 1;
        
        for (int i = index; i < fornecedoresCount - 1; i++)
        {
            fornecedores[i] = fornecedores[i + 1];
        }

        fornecedoresCount--; 
        fornecedores[fornecedoresCount] = null;
        
        Console.Clear();
        Console.WriteLine("Fornecedor removido com sucesso!");
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public static void BuscarFornecedor()
    {
        Console.Clear();
        Console.WriteLine("----BUSCAR FORNECEDOR----");
        Console.Write("Digite o ID do fornecedor que deseja buscar: ");
        int id = int.Parse(Console.ReadLine());
        Fornecedor fornecedorEncontrado = null;

        for (int i = 0; i < fornecedoresCount; i++)
        {
            if (fornecedores[i].Id == id)
            {
                fornecedorEncontrado = fornecedores[i];
            }
        }

        Console.Clear();
        if (fornecedorEncontrado != null)
        {
            Console.WriteLine(fornecedorEncontrado);
        }
        else
        {
            Console.WriteLine("Fornecedor não encontrado!");
        }
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public static void AlterarFornecedor()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----ALTERAR FORNECEDOR----");
            
            if (fornecedoresCount == 0)
            {
                Console.WriteLine("Não há nenhum fornecedor cadastrado para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Fornecedores cadastrados:");
            for (int i = 0; i < fornecedoresCount; i++)
            {
                Console.WriteLine($"{i + 1}. {fornecedores[i].Nome} - {fornecedores[i].Email}");
            }

            Console.Write("\nDigite o número do fornecedor que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > fornecedoresCount)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Fornecedor fornecedor = fornecedores[index];

            Console.Clear();
            Console.WriteLine($"Alterando dados do fornecedor: {fornecedor.Nome}");
            Console.WriteLine("\nDeixe em branco para manter o valor atual");
            
            Console.Write($"Nome do Fornecedor ({fornecedor.Nome}): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
            {
                fornecedor.Nome = nome;
            }

            Console.Write($"Email do Fornecedor ({fornecedor.Email}): ");
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!email.Contains("@"))
                {
                    throw new Exception("Email inválido.");
                }
                fornecedor.Email = email;
            }

            Console.Write($"Telefone do Fornecedor ({fornecedor.Telefone}): ");
            string telefone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(telefone))
            {
                if (telefone.Length < 10)
                {
                    throw new Exception("Telefone inválido. Deve conter pelo menos 10 dígitos.");
                }
                fornecedor.Telefone = telefone;
            }

            Console.Write($"Descrição do Fornecedor ({fornecedor.Descricao}): ");
            string descricao = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(descricao))
            {
                fornecedor.Descricao = descricao;
            }

            Console.Write("\nDeseja alterar o endereço? (S/N): ");
            string alterarEndereco = Console.ReadLine()?.ToUpper();
            
            if (alterarEndereco == "S")
            {
                Console.Clear();
                fornecedor.Endereco = Endereco.AdicionarEndereco();
            }

            Console.Clear();
            Console.WriteLine("Fornecedor alterado com sucesso!");
            Console.WriteLine($"Nome: {fornecedor.Nome}");
            Console.WriteLine($"Email: {fornecedor.Email}");
            Console.WriteLine($"Telefone: {fornecedor.Telefone}");
            Console.WriteLine($"Descrição: {fornecedor.Descricao}");
            Console.WriteLine($"Endereço: {fornecedor.Endereco}");
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

    public override string ToString()
    {
        return $"Nome: {Nome}, E-mail: {Email}, Telefone: {Telefone}, Descrição: {Descricao}\n" +
               $"{Endereco}";
    }
}
