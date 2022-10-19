using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Models
{
    public class NewsDetail
    {
        public int Id { get; set; }
        public News News { get; set; }
        [ForeignKey("News")]
        public int NewsId { get; set; }
    }
}
