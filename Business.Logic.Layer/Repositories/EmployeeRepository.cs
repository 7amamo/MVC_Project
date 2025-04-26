
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic.Layer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository (DataContext dataContext) : base (dataContext) { }

        public async Task <IEnumerable<Employee>> GetAllAsync(string name)     
           => await _dbSet.Where(e=>e.Name.ToLower().Contains( name.ToLower())).Include(e => e.Department).ToListAsync();
        

        public async Task <IEnumerable<Employee>> GetAllWithDepartmentAsync() =>
            await _dbSet.Include(e => e.Department).ToListAsync();
        
    }
}
