using Microsoft.AspNetCore.Http;
using System;
using WebAPI.Core.Models.Helpers;

namespace WebAPI.Core.DTOs
{
    public class CategoryCreateDTO : AuditEntity
    {        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        //public IFormFile Image { get; set; }

    }

}