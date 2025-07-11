using ProjetoPOO.Modelos;
using ProjetoPOO.Repository;
using ProjetoPOO.Repository.Arrays;

namespace ProjetoPOO.Repository.Arrays;

public class ClienteRepositorioArray : RepositorioBaseArray<Cliente>
{
    public ClienteRepositorioArray() : base("clientes_array.csv") { }

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
                partes[8], // Cidade
                partes[9], // Estado
                partes[10]  // Cep
            )
        };
    }

    public override List<Cliente> BuscarTodos()
    {
        var clientes = base.BuscarTodos();
        var pedidoRepo = new PedidoRepositorioArray();
        var pedidos = pedidoRepo.BuscarTodos();
        foreach (var cliente in clientes)
        {
            cliente.Pedidos = pedidos.Where(p => p.Cliente != null && p.Cliente.Email == cliente.Email).ToList();
        }
        return clientes;
    }
} 