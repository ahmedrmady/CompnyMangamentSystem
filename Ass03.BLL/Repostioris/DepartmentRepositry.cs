using Demo.BLL.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostioris
{
    public class DepartmentRepositry : GeneicRepository<Department>, IDeparmentRepositry
    {
        public DepartmentRepositry(AppDBContext dBContext):base(dBContext)
        {
            
        }
    }
}
