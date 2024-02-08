using ToDoList.Entity;

namespace ToDoList.Repository.IRepository
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        void Update(TaskEntity obj);
    }
}
