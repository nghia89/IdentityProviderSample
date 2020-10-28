using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.WebApi.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Publisher { get; set; }
        public Double Price { get; set; }
        public string Author { get; set; }
    }
}
