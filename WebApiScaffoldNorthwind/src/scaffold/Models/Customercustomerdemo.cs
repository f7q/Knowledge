using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class Customercustomerdemo
    {
        public string Customerid { get; set; }
        public string Customertypeid { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Customerdemographics Customertype { get; set; }
    }
}
