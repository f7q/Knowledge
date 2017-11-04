﻿using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public short Productid { get; set; }
        public string Productname { get; set; }
        public short? Supplierid { get; set; }
        public short? Categoryid { get; set; }
        public string Quantityperunit { get; set; }
        public float? Unitprice { get; set; }
        public short? Unitsinstock { get; set; }
        public short? Unitsonorder { get; set; }
        public short? Reorderlevel { get; set; }
        public int Discontinued { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual Categories Category { get; set; }
        public virtual Suppliers Supplier { get; set; }
    }
}
