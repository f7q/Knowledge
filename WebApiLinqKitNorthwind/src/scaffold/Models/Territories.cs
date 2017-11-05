using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class Territories
    {
        public Territories()
        {
            Employeeterritories = new HashSet<Employeeterritories>();
        }

        public string Territoryid { get; set; }
        public string Territorydescription { get; set; }
        public short Regionid { get; set; }

        public virtual ICollection<Employeeterritories> Employeeterritories { get; set; }
        public virtual Region Region { get; set; }
    }
}
