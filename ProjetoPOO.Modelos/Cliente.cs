namespace ProjetoPOO.Modelos;

public class Cliente : Usuario
{
    public required string Senha { get; set; }
    public required string CPF { get; set; }
    public Endereco Endereco { get; set; }
    public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public Cliente()
    {
    }

    public Cliente(string nome, string email, string telefone, string cpf, string senha)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        CPF = cpf;
        Senha = senha;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Email: {Email}, Telefone: {Telefone}, CPF: {CPF}\n" +
               $"{Endereco}";
    }
}
