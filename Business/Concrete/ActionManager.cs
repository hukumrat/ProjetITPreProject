using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ActionManager : IActionService
    {
        private readonly IActionRepository _actionRepository;
        public ActionManager(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }

        public void Add(Entities.Concrete.Action action)
        {
            _actionRepository.Add(action);
        }

        public void Delete(Entities.Concrete.Action action)
        {
            _actionRepository.Delete(action);
        }

        public Entities.Concrete.Action GetById(int id)
        {
            return _actionRepository.GetById(id);
        }

        public List<Entities.Concrete.Action> ListAllByTaskIdWithIncludes(int id)
        {
            return _actionRepository.ListAllByTaskIdWithIncludes(id);
        }

        public List<Entities.Concrete.Action> ListAll()
        {
            return _actionRepository.ListAll();
        }

        public void Update(Entities.Concrete.Action action)
        {
            _actionRepository.Update(action);
        }
    }
}
