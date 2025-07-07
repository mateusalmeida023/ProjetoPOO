using ProjetoPOO.Controllers.Exceptions;
using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO.Controllers;

public class ClienteController
{
    private EnderecoController _enderecoController;
    private ClienteRepositorio _repo;

    public ClienteController()
    {
        _enderecoController = new EnderecoController();
        _repo = new ClienteRepositorio();
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
                throw new NullException("O nome do cliente não pode estar vazio.");
            }

            Console.Write("Email do Cliente: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new EmailInvalidoException();
            }

            Console.Write("Telefone do Cliente: ");
            string telefone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(telefone) || telefone.Length < 10)
            {
                throw new TelefoneInvalidoException();
            }

            Console.Write("CPF do Cliente: ");
            string cpf = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
            {
                throw new CpfInvalidoException();
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
            _repo.Incluir(novo);

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

            var clientes = _repo.BuscarTodos();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Não há nenhum cliente para remover.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < clientes.Count; i++)
            {
                Console.WriteLine($"{i + 1}.  {clientes[i].Nome} - CPF: {clientes[i].CPF}");
            }
            Console.Write("\nDigite o número do cliente que deseja remover: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > clientes.Count)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida! Digite um número da lista.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int index = opcao - 1;
            _repo.Excluir(index);

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

            var clientes = _repo.BuscarTodos();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Não há nenhum cliente cadastrado.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < clientes.Count; i++)
            {
                Console.WriteLine($"{i + 1}.  {clientes[i].Nome} - CPF: {clientes[i].CPF}");
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
            
            var clientes = _repo.BuscarTodos();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Não há nenhum cliente cadastrado para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Clientes cadastrados:");
            for (int i = 0; i < clientes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {clientes[i].Nome} - CPF: {clientes[i].CPF}");
            }

            Console.Write("\nDigite o número do cliente que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > clientes.Count)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Cliente clienteAtual = clientes[index];

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
                    throw new EmailInvalidoException();
                }
                clienteAtual.Email = email;
            }

            Console.Write($"Telefone do Cliente ({clienteAtual.Telefone}): ");
            string telefone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(telefone))
            {
                if (telefone.Length < 10)
                {
                    throw new TelefoneInvalidoException();
                }
                clienteAtual.Telefone = telefone;
            }

            Console.Write($"CPF do Cliente ({clienteAtual.CPF}): ");
            string cpf = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(cpf))
            {
                if (cpf.Length != 11)
                {
                    throw new CpfInvalidoException();
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

            _repo.Alterar(index, clienteAtual);

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