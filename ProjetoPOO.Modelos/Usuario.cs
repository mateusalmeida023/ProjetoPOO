namespace ConsoleApp1.ProjetoPOO.Modelos;

public abstract class Usuario
{
    public required string Nome { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public int Id { get; set; }
    
}
