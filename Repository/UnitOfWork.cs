using ToDoList.DB;
using ToDoList.Repository.IRepository;

namespace ToDoList.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public ITaskRepository Task { get; private set;}
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Task = new TaskRepository(dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
