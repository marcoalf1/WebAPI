using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core.Models.NorthwindDB;

namespace WebAPI.Core.Repository
{
    public interface ICategoryRepo
    {
        IEnumerable<Categories> GetAllCategories();
        Categories GetCategory(int id);
        
    }
}
