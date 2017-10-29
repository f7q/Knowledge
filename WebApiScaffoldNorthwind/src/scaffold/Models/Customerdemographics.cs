using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class Customerdemographics
    {
        public Customerdemographics()
        {
            Customercustomerdemo = new HashSet<Customercustomerdemo>();
        }

        public string Customertypeid { get; set; }
        public string Customerdesc { get; set; }

        public virtual ICollection<Customercustomerdemo> Customercustomerdemo { get; set; }
    }
}
