using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.Models
{
    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public int PubYear { get; set; }
        public ICollection<Comic> Comics { get; set; }
    }
}
