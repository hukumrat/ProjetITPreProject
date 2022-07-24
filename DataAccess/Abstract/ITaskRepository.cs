using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ITaskRepository:IGenericRepository<Entities.Concrete.Task>
    {
        List<Entities.Concrete.Task> ListAllWithIncludes();
        Entities.Concrete.Task GetByIdWithIncludes(int id);
        List<Entities.Concrete.Task> GetByEmployeeIdWithIncludes(int id);
        List<Entities.Concrete.Task> ListAllByCompanyIdWithIncludes(int id);
        List<Entities.Concrete.Task> ListAllByUserIdWithIncludes(string id);
    }
}
