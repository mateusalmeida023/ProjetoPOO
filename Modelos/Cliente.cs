namespace ProjetoPOO.Modelos;

public class Cliente : Usuario
{
    public Endereco Endereco { get; set; }
    public static Cliente[] cliente = new Cliente[100]; 
    public static int clienteCount = 0;

    public Cliente(string nome, string email, string telefone, int id)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Id = id;
    }
    
     public static void CadastroClientes()
    {
        int opcao = -1;

        while (opcao != 99)
        {
            Console.Clear();
            Console.WriteLine("----CLIENTES----");
            Console.WriteLine("1 - Incluir cliente");
            Console.WriteLine("2 - Alterar cliente");
            Console.WriteLine("3 - Excluir cliente");
            Console.WriteLine("4 - Buscar cliente");
            Console.WriteLine("99 - Voltar");
            Console.Write("Escolha uma opção: ");

            opcao = int.Parse(Console.ReadLine());
            
            if (opcao == 1)
            {
                IncluirCliente();
            }
            else if (opcao == 2)
            {
                AlterarCliente();
            }
            else if (opcao == 3)
            {
                ExcluirCliente();
            }
            else if (opcao == 4)
            {
                BuscarCliente();
            }
            else if (opcao == 99)
                return;
        }
    }

    public static void IncluirCliente()
    {
        Console.Clear();
        Console.WriteLine("----INCLUIR CLIENTE----");
        Console.Write("Nome do Cliente: ");
        string nome = Console.ReadLine();
        Console.Write("Email do Cliente: ");
        string email = Console.ReadLine();
        Console.Write("Telefone do Cliente: ");
        string telefone = Console.ReadLine();
        
        Console.Clear();
        Endereco endereco = Endereco.AdicionarEndereco();
        
        int id = clienteCount + 1;
        Cliente novo = new Cliente(nome, email, telefone, id);
        novo.Endereco = endereco;
        
        cliente[clienteCount++] = novo;
        
        Console.Clear();
        Console.WriteLine("Cliente incluído com sucesso!");
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public static void ExcluirCliente()
    {
        Console.Clear();
        Console.WriteLine("----EXCLUIR CLIENTE----");
        
        if (clienteCount == 0)
        {
            Console.WriteLine("Não há nenhum cliente para remover.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Clientes cadastrados:");
        for (int i = 0; i < clienteCount; i++)
        {
            Console.WriteLine($"{i + 1}. {cliente[i].Nome} - {cliente[i].Email}");
        }

        Console.Write("\nDigite o número do cliente que deseja remover: ");
        if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > clienteCount)
        {
            Console.Clear();
            Console.WriteLine("Opção inválida! Digite um número da lista.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        int index = opcao - 1;
        
        for (int i = index; i < clienteCount - 1; i++)
        {
            cliente[i] = cliente[i + 1];
        }

        clienteCount--; 
        cliente[clienteCount] = null;
        
        Console.Clear();
        Console.WriteLine("Cliente removido com sucesso!");
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public static void BuscarCliente()
    {
        Console.Clear();
        Console.WriteLine("----BUSCAR CLIENTE----");
        Console.Write("Digite o ID do cliente que deseja buscar: ");
        int id = int.Parse(Console.ReadLine());
        Cliente clienteEncontrado = null;

        for (int i = 0; i < clienteCount; i++)
        {
            if (cliente[i].Id == id)
            {
                clienteEncontrado = cliente[i];
            }
        }

        Console.Clear();
        if (clienteEncontrado != null)
        {
            Console.WriteLine(clienteEncontrado);
        }
        else
        {
            Console.WriteLine("Cliente não encontrado!");
        }
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public static void AlterarCliente()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----ALTERAR CLIENTE----");
            
            if (clienteCount == 0)
            {
                Console.WriteLine("Não há nenhum cliente cadastrado para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Clientes cadastrados:");
            for (int i = 0; i < clienteCount; i++)
            {
                Console.WriteLine($"{i + 1}. {cliente[i].Nome} - {cliente[i].Email}");
            }

            Console.Write("\nDigite o número do cliente que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > clienteCount)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Cliente clienteAtual = cliente[index];

            Console.Clear();
            Console.WriteLine($"Alterando dados do cliente: {clienteAtual.Nome}");
            Console.WriteLine("\nDeixe em branco para manter o valor atual");
            
            Console.Write($"Nome do Cliente ({clienteAtual.Nome}): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
            {
                clienteAtual.Nome = nome;
            }

            Console.Write($"Email do Cliente ({clienteAtual.Email}): ");
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!email.Contains("@"))
                {
                    throw new Exception("Email inválido.");
                }
                clienteAtual.Email = email;
            }

            Console.Write($"Telefone do Cliente ({clienteAtual.Telefone}): ");
            string telefone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(telefone))
            {
                if (telefone.Length < 10)
                {
                    throw new Exception("Telefone inválido. Deve conter pelo menos 10 dígitos.");
                }
                clienteAtual.Telefone = telefone;
            }

            Console.Write("\nDeseja alterar o endereço? (S/N): ");
            string alterarEndereco = Console.ReadLine()?.ToUpper();
            
            if (alterarEndereco == "S")
            {
                Console.Clear();
                clienteAtual.Endereco = Endereco.AdicionarEndereco();
            }

            Console.Clear();
            Console.WriteLine("Cliente alterado com sucesso!");
            Console.WriteLine(clienteAtual);
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
        return $"Nome: {Nome}, E-mail: {Email}, Telefone: {Telefone}\n" +
               $"{Endereco}";
    }
}
