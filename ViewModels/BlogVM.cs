using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManager.ViewModels
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string PublishingDateTime { get; set; }
    }
}
