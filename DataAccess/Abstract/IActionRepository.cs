using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IActionRepository : IGenericRepository<Entities.Concrete.Action>
    {
        List<Entities.Concrete.Action> ListAllByTaskIdWithIncludes(int id);
    }
}
