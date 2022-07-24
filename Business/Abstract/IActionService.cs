using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IActionService
    {
        void Add(Entities.Concrete.Action action);
        void Delete(Entities.Concrete.Action action);
        void Update(Entities.Concrete.Action action);
        List<Entities.Concrete.Action> ListAll();
        Entities.Concrete.Action GetById(int id);
        List<Entities.Concrete.Action> ListAllByTaskIdWithIncludes(int id);
    }
}
