using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult<CategoryReadDTO> CreateCategory([FromForm] CategoryCreateDTO categoryCreateDTO)
        {
            var files = categoryCreateDTO.Files;

            // Saving Image on Database
            if (files.Length > 0)
            {
                //using (var fs = new FileStream(files.FileName, FileMode.Create))
                using (var fs = new FileStream(files.FileName, FileMode.Open,FileAccess.Read))
                {
                    //files.CopyTo(fs);
                    
                    categoryCreateDTO.Picture = new byte[fs.Length]; //fileStream.ToArray();
                    fs.Read(categoryCreateDTO.Picture, 0, Convert.ToInt32(fs.Length));
                }
            }

            var categoryModel = _mapper.Map<Categories>(categoryCreateDTO);
            _repo.CreateCategory(categoryModel);
            _repo.SaveChanges();

            var categoryReadDTO = _mapper.Map<CategoryReadDTO>(categoryModel);

            // Decodificar base64 online
            // https://base64.guru/converter/decode/image
            // https://codebeautify.org/base64-to-image-converter

            return Ok(categoryReadDTO);
        }

    }

}