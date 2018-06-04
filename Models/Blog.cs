using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManager.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime PublishingDateTime { get; set; }
    }
}
