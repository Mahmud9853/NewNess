using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool IsDeactive { get; set; }
        public string InstagramLink { get; set; }
        public string InstagramName { get; set; }
        public string Email { get; set; }
        public string NewsImages { get; set; }
        [NotMapped]
        public IFormFile Photos { get; set; }
        public string YoutubeLink { get; set; }
        public string YoutubemName { get; set; }

    }
}
