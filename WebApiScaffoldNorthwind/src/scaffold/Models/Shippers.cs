using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class Shippers
    {
        public Shippers()
        {
            Orders = new HashSet<Orders>();
        }

        public short Shipperid { get; set; }
        public string Companyname { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
