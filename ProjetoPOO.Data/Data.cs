using ConsoleApp1.ProjetoPOO.Modelos;

namespace ConsoleApp1.ProjetoPOO.Data;

public static class Data
{
    public static Transportadora[] Transportadoras = new Transportadora[100];
    public static int TransportadorasCount = 0;

    public static Cliente[] Clientes = new Cliente[100];
    public static int ClientesCount = 0;

    public static Fornecedor[] Fornecedores = new Fornecedor[100];
    public static int FornecedoresCount = 0;

    public static Produto[] Produtos = new Produto[100];
    public static int ProdutosCount = 0;
} 