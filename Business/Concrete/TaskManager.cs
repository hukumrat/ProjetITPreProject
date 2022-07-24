using Business.Abstract;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TaskManager:ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskManager(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Add(Entities.Concrete.Task task)
        {
            _taskRepository.Add(task);
        }

        public void Delete(Entities.Concrete.Task task)
        {
            _taskRepository.Delete(task);
        }

        public List<Entities.Concrete.Task> GetByEmployeeIdWithIncludes(int id)
        {
            return _taskRepository.GetByEmployeeIdWithIncludes(id);
        }

        public Entities.Concrete.Task GetById(int id)
        {
            return _taskRepository.GetById(id);
        }

        public Entities.Concrete.Task GetByIdWithIncludes(int id)
        {
            return _taskRepository.GetByIdWithIncludes(id);
        }

        public List<Entities.Concrete.Task> ListAll()
        {
            return _taskRepository.ListAll();
        }

        public List<Entities.Concrete.Task> ListAllByCompanyIdWithIncludes(int id)
        {
            return _taskRepository.ListAllByCompanyIdWithIncludes(id);
        }

        public List<Entities.Concrete.Task> ListAllByUserIdWithIncludes(string id)
        {
            return _taskRepository.ListAllByUserIdWithIncludes(id);
        }

        public List<Entities.Concrete.Task> ListAllWithIncludes()
        {
            return _taskRepository.ListAllWithIncludes();
        }

        public void Update(Entities.Concrete.Task task)
        {
            _taskRepository.Update(task);
        }
    }
}
