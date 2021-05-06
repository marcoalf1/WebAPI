using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Core.DTOs;
using WebAPI.Core.Models.NorthwindDB;
using WebAPI.Core.Repository;

namespace WebAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //GET: api/Categories
        [HttpGet]
        public ActionResult <IEnumerable<CategoryReadDTO>> GetAllCategories()
        {
            var categories = _repo.GetAllCategories();

            return Ok(_mapper.Map<IEnumerable<CategoryReadDTO>>(categories)) ;

        }

        //GET: api/Categories/{id}
        [HttpGet("{id}")]
        public ActionResult <CategoryReadDTO> GetCategoryById(int id)
        {
            var category = _repo.GetCategory(id);

            if (category != null)
            {
                return Ok(_mapper.Map<CategoryReadDTO>(category));        
            }

            return NotFound();
        }

        //POST: api/Categories
        [HttpPost]
        public ActionResult<CategoryReadDTO> CreateCategory(CategoryCreateDTO categoryCreateDTO)
        {
            
            var categoryModel = _mapper.Map<Categories>(categoryCreateDTO);
            _repo.CreateCategory(categoryModel);
            _repo.SaveChanges();

            var categoryReadDTO = _mapper.Map<CategoryReadDTO>(categoryModel);

            return Ok(categoryReadDTO);
        }

    }

}