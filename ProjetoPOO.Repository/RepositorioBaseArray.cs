using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjetoPOO.Repository;

namespace ProjetoPOO.Repository.Arrays;

public abstract class RepositorioBaseArray<T> : IRepositorio<T>
{
    protected T[] _itens;
    protected int _tamanhoAtual;
    protected int _capacidade;
    protected string _caminhoArquivo;

    public int Count => _tamanhoAtual;

    public RepositorioBaseArray(string caminhoArquivo, int capacidadeInicial = 100)
    {
        _caminhoArquivo = caminhoArquivo;
        _capacidade = capacidadeInicial;
        _itens = new T[_capacidade];
        _tamanhoAtual = 0;
        Carregar();
    }

    public void Incluir(T entidade)
    {
        if (_tamanhoAtual >= _capacidade)
        {
            ExpandirArray();
        }
        
        _itens[_tamanhoAtual] = entidade;
        _tamanhoAtual++;
        Salvar();
    }

    public void Excluir(int index)
    {
        if (index >= 0 && index < _tamanhoAtual)
        {
            for (int i = index; i < _tamanhoAtual - 1; i++)
            {
                _itens[i] = _itens[i + 1];
            }
            _tamanhoAtual--;
            Salvar();
        }
    }

    public void Alterar(int index, T entidade)
    {
        if (index >= 0 && index < _tamanhoAtual)
        {
            _itens[index] = entidade;
            Salvar();
        }
    }
    
    public virtual List<T> BuscarTodos()
    {
        var resultado = new List<T>();
        for (int i = 0; i < _tamanhoAtual; i++)
        {
            resultado.Add(_itens[i]);
        }
        return resultado;
    }

    protected void ExpandirArray()
    {
        _capacidade *= 2;
        var novoArray = new T[_capacidade];
        for (int i = 0; i < _tamanhoAtual; i++)
        {
            novoArray[i] = _itens[i];
        }
        _itens = novoArray;
    }

    public abstract string ToCsv(T entidade);
    public abstract T FromCsv(string linha);

    public virtual void Salvar()
    {
        var linhas = new List<string>();
        for (int i = 0; i < _tamanhoAtual; i++)
        {
            linhas.Add(ToCsv(_itens[i]));
        }
        File.WriteAllLines(_caminhoArquivo, linhas);
    }

    public virtual void Carregar()
    {
        _tamanhoAtual = 0;
        if (File.Exists(_caminhoArquivo))
        {
            var linhas = File.ReadAllLines(_caminhoArquivo);
            foreach (var linha in linhas)
            {
                if (!string.IsNullOrWhiteSpace(linha))
                {
                    if (_tamanhoAtual >= _capacidade)
                    {
                        ExpandirArray();
                    }
                    _itens[_tamanhoAtual] = FromCsv(linha);
                    _tamanhoAtual++;
                }
            }
        }
    }
} 