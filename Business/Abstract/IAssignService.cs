using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAssignService
    {
        void Add(Assign assign);
        void Delete(Assign assign);
        void Update(Assign assign);
        List<Assign> ListAll();
        Assign GetById(int id);
        List<Assign> ListAllWithIncludes();
        Assign GetByTaskIdAndEmployeeId(int taskId, int employeeId);
        List<Assign> ListAllWithIncludesByEmployeeId(int id);
    }
}
