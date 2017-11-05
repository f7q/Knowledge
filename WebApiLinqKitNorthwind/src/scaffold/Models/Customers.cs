using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Customercustomerdemo = new HashSet<Customercustomerdemo>();
            Orders = new HashSet<Orders>();
        }

        public string Customerid { get; set; }
        public string Companyname { get; set; }
        public string Contactname { get; set; }
        public string Contacttitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Customercustomerdemo> Customercustomerdemo { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
