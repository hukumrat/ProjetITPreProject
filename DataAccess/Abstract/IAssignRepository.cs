using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAssignRepository:IGenericRepository<Assign>
    {
        List<Assign> ListAllWithIncludes();
        Assign GetByTaskIdAndEmployeeId(int taskId, int employeeId);
        List<Assign> ListAllWithIncludesByEmployeeId(int id);
    }
}
