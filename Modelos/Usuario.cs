using System;

namespace ProjetoPOO.Modelos;

public abstract class Usuario
{
    public string Nome;
    public string Telefone;
    public string Email { get; set; }
    public int Id { get; set; }
    
}
