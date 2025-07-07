namespace ProjetoPOO.Controllers.Exceptions;

public class TelefoneInvalidoException : Exception
{
    public TelefoneInvalidoException () : 
        base("Telefone inválido. Deve conter pelo menos 10 dígitos.") { }
}