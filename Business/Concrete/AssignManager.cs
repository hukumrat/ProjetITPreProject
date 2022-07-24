using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AssignManager : IAssignService
    {
        private readonly IAssignRepository _assingRepository;
        public AssignManager(IAssignRepository assingRepository)
        {
            _assingRepository = assingRepository;
        }

        public void Add(Assign assign)
        {
            _assingRepository.Add(assign);
        }

        public void Delete(Assign assign)
        {
            _assingRepository.Delete(assign);
        }

        public Assign GetById(int id)
        {
            return _assingRepository.GetById(id);
        }

        public Assign GetByTaskIdAndEmployeeId(int taskId, int employeeId)
        {
            return _assingRepository.GetByTaskIdAndEmployeeId(taskId, employeeId);
        }

        public List<Assign> ListAll()
        {
            return _assingRepository.ListAll();
        }

        public List<Assign> ListAllWithIncludes()
        {
            return _assingRepository.ListAllWithIncludes();
        }

        public List<Assign> ListAllWithIncludesByEmployeeId(int id)
        {
            return _assingRepository.ListAllWithIncludesByEmployeeId(id);
        }

        public void Update(Assign assign)
        {
            _assingRepository.Update(assign);
        }
    }
}
