﻿using System;
using System.Collections.Generic;

namespace WebApiScaffoldNorthwind.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Employeeterritories = new HashSet<Employeeterritories>();
            Orders = new HashSet<Orders>();
        }

        public short Employeeid { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Title { get; set; }
        public string Titleofcourtesy { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? Hiredate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Homephone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public short? Reportsto { get; set; }
        public string Photopath { get; set; }

        public virtual ICollection<Employeeterritories> Employeeterritories { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual Employees ReportstoNavigation { get; set; }
        public virtual ICollection<Employees> InverseReportstoNavigation { get; set; }
    }
}
