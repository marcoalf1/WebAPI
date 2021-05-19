using Microsoft.EntityFrameworkCore;
using System;
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

        public void CreateCategory(Categories category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _db.Categories.Add(category);

        }

        public void UpdateCategory(Categories category)
        {


            // Nothing
            //throw new NotImplementedException();
        }

        public IEnumerable<Categories> GetAllCategories()
        {
            var categories = _db.Categories;

            return categories;
        }

        public Categories GetCategoryById(int id)
        {
            var category = _db.Categories.Find(id);

            return category;
        }

        public bool SaveChanges()
        {
            try
            {
                return (_db.SaveChanges() >= 0);
            }
            catch (Exception ex)
            {
                foreach (var entry in _db.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
                {
                    foreach (var prop in entry.CurrentValues.Properties)
                    {
                        var val = prop.PropertyInfo.GetValue(entry.Entity);
                        
                        Console.WriteLine($"{prop.ToString()} ~ ({val?.ToString().Length})({val})");
                    }
                }

                return _db.SaveChanges() >= 0;

            }
            
        }

        public void DeleteCategory(Categories category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            
            _db.Categories.Remove(category);

        }
    }
}


