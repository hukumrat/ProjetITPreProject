using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITaskService
    {
        void Add(Entities.Concrete.Task task);
        void Delete(Entities.Concrete.Task task);
        void Update(Entities.Concrete.Task task);
        List<Entities.Concrete.Task> ListAll();
        Entities.Concrete.Task GetById(int id);
        List<Entities.Concrete.Task> ListAllWithIncludes();
        Entities.Concrete.Task GetByIdWithIncludes(int id);
        List<Entities.Concrete.Task> GetByEmployeeIdWithIncludes(int id);
        List<Entities.Concrete.Task> ListAllByCompanyIdWithIncludes(int id);
        List<Entities.Concrete.Task> ListAllByUserIdWithIncludes(string id);
    }
}
