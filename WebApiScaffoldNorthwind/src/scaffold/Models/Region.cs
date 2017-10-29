using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class Region
    {
        public Region()
        {
            Territories = new HashSet<Territories>();
        }

        public short Regionid { get; set; }
        public string Regiondescription { get; set; }

        public virtual ICollection<Territories> Territories { get; set; }
    }
}
