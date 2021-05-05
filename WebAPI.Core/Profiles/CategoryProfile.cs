using AutoMapper;
using WebAPI.Core.DTOs;
using WebAPI.Core.Models.NorthwindDB;

namespace WebAPI.Core.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Source -> Target
            CreateMap<Categories, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Categories>();            
        }
    }
    
}