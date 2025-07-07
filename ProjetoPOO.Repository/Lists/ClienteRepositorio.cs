using ProjetoPOO.Modelos;
using ProjetoPOO.Repository;

namespace ProjetoPOO.Repository.Lists;

public class ClienteRepositorio : RepositorioBase<Cliente>
{
    public ClienteRepositorio() : base("clientes.csv") { }

    public override string ToCsv(Cliente c)
    {
        return $"{c.Nome},{c.Email},{c.Senha},{c.Telefone},{c.CPF},{c.Endereco?.Rua},{c.Endereco?.Numero},{c.Endereco?.Bairro},{c.Endereco?.Cidade},{c.Endereco?.Estado},{c.Endereco?.Cep}";
    }

    public override Cliente FromCsv(string linha)
    {
        var partes = linha.Split(',');
        return new Cliente
        {
            Nome = partes[0],
            Email = partes[1],
            Senha = partes[2],
            Telefone = partes[3],
            CPF = partes[4],
            Endereco = new ProjetoPOO.Modelos.Endereco(
                partes[5], // Rua
                partes[6], // Numero
                partes[7], // Bairro
                partes[8], // Cidade1
                partes[9], // Estado
                partes[10]  // Cep
            )
        };
    }
} 