using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core.Models.NorthwindDB;

namespace WebAPI.Core.Models
{
    public class ProductsCategoryViewModel
    {
        public List<Products> products { get; set; }
        public Categories category { get; set; }
    }
}
