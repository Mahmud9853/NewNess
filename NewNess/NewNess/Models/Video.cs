using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public bool IsDeactive { get; set; }
    }
}
