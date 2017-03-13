using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookStore.Models
{
    [Serializable]
    public class CartItem
    {
        public Product product { get; set; }
        public int quanlity { get; set; }
    }
}