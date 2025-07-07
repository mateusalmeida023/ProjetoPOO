using ProjetoPOO.Modelos;
using ProjetoPOO.Repository;

namespace ProjetoPOO.Repository.Lists;

public class ClienteRepositorio : RepositorioBase<Cliente>
{
    public ClienteRepositorio() : base("clientes.csv") { }

    public override string ToCsv(Cliente c)
    {
        return $"{c.Nome},{c.Email},{c.Telefone},{c.CPF},{c.Endereco?.Rua},{c.Endereco?.Numero},{c.Endereco?.Bairro},{c.Endereco?.Cidade},{c.Endereco?.Estado},{c.Endereco?.Cep}";
    }

    public override Cliente FromCsv(string linha)
    {
        var partes = linha.Split(',');
        return new Cliente
        {
            Nome = partes[0],
            Email = partes[1],
            Telefone = partes[2],
            CPF = partes[3],
            Endereco = new ProjetoPOO.Modelos.Endereco(
                partes[4], // Rua
                partes[5], // Numero
                partes[6], // Bairro
                partes[7], // Cidade
                partes[8], // Estado
                partes[9]  // Cep
            )
        };
    }
} 