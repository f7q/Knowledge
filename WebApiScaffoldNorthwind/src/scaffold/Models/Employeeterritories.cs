using System;
using System.Collections.Generic;

namespace scaffold.Models
{
    public partial class Employeeterritories
    {
        public short Employeeid { get; set; }
        public string Territoryid { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Territories Territory { get; set; }
    }
}
