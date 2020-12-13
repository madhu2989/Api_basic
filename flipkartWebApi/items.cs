using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flipkartWebApi
{
    public class Items
    {
        
            //public int orderDetailId { get; set; }

            public int itemId { get; set; }
            public string itemName { get; set; }
            public string size { get; set; }
            public decimal price { get; set; }
            public int quantity { get; set; }
        
    }
}