using ProjetoPOO.Modelos;

namespace ProjetoPOO.Repository;

public class Data
{
    public Transportadora[] Transportadoras;
    public Cliente[] Clientes;
    public Fornecedor[] Fornecedores;
    public Produto[] Produtos;

    public Data()
    {
        Produtos = new Produto[200];
        Fornecedores = new Fornecedor[100];
        Transportadoras = new Transportadora[100];
        Clientes = new Cliente[100];
    }
} 