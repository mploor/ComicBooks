using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.Models
{
    public class Comic
    {
        public int Id { get; set; }
        public int IssueNum { get; set; }
        public string Condition { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Value { get; set; }
        public bool Cgc { get; set; }
        public bool WishList { get; set; }
        public string   ImageUrl { get; set; }
        public int PicId { get; set; }
        public string User { get; set; }
        public Title Title { get; set; }
    }
}
