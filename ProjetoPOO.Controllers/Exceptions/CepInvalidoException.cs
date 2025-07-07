namespace ProjetoPOO.Controllers.Exceptions;

public class CepInvalidoException : Exception
{
    public CepInvalidoException()
        : base("CEP inválido. Deve conter 8 dígitos.") { }
}