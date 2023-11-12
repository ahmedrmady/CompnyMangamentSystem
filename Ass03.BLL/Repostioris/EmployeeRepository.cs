using Demo.BLL.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostioris
{
    public class EmployeeRepository:GeneicRepository<Employee>,IEmployeeRepository
    {

        public EmployeeRepository(AppDBContext dbContext): base(dbContext)
        { 
                
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dBContext.Employees.Where(
                E => E.Address.ToLower()
                               .Contains(address.ToLower())
                );
        }

        public IQueryable<Employee> SearchByName(string Name)
        {

            return _dBContext.Employees.Where(E => E.Name.ToLower().Contains(Name));
        }
    }
}
