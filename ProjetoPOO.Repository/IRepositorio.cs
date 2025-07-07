using System.Net.Security;

namespace ProjetoPOO.Repository;

public interface IRepositorio<T>
{
    public void Incluir(T entity);
    public void Alterar(int id,T entity);
    public void Excluir(int id);
    public List<T> BuscarTodos();
}