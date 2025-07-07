namespace ProjetoPOO.Controllers.Exceptions;

public class CpfInvalidoException : Exception
{
    public CpfInvalidoException() 
        : base("CPF inválido. Deve conter 11 dígitos.") {}
    
}