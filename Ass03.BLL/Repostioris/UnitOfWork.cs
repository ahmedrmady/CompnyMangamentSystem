using Demo.BLL.Interfaces;
using Demo.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostioris
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _dbContext;

        public UnitOfWork(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
            this.DeparmentRepositry = new DepartmentRepositry(_dbContext);
            this.EmployeeRepository = new EmployeeRepository(_dbContext);

        }

        public IDeparmentRepositry DeparmentRepositry { get ; set ; }
        public IEmployeeRepository EmployeeRepository { get ; set ; }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

       

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

      
    }
}
