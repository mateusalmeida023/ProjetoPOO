using ProjetoPOO.Modelos;
using ProjetoPOO.Repository.Lists;

namespace ProjetoPOO.Controllers;

public class TransportadoraController
{
    private readonly EnderecoController _enderecoController;
    private readonly TransportadoraRepositorio _repo;

    public TransportadoraController()
    {
        _enderecoController = new EnderecoController();
        _repo = new TransportadoraRepositorio();
    }

    public void IncluirTransportadora()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----INCLUIR TRANSPORTADORA----");
            Console.Write("Nome da Transportadora: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("O nome da transportadora não pode estar vazio.");
            }

            Console.Write("Preço por KM cobrado: R$");
            if (!double.TryParse(Console.ReadLine(), out double preco) || preco <= 0)
            {
                throw new Exception("Preço inválido. Digite um número maior que zero.");
            }
            
            Console.Clear();
            Endereco endereco = _enderecoController.CriarEndereco();
            
            var nova = new Transportadora
            {
                Nome = nome,
                PrecoPorKm = preco,
                Endereco = endereco
            };
            _repo.Incluir(nova);

            Console.Clear();
            Console.WriteLine("Transportadora incluída com sucesso!");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Preço por KM: R${preco:F2}");
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

    public void ExcluirTransportadora()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----EXCLUIR TRANSPORTADORA----");

            var transportadoras = _repo.BuscarTodos();
            if (transportadoras.Count == 0)
            {
                Console.WriteLine("Não há nenhuma transportadora para remover.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < transportadoras.Count; i++)
            {
                Console.WriteLine($"{i + 1}.  {transportadoras[i].Nome}");
            }
            Console.Write("\nDigite o número da transportadora que deseja remover: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > transportadoras.Count)
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
            Console.WriteLine("Transportadora removida com sucesso!");
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

    public void ConsultarTransportadoras()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----CONSULTAR TRANSPORTADORAS----");

            var transportadoras = _repo.BuscarTodos();
            if (transportadoras.Count == 0)
            {
                Console.WriteLine("Não há nenhuma transportadora cadastrada.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < transportadoras.Count; i++)
            {
                Console.WriteLine($"{i + 1}.  {transportadoras[i].Nome}");
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

    public void AlterarTransportadora()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("----ALTERAR TRANSPORTADORA----");
            
            var transportadoras = _repo.BuscarTodos();
            if (transportadoras.Count == 0)
            {
                Console.WriteLine("Não há nenhuma transportadora cadastrada para alterar.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Transportadoras cadastradas:");
            for (int i = 0; i < transportadoras.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {transportadoras[i].Nome} - R${transportadoras[i].PrecoPorKm:F2} por Km");
            }

            Console.Write("\nDigite o número da transportadora que deseja alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > transportadoras.Count)
            {
                throw new Exception("Opção inválida! Digite um número da lista.");
            }

            int index = opcao - 1;
            Transportadora transportadoraAtual = transportadoras[index];

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

            Console.Write("\nDeseja alterar o endereço? (S/N): ");
            string alterarEndereco = Console.ReadLine()?.ToUpper();
            
            if (alterarEndereco == "S")
            {
                Console.Clear();
                transportadoraAtual.Endereco = _enderecoController.CriarEndereco();
            }

            _repo.Alterar(index, transportadoraAtual);

            Console.Clear();
            Console.WriteLine("Transportadora alterada com sucesso!");
            Console.WriteLine($"Nome: {transportadoraAtual.Nome}");
            Console.WriteLine($"Preço por KM: R${transportadoraAtual.PrecoPorKm:F2}");
            Console.WriteLine($"Endereço: {transportadoraAtual.Endereco}");
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