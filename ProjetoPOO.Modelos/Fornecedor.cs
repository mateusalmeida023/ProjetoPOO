namespace ConsoleApp1.ProjetoPOO.Modelos;

public class Fornecedor : Usuario
{
    public required string Descricao { get; set; }
    public Endereco Endereco { get; set; }
    public new int Id { get; set; }

    public Fornecedor()
    {
    }

    public Fornecedor(string nome, string email, string telefone, string descricao, int id)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Descricao = descricao;
        Id = id;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, E-mail: {Email}, Telefone: {Telefone}, Descrição: {Descricao}\n" +
               $"{Endereco}";
    }
}
