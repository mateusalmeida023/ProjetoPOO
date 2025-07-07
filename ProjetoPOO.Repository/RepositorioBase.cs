using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjetoPOO.Repository;

namespace ProjetoPOO.Repository.Lists;

public abstract class RepositorioBase<T> : IRepositorio<T>
{
    protected List<T> _itens;
    protected string _caminhoArquivo;

    public int Count => _itens.Count;

    public RepositorioBase(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;
        _itens = new List<T>();
        Carregar();
    }

    public void Incluir(T entidade)
    {
        _itens.Add(entidade);
        Salvar();
    }

    public void Excluir(int index)
    {
        if (index >= 0 && index < _itens.Count)
        {
            _itens.RemoveAt(index);
            Salvar();
        }
    }

    public void Alterar(int index, T entidade)
    {
        if (index >= 0 && index < _itens.Count)
        {
            _itens[index] = entidade;
            Salvar();
        }
    }
    
    public virtual List<T> BuscarTodos()
    {
        return new List<T>(_itens);
    }
    public abstract string ToCsv(T entidade);
    public abstract T FromCsv(string linha);

    public virtual void Salvar()
    {
        File.WriteAllLines(_caminhoArquivo, _itens.Select(ToCsv));
    }

    public virtual void Carregar()
    {
        _itens.Clear();
        if (File.Exists(_caminhoArquivo))
        {
            foreach (var linha in File.ReadAllLines(_caminhoArquivo))
            {
                if (!string.IsNullOrWhiteSpace(linha))
                    _itens.Add(FromCsv(linha));
            }
        }
    }
} 