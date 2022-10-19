using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime CreateTime { get; set; }
        public List<NewsImage> NewsImages { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }
        public string By { get; set; }


    }
}
