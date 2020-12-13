using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flipkartWebApi
{
    public class myOrder
    {
        public int orderId { get; set; }
        public string CusName { get; set; }
        public string CusAddress { get; set; }
        public List<orderDetails> order { get; set; }


    }
}