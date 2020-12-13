using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace flipkartWebApi
{
    public class DAL
    {
        string con = "Data Source=IDEA-PC\\ECSQLEXPRESS;Initial Catalog=flipkart;Integrated Security=True";
        SqlConnection sqlcon;

        DataSet ds = new DataSet();
        Dictionary<int, List<Items>> dicitems = new Dictionary<int, List<Items>>();
        Dictionary<string, decimal> dicSize = new Dictionary<string, decimal>();
        Items i = new Items();
        public void itemDetails()
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                string view = string.Format("select * from itemsTable", con);
                SqlCommand cmd = new SqlCommand(view, sqlcon);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);

                DataSet ds1 = new DataSet();
                string views = string.Format("select * from sizeTable", con);
                SqlCommand cmd1 = new SqlCommand(views, sqlcon);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                sda1.Fill(ds1);

                List<Items> rderDet = new List<Items>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    rderDet.Add(new Items { itemId = Convert.ToInt16(dr["itemId"]), itemName = Convert.ToString(dr["itemName"]), price = Convert.ToDecimal(dr["price"]), size = Convert.ToString(dr["size"]) });
                }

                foreach (Items o in rderDet)
                {
                    dicitems.Add(o.itemId, rderDet);
                }

                List<Items> rderDet1 = new List<Items>();
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    rderDet1.Add(new Items { size = Convert.ToString(dr["sizeName"]), price = Convert.ToDecimal(dr["price"]) });
                }

                foreach (Items o in rderDet1)
                {
                    dicSize.Add(o.size, o.price);
                }

            }
        }

        //public DataSet view()
        //{
        //    using (SqlConnection sqlcon = new SqlConnection(con))
        //    {
        //        string view = string.Format("select * from itemsTable", con);
        //        SqlCommand cmd = new SqlCommand(view, sqlcon);
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        sda.Fill(ds);
        //        return ds;
        //    }

        //}

        //public myOrder vieworder(orderDetails id)
        //{
        //    using (SqlConnection sqlcon = new SqlConnection(con))
        //    {
        //        sqlcon.Open();
        //        string viewot = string.Format("select * from orderTable", con);
        //        SqlCommand cmd = new SqlCommand(viewot, sqlcon);
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        sda.Fill(ds);
        //        sqlcon.Close();

        //        sqlcon.Open();
        //        DataSet ds1 = new DataSet();
        //        string viewod = string.Format("select * from orderDetails", con);
        //        SqlCommand cmd1 = new SqlCommand(viewod, sqlcon);
        //        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
        //        sda1.Fill(ds1);
        //        sqlcon.Close();


        //        var orDetails = ds1.Tables[0].AsEnumerable().Select(or => new orderDetails()
        //            {
        //                orderId = int.Parse(or["orderId"].ToString()),
        //                itemId = int.Parse(or["itemId"].ToString()),
        //                quantity = int.Parse(or["quantity"].ToString())
        //            }).ToList();

        //        var myord = ds.Tables[0].AsEnumerable().Select(myo => new myOrder()
        //        {
        //            CusName = myo["cusName"].ToString(),
        //            CusAddress = myo["cusAddress"].ToString(),
        //            order = new List<orderDetails>()
        //        }).ToList();


        //        foreach (myOrder m in myord)
        //        {
        //            foreach (orderDetails o in orDetails)
        //            {
        //                if (m.orderId == o.orderId)
        //                {
        //                    m.order.Add(o);
        //                }
        //            }
        //        }

        //        return myord.FirstOrDefault();
        //    }

        //}

        public BookingDetails startOrder(startOrder so)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(con))
                {
                    decimal price = 0;
                    decimal Totalprice = 0;
                    decimal sizePrice = 0;
                    int quantity = 0;

                    Guid g = Guid.NewGuid();
                    string orderId = Convert.ToString(g);
                    string Name = so.CusName;
                    string Address = so.CusAddress;
                    List<orderDetails> list = so.ordDet;

                    itemDetails();

                    foreach (orderDetails o in list)
                    {
                        List<Items> i = dicitems[o.itemId];
                        if (o.size.ToLower() == "s" || o.size.ToLower() == "m" || o.size.ToLower() == "l")
                        {
                           decimal siPrice = dicSize[o.size];
                           sizePrice = siPrice * o.quantity;

                        }
                        else
                        {
                            sizePrice = 0;
                        }
                        Items it = (Items)i[o.itemId - 1];
                        price = it.price;
                        Totalprice = price * o.quantity + sizePrice + Totalprice;
                        quantity = quantity + o.quantity;
                    }
                    sqlcon.Open();
                    string query = string.Format("insert into orderTableNew values ('{0}','{1}','{2}','{3}','{4}')", orderId, Name, Address, Totalprice, quantity);
                    SqlCommand cmd = new SqlCommand(query, sqlcon);
                    cmd.ExecuteNonQuery();

                    foreach (orderDetails o in list)
                    {
                        string query1 = string.Format("insert into orderDetailsNew values ('{0}','{1}','{2}','{3}')", orderId, o.itemId, o.size, o.quantity);
                        SqlCommand cmd1 = new SqlCommand(query1, sqlcon);
                        cmd1.ExecuteNonQuery();
                    }
                    sqlcon.Close();


                    BookingDetails book = new BookingDetails();
                    book.CusName = Name;
                    book.orderId = orderId;
                    book.price = Totalprice;

                    return book;
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}