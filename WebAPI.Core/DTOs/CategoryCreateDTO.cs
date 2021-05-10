using System;
using Microsoft.AspNetCore.Http;
using WebAPI.Core.Models.Helpers;

namespace WebAPI.Core.DTOs
{
    public class CategoryCreateDTO : AuditEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public IFormFile Files { get; set; }

    }

}