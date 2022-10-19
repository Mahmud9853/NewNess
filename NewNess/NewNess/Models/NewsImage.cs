using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Models
{
    public class NewsImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
