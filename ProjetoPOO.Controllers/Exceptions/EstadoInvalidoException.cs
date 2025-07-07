namespace ProjetoPOO.Controllers.Exceptions;

public class EstadoInvalidoException : Exception
{
    public EstadoInvalidoException()
        : base("Estado inválido. Use a sigla do estado (ex: RS).") { }
}