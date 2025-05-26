using System;

namespace ProjetoPOO.Modelos;

public class Cliente : Usuario
{
    public required string CPF { get; set; }
    public Endereco? Endereco { get; set; }

    public Cliente()
    {
        // Construtor vazio necessário para object initializer
    }

    public Cliente(string nome, string email, string telefone, string cpf)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        CPF = cpf;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Email: {Email}, Telefone: {Telefone}, CPF: {CPF}\n" +
               $"{Endereco}";
    }
}
