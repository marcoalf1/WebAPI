using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Core.Models.NorthwindDB;

namespace WebAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        //GET: api/Categories
        [HttpGet]
        public ActionResult <IEnumerable<Categories>> GetAllCategories()
        {

        }

        //GET: api/Categories/{id}
        [HttpGet("{id}")]
        public ActionResult <Categories> GetCategoryById(int id)
        {

        }
    }

}