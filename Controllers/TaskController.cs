using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using ToDoList.Entity;
using ToDoList.Entity.Enum;
using ToDoList.Repository.IRepository;

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskEntity task)
        {
            var query = (from Task in (_unitOfWork.Task.GetAll())
                         where Task.Name == task.Name
                         select Task.Name).FirstOrDefault();
            if (query.IsNullOrEmpty())
            {
                if (ModelState.IsValid)
                {
                    TaskEntity entity = new TaskEntity()
                    {
                        Name = task.Name,
                        Description = task.Description,
                        Priority = task.Priority,
                        Status = Status.InProgress,
                        DateTime = DateTime.Today,
                    };

                    await _unitOfWork.Task.Add(entity);
                    await _unitOfWork.Save();
                    TempData["success"] = "Task created";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = "Invalid model";
                    return View();
                }
            }
            else
            {
                TempData["error"] = "Task with this name already exist";
                return View();
            }
        }
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskEntity? task = await _unitOfWork.Task.Get(u => u.Id == id);
            return View(task);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TaskEntity task)
        {
            var query = (from Task in _unitOfWork.Task.GetAll()
                         where Task.Name == task.Name
                         select Task).ToList();

            if (query.IsNullOrEmpty())
            {
                if (ModelState.IsValid)
                {
                    TaskEntity entity = new TaskEntity()
                    {
                        Name = task.Name,
                        Description = task.Description,
                        Priority = task.Priority,
                        Status = Status.InProgress,
                        DateTime = task.DateTime,
                    };

                    _unitOfWork.Task.Update(entity);
                    await _unitOfWork.Save();
                    TempData["success"] = "Task Updated";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = "Invalid model";
                    return View();
                }
            }
            else
            {
                TempData["error"] = "Task with this name already exist";
                return View();
            }
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskEntity? task = await _unitOfWork.Task.Get(u => u.Id == id);
            return View(task);
        }
        [HttpPost]
        public  async Task <IActionResult> Delete(TaskEntity task)
        {
                _unitOfWork.Task.Remove(task);
                await _unitOfWork.Save();
                TempData["success"] = "Task Deleted";
                return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> MarkAsComplete(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskEntity? task = await _unitOfWork.Task.Get(u => u.Id == id);
            task.Status = Status.Completed;
            _unitOfWork.Task.Update(task);
            await _unitOfWork.Save();
            TempData["success"] = "Task Completed";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MarkAsCanceled(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskEntity? task = await _unitOfWork.Task.Get(u => u.Id == id);
            task.Status = Status.Canceled;
            _unitOfWork.Task.Update(task);
            await _unitOfWork.Save();
            TempData["success"] = "Task Canceled";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MarkAsInProgress(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskEntity? task = await _unitOfWork.Task.Get(u => u.Id == id);
            task.Status = Status.InProgress;
            _unitOfWork.Task.Update(task);
            await _unitOfWork.Save();
            TempData["success"] = "Task Returned";
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskEntity? task =await _unitOfWork.Task.Get(u => u.Id == id);
            return View(task);
        }


    }
}
