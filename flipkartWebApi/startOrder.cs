using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace flipkartWebApi
{
    public class startOrder
    {
        [Required(ErrorMessage="Please enter your name.")]
        [RegularExpression("[A-Za-z]",ErrorMessage="Your name is not in correct format.")]
        public string CusName { get; set; }
        public string CusAddress { get; set; }
        public List<orderDetails> ordDet { get; set; }

    }
}