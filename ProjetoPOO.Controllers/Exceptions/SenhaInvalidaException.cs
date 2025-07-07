namespace ProjetoPOO.Controllers.Exceptions;

public class SenhaInvalidaException : Exception
{
    public SenhaInvalidaException() 
        : base("Senha inválida. A senha deve conter no mínimo 6 caracteres.") {}
}