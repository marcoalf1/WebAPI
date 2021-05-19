using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebAPI.Core.Models.Helpers;

namespace WebAPI.Core.DTOs
{
    public class CategoryUpdateDTO : AuditEntity
    {
        [Required]
        [MaxLength(250)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public IFormFile Files { get; set; }

    }

}