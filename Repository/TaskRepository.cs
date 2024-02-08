using System.Linq.Expressions;
using ToDoList.DB;
using ToDoList.Entity;
using ToDoList.Repository.IRepository;

namespace ToDoList.Repository
{
    public class TaskRepository : Repository<TaskEntity>, ITaskRepository
    {
        private AppDbContext _dbContext;
        public TaskRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(TaskEntity obj)
        {
            _dbContext.Update(obj);
        }

    }
}
