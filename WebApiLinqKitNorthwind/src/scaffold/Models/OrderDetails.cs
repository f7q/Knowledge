using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class OrderDetails
    {
        public short Orderid { get; set; }
        public short Productid { get; set; }
        public float Unitprice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
