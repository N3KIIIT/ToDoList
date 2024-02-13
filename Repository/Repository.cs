using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoList.DB;
using ToDoList.Repository.IRepository;

namespace ToDoList.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }
        public async Task Add(T item)
        {
            await _dbContext.AddAsync(item);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Remove(T item)
        {
             dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> values)
        {
            dbSet.RemoveRange(values);
        }


    }
}
