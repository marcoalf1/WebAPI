using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI.Core.DTOs;
using WebAPI.Core.Models;
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
        private readonly ILogger<CategoriesController> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public CategoriesController(ICategoryRepo repo, IMapper mapper, ILogger<CategoriesController> logger, IHostEnvironment hostEnvironment)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        //GET: api/Categories
        [HttpGet()]
        public ActionResult<IEnumerable<CategoryReadDTO>> GetAllCategories()
        {
            var categories = _repo.GetAllCategories();

            return Ok(_mapper.Map<IEnumerable<CategoryReadDTO>>(categories));

        }

        //GET: api/Categories/{id}
        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<CategoryReadDTO> GetCategoryById(int id)
        {
            var category = _repo.GetCategoryById(id);

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
            // SUBE UNA SOLA IMAGEN A UN CAMPO DE TIPO Byte[] en una tabla
            if (categoryCreateDTO.Files.FileName != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    categoryCreateDTO.Files.OpenReadStream().CopyTo(ms);
                    // return arreglo de bytes
                    categoryCreateDTO.Picture = ms.ToArray();
                }
            }

            var categoryModel = _mapper.Map<Categories>(categoryCreateDTO);
            _repo.CreateCategory(categoryModel);
            _repo.SaveChanges();

            var categoryReadDTO = _mapper.Map<CategoryReadDTO>(categoryModel);

            // Decodificar base64 online
            // https://base64.guru/converter/decode/image
            // https://codebeautify.org/base64-to-image-converter

            return CreatedAtRoute(nameof(GetCategoryById), new { id = categoryReadDTO.CategoryId }, categoryReadDTO);
            //return Ok(categoryReadDTO);

        }

        //PUT: api/Categories/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCategory([FromForm] CategoryUpdateDTO categoryUpdateDTO, int id)
        {
            var categoryRepo = _repo.GetCategoryById(id);

            if (categoryRepo == null)
            {
                return NotFound();
            }

            // SUBE UNA SOLA IMAGEN A UN CAMPO DE TIPO Byte[] en una tabla
            if (categoryUpdateDTO.Files.FileName != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    categoryUpdateDTO.Files.OpenReadStream().CopyTo(ms);
                    // return arreglo de bytes
                    categoryUpdateDTO.Picture = ms.ToArray();
                }
            }

            _mapper.Map(categoryUpdateDTO, categoryRepo);

            _repo.UpdateCategory(categoryRepo);

            _repo.SaveChanges();

            return NoContent();
        }

        //DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var category = _repo.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            _repo.DeleteCategory(category);
            _repo.SaveChanges();

            return NoContent();

        }

    }

}