namespace ProjetoPOO.Modelos;

public class Transportadora
{
    public required string Nome { get; set; }
    public double PrecoPorKm { get; set; }
    public Endereco Endereco { get; set; }

    public Transportadora()
    {
    }

    public Transportadora(string nome, double preco)
    {
        Nome = nome;
        PrecoPorKm = preco;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Preço por KM: R${PrecoPorKm:F2}\n" +
               $"{Endereco}";
    }
}