using System.Collections.Generic;
using System.Linq;
using WebAPI.Core.Models.NorthwindDB;

namespace WebAPI.Core.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly NorthwindDBContext db = new NorthwindDBContext();
        public IEnumerable<Categories> GetCategories()
        {
            var categories = db.Categories;

            return categories.ToList();
        }

        public Categories GetCategory(int id)
        {
            var category = db.Categories.Find(id);

            return category;
        }
    }
}


