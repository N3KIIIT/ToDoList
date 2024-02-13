using System.Linq.Expressions;

namespace ToDoList.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> Get(Expression<Func<T,bool>> filter);
        Task Add(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> values);

    }
}
