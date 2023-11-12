using Demo.DAL.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo.PL.ViewModels
{
    public class DepartmentViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "Date Of Ceration Is Required")]

        public DateTime DateOfCreation { get; set; }


        // navigatinaol property =>[many]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
