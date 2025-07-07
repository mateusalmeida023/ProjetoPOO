using ProjetoPOO.Controllers.Exceptions;
using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO.Controllers;

public class FornecedorController
{
    private EnderecoController _enderecoController;
    private FornecedorRepositorio _repo;

    public FornecedorController()
    {
        _enderecoController = new EnderecoController();
        _repo = new FornecedorRepositorio();
    }

    public void IncluirFornecedor()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----INCLUIR FORNECEDOR----");
            
            Console.Write("Nome do Fornecedor: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new NullException("O nome do fornecedor não pode estar vazio.");
            }

            Console.Write("Email do Fornecedor: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new EmailInvalidoException();
            }

            Console.Write("Telefone do Fornecedor: ");
            string telefone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(telefone) || telefone.Length < 10)
            {
                throw new TelefoneInvalidoException();
            }

            Console.Write("Descrição do Fornecedor: ");
            string descricao = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new NullException("A descrição não pode estar vazia.");
            }
            
            Console.Clear();
            Endereco endereco = _enderecoController.CriarEndereco();
            
            int id = _repo.BuscarTodos().Count + 1;
            var novo = new Fornecedor
            {
                Nome = nome,
                Email = email,
                Telefone = telefone,
                Descricao = descricao,
                Id = id,
                Endereco = endereco
            };
            _repo.Incluir(novo);
            
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

    public void ExcluirFornecedor()
    {
        Console.Clear();
        Console.WriteLine("----EXCLUIR FORNECEDOR----");
        
        var fornecedores = _repo.BuscarTodos();
        if (fornecedores.Count == 0)
        {
            Console.WriteLine("Não há nenhum fornecedor para remover.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Fornecedores cadastrados:");
        for (int i = 0; i < fornecedores.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {fornecedores[i].Nome} - {fornecedores[i].Email}");
        }

        Console.Write("\nDigite o número do fornecedor que deseja remover: ");
        if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > fornecedores.Count)
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
        Console.WriteLine("Fornecedor removido com sucesso!");
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public void BuscarFornecedor()
    {
        Console.Clear();
        Console.WriteLine("----BUSCAR FORNECEDOR----");
        Console.Write("Digite o ID do fornecedor que deseja buscar: ");
        int id = int.Parse(Console.ReadLine());
        Fornecedor fornecedorEncontrado = null;

        var fornecedores = _repo.BuscarTodos();
        for (int i = 0; i < fornecedores.Count; i++)
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

    public void AlterarFornecedor()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----ALTERAR FORNECEDOR----");
            
            var fornecedores = _repo.BuscarTodos();
            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Não há nenhum fornecedor cadastrado para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Fornecedores cadastrados:");
            for (int i = 0; i < fornecedores.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {fornecedores[i].Nome} - {fornecedores[i].Email}");
            }

            Console.Write("\nDigite o número do fornecedor que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > fornecedores.Count)
            {
                throw new NullException("Opção inválida! Digite um número da lista.");
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
                    throw new EmailInvalidoException();
                }
                fornecedor.Email = email;
            }

            Console.Write($"Telefone do Fornecedor ({fornecedor.Telefone}): ");
            string telefone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(telefone))
            {
                if (telefone.Length < 10)
                {
                    throw new TelefoneInvalidoException();
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
                fornecedor.Endereco = _enderecoController.CriarEndereco();
            }

            _repo.Alterar(index, fornecedor);

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
} 