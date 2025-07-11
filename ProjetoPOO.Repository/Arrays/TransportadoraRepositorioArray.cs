using ProjetoPOO.Modelos;
using ProjetoPOO.Repository;
using ProjetoPOO.Repository.Arrays;

namespace ProjetoPOO.Repository.Arrays;

public class TransportadoraRepositorioArray : RepositorioBaseArray<Transportadora>
{
    public TransportadoraRepositorioArray() : base("transportadoras_array.csv") { }

    public override string ToCsv(Transportadora t)
    {
        return $"{t.Nome},{t.PrecoPorKm},{t.Endereco?.Rua},{t.Endereco?.Numero},{t.Endereco?.Bairro},{t.Endereco?.Cidade},{t.Endereco?.Estado},{t.Endereco?.Cep}";
    }

    public override Transportadora FromCsv(string linha)
    {
        var partes = linha.Split(',');
        return new Transportadora
        {
            Nome = partes[0],
            PrecoPorKm = double.Parse(partes[1]),
            Endereco = new ProjetoPOO.Modelos.Endereco(
                partes[2], // Rua
                partes[3], // Numero
                partes[4], // Bairro
                partes[5], // Cidade
                partes[6], // Estado
                partes[7]  // Cep
            )
        };
    }
} 