using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core.Models.NorthwindDB;

namespace WebAPI.Core.Repository
{
    public interface ICategoryRepo
    {
        bool SaveChanges();
        
        IEnumerable<Categories> GetAllCategories();
        Categories GetCategory(int id);
        void CreateCategory(Categories category);
        
    }
}
