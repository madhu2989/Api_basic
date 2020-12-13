using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace flipkartWebApi.Controllers
{
    public class flipkartController : ApiController
    {
        DAL d = new DAL();
        DataSet ds = new DataSet();

        //[Route("flipkart/view")]
        //[HttpGet]
        //public DataSet view()
        //{
        //    ds = d.view();
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "all items are here...!");
        //    return ds;
        //}

        //[Route("flipkart/myorders")]
        //[HttpPost]
        //public IHttpActionResult myorders(orderDetails od)
        //{
        //    //List<orderDetails> list = new List<orderDetails>();
        //     var c = d.vieworder(od);
        //     return Ok(c);
        //}

        [Route("flipkart/startorder")]
        [HttpPost]
        public BookingDetails startOrder(startOrder s)
        {
            //List<orderDetails> list = new List<orderDetails>();
            BookingDetails c = d.startOrder(s);
            return c;
        }
    }
}
