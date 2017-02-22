using ComicBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.ViewModels
{
    public class TitleSummary
    {
        public int TitleId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int PubYear { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalValue { get; set; }
    }
}
