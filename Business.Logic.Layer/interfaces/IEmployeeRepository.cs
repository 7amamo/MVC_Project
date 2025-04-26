using Data.Access.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic.Layer.interfaces
{
    public interface IEmployeeRepository : IGenearicRepository<Employee>
    {
        public Task <IEnumerable<Employee>> GetAllAsync(string name);
        public Task <IEnumerable<Employee>> GetAllWithDepartmentAsync();

    }
}
