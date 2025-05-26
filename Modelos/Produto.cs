using System;

namespace ProjetoPOO.Modelos;

public class Produto
{
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public double Preco { get; set; }
    public int Quantidade { get; set; }
    public Fornecedor? Fornecedor { get; set; }

    public Produto()
    {
        // Construtor vazio necessário para object initializer
    }

    public Produto(string nome, string descricao, double preco, int quantidade)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Quantidade = quantidade;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Descrição: {Descricao}, Preço: R${Preco:F2}, Quantidade: {Quantidade}";
    }
}
