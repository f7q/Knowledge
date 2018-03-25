using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EnumItem
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple =false, Inherited = false)]
    public class EnumAttribute : Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdAndName { get; set; }
    }
}
