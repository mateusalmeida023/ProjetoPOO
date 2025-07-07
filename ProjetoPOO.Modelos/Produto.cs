namespace ProjetoPOO.Modelos;

public class Produto
{
    public required string Nome { get; set; }

    public double Preco { get; set; }
    public int Quantidade { get; set; }
    public Fornecedor Fornecedor { get; set; }

    public Produto()
    {
    }

    public Produto(string nome, double preco, int quantidade, Fornecedor fornecedor)
    {
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
        Fornecedor = fornecedor;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Preço: R${Preco:F2}, Quantidade: {Quantidade}\n" +
               $"Fornecedor: {Fornecedor.Nome}";
    }
}
