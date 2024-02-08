using ToDoList.Entity.Enum;

namespace ToDoList.Entity
{
    public class TaskEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
