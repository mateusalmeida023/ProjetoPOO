using ProjetoPOO.Modelos;
using ProjetoPOO.Repository;
using ProjetoPOO.Repository.Arrays;

namespace ProjetoPOO.Repository.Arrays;

public class FornecedorRepositorioArray : RepositorioBaseArray<Fornecedor>
{
    public FornecedorRepositorioArray() : base("fornecedores_array.csv") { }

    public override string ToCsv(Fornecedor f)
    {
        return $"{f.Nome},{f.Email},{f.Telefone},{f.Descricao},{f.Id},{f.Endereco?.Rua},{f.Endereco?.Numero},{f.Endereco?.Bairro},{f.Endereco?.Cidade},{f.Endereco?.Estado},{f.Endereco?.Cep}";
    }

    public override Fornecedor FromCsv(string linha)
    {
        var partes = linha.Split(',');
        return new Fornecedor
        {
            Nome = partes[0],
            Email = partes[1],
            Telefone = partes[2],
            Descricao = partes[3],
            Id = int.Parse(partes[4]),
            Endereco = new ProjetoPOO.Modelos.Endereco(
                partes[5], // Rua
                partes[6], // Numero
                partes[7], // Bairro
                partes[8], // Cidade
                partes[9], // Estado
                partes[10] // Cep
            )
        };
    }
} 