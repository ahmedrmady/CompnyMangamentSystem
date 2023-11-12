using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public IDeparmentRepositry DeparmentRepositry { get; set; }

        public IEmployeeRepository EmployeeRepository { get; set; }

        public Task<int> Complete();

    }
}
