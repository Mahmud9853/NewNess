using NewNess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.ViewModels
{
    public class HomeVM
    {
        public List<News> News { get; set; }
        public List<Category> Categories  { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Popular> Populars { get; set; }
    }
}
