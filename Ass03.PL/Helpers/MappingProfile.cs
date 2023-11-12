using Demo.DAL.Data.Models;
using Demo.PL.ViewModels;
using AutoMapper;

namespace Demo.PL.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();

            CreateMap<DepartmentViewModel,Department>().ReverseMap();

            CreateMap<ApplicationUser,UserViewModel>().ReverseMap();

        }
    }
}
