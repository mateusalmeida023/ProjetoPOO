using ProjetoPOO.Modelos;

namespace ProjetoPOO.Controllers;

public class EnderecoController
{
    public Endereco CriarEndereco()
    {   
        try
        {
            Console.Write("Rua: ");
            string rua = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(rua))
            {
                throw new Exception("A rua não pode estar vazia.");
            }

            Console.Write("Número: ");
            string numero = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(numero))
            {
                throw new Exception("O número não pode estar vazio.");
            }

            Console.Write("Bairro: ");
            string bairro = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bairro))
            {
                throw new Exception("O bairro não pode estar vazio.");
            }

            Console.Write("CEP: ");
            string cep = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8)
            {
                throw new Exception("CEP inválido. Deve conter 8 dígitos.");
            }

            Console.Write("Cidade: ");
            string cidade = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cidade))
            {
                throw new Exception("A cidade não pode estar vazia.");
            }

            Console.Write("Estado: ");
            string estado = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(estado) || estado.Length != 2)
            {
                throw new Exception("Estado inválido. Use a sigla do estado (ex: RS).");
            }
            
            return new Endereco(rua, numero, bairro, cidade, estado, cep);
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            throw;
        }
    }
} 