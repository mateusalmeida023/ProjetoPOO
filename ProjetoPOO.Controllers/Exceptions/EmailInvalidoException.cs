using System.Transactions;

namespace ProjetoPOO.Controllers.Exceptions;

public class EmailInvalidoException : Exception
{
    public EmailInvalidoException () : 
        base("E-mail inválido") { }
}