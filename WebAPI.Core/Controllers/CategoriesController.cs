using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Core.Models.NorthwindDB;
using WebAPI.Core.Repository;

namespace WebAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _repo;

        public CategoriesController(ICategoryRepo repo)
        {
            _repo = repo;
        }

        //GET: api/Categories
        [HttpGet]
        public ActionResult <IEnumerable<Categories>> GetAllCategories()
        {
            var categories = _repo.GetAllCategories();

            return categories.ToList();

        }

        //GET: api/Categories/{id}
        [HttpGet("{id}")]
        public ActionResult <Categories> GetCategoryById([FromBody] int id)
        {
            var category = _repo.GetCategory(id);

            return Ok(category);

        }
    }

}