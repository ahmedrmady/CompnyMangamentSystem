using Demo.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        void Add(T department);
        void Update(T department);
        void Delete(T department);
        Task<IEnumerable<T>> GetAll();
       Task<T> Get(int id);

    }
}
