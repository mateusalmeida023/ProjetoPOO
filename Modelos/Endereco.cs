namespace ProjetoPOO.Modelos;

public class Endereco
{
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cep { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }

    public Endereco(string rua, string numero, string complemento, string bairro, string cep, string cidade,
        string estado)
    {
        Rua = rua;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
    }

    public static Endereco AdicionarEndereco()
    {   
        try
        {
            Console.Write("Rua: ");
            string rua = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(rua))
            {
                throw new Exception("A rua não pode estar vazia.");
            }

            Console.Write("Numero: ");
            string numero = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(numero))
            {
                throw new Exception("O número não pode estar vazio.");
            }

            Console.Write("Complemento: ");
            string complemento = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(complemento))
            {
                throw new Exception("O complemento não pode estar vazio.");
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
            
            return new Endereco(rua, numero, complemento, bairro, cep, cidade, estado);
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

    public override string ToString()
    {
        return $"Rua: {Rua}, {Numero} {Complemento}, Bairro: {Bairro}, CEP: {Cep}, Cidade: {Cidade}, Estado: {Estado}";
    }
}
