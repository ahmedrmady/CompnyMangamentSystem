using Demo.BLL.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostioris
{
    public class GeneicRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly AppDBContext _dBContext;

        public GeneicRepository(AppDBContext dBContext) => _dBContext = dBContext;

        public void Add(T entity)

        => _dBContext.Set<T>().Add(entity);

        public void Delete(T entity) => _dBContext.Set<T>().Remove(entity);

        public async Task<T> Get(int id)
        {
            // first method
            //var department = _dBContext.Set<T>().Local.FirstOrDefault(D=>D.Id==id);  
            //if (department == null)
            //{
            //     department = _dBContext.Set<T>().FirstOrDefault(D => D.Id == id);
            //}
            //second Method
            var entity = await _dBContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Department))
                return  (IEnumerable<T>)await _dBContext.Departments.Include(D => D.Employees).AsNoTracking().ToListAsync();
            else
                return  (IEnumerable<T>) await _dBContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();



        }   
        public void Update(T entity) => _dBContext.Set<T>().Update(entity);


    }
}
