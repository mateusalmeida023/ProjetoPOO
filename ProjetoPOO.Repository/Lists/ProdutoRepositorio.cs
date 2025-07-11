using ProjetoPOO.Modelos;
using ProjetoPOO.Repository;

namespace ProjetoPOO.Repository.Lists;

public class ProdutoRepositorio : RepositorioBase<Produto>
{
    public ProdutoRepositorio() : base("produtos.csv") { }

    public override string ToCsv(Produto p)
    {
        return $"{p.Nome},{p.Preco},{p.Quantidade},{p.Fornecedor?.Id ?? 0}";
    }

    public override Produto FromCsv(string linha)
    {
        var partes = linha.Split(',');
        return new Produto
        {
            Nome = partes[0],
            Preco = double.Parse(partes[1]),
            Quantidade = int.Parse(partes[2]),
        };
    }

    public List<Produto> BuscarProdutosDisponiveis()
    {
        return BuscarTodos().Where(p => p.Quantidade > 0).ToList();
    }

    public List<Produto> BuscarTodosProdutos()
    {
        return BuscarTodos();
    }
} 