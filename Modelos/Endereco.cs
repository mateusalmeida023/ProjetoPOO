namespace ProjetoPOO.Modelos;

public class Endereco
{
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string CEP { get; set; }

    public Endereco(string rua, string numero, string bairro, string cidade, string estado, string cep)
    {
        Rua = rua;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
    }

    public override string ToString()
    {
        return $"Endereço: {Rua}, {Numero} - {Bairro}, {Cidade}/{Estado} - CEP: {CEP}";
    }
}