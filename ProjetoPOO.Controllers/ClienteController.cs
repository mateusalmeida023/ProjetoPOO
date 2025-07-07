using ConsoleApp1.ProjetoPOO.Modelos;

namespace ConsoleApp1.ProjetoPOO.Controllers;

public class ClienteController
{
    private readonly EnderecoController _enderecoController;

    public ClienteController()
    {
        _enderecoController = new EnderecoController();
    }

    public void IncluirCliente()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----INCLUIR CLIENTE----");
            Console.Write("Nome do Cliente: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("O nome do cliente não pode estar vazio.");
            }

            Console.Write("Email do Cliente: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new Exception("Email inválido.");
            }

            Console.Write("Telefone do Cliente: ");
            string telefone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(telefone) || telefone.Length < 10)
            {
                throw new Exception("Telefone inválido. Deve conter pelo menos 10 dígitos.");
            }

            Console.Write("CPF do Cliente: ");
            string cpf = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
            {
                throw new Exception("CPF inválido. Deve conter 11 dígitos.");
            }
            
            Console.Clear();
            Endereco endereco = _enderecoController.CriarEndereco();
            
            var novo = new Cliente
            {
                Nome = nome,
                Email = email,
                Telefone = telefone,
                CPF = cpf,
                Endereco = endereco
            };
            Data.Data.Clientes[Data.Data.ClientesCount++] = novo;

            Console.Clear();
            Console.WriteLine("Cliente incluído com sucesso!");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Telefone: {telefone}");
            Console.WriteLine($"CPF: {cpf}");
            Console.WriteLine($"Endereço: {endereco}");
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

    public void ExcluirCliente()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----EXCLUIR CLIENTE----");

            if (Data.Data.ClientesCount == 0)
            {
                Console.WriteLine("Não há nenhum cliente para remover.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < Data.Data.ClientesCount; i++)
            {
                Console.WriteLine($"{i + 1}.  {Data.Data.Clientes[i].Nome} - CPF: {Data.Data.Clientes[i].CPF}");
            }
            Console.Write("\nDigite o número do cliente que deseja remover: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > Data.Data.ClientesCount)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida! Digite um número da lista.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int index = opcao - 1;
            
            for (int i = index; i < Data.Data.ClientesCount - 1; i++)
            {
                Data.Data.Clientes[i] = Data.Data.Clientes[i + 1];
            }

            Data.Data.ClientesCount--;
            Data.Data.Clientes[Data.Data.ClientesCount] = null;

            Console.Clear();
            Console.WriteLine("Cliente removido com sucesso!");
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

    public void ConsultarClientes()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----CONSULTAR CLIENTES----");

            if (Data.Data.ClientesCount == 0)
            {
                Console.WriteLine("Não há nenhum cliente cadastrado.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < Data.Data.ClientesCount; i++)
            {
                Console.WriteLine($"{i + 1}.  {Data.Data.Clientes[i].Nome} - CPF: {Data.Data.Clientes[i].CPF}");
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

    public void AlterarCliente()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----ALTERAR CLIENTE----");
            
            if (Data.Data.ClientesCount == 0)
            {
                Console.WriteLine("Não há nenhum cliente cadastrado para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Clientes cadastrados:");
            for (int i = 0; i < Data.Data.ClientesCount; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.Data.Clientes[i].Nome} - CPF: {Data.Data.Clientes[i].CPF}");
            }

            Console.Write("\nDigite o número do cliente que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > Data.Data.ClientesCount)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Cliente clienteAtual = Data.Data.Clientes[index];

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

            Console.Write($"CPF do Cliente ({clienteAtual.CPF}): ");
            string cpf = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(cpf))
            {
                if (cpf.Length != 11)
                {
                    throw new Exception("CPF inválido. Deve conter 11 dígitos.");
                }
                clienteAtual.CPF = cpf;
            }

            Console.Write("\nDeseja alterar o endereço? (S/N): ");
            string alterarEndereco = Console.ReadLine()?.ToUpper();
            
            if (alterarEndereco == "S")
            {
                Console.Clear();
                clienteAtual.Endereco = _enderecoController.CriarEndereco();
            }

            Console.Clear();
            Console.WriteLine("Cliente alterado com sucesso!");
            Console.WriteLine($"Nome: {clienteAtual.Nome}");
            Console.WriteLine($"Email: {clienteAtual.Email}");
            Console.WriteLine($"Telefone: {clienteAtual.Telefone}");
            Console.WriteLine($"CPF: {clienteAtual.CPF}");
            Console.WriteLine($"Endereço: {clienteAtual.Endereco}");
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