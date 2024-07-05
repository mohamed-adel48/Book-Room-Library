using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class cartProducts
    {
        public Book book { get; set; }
        public int quantity { get; set; }
    }
}