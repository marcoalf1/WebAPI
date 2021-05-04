using System.Collections.Generic;
using System.Linq;
using WebAPI.Core.Models.NorthwindDB;

namespace WebAPI.Core.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly NorthwindDBContext _db;

        public CategoryRepo(NorthwindDBContext db)
        {
            _db = db;
        }

        public IEnumerable<Categories> GetAllCategories()
        {
            var categories = _db.Categories;

            return categories;
        }

        public Categories GetCategory(int id)
        {
            var category = _db.Categories.Find(id);

            return category;
        }
    }
}


