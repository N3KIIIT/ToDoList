﻿namespace ToDoList.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITaskRepository Task{ get; }
        void Save();
    }
}
