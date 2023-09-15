using AutoMapper;
using DataLayer.Entities;
using System.CodeDom;
using ViewModels;

namespace UserHelpPageTemplate.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Application, ApplicationVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();            
        }
    }
}
