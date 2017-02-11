using System.Collections.Generic;

namespace ef.Models
{
    public class Company 
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Capital { get; set; }
        public List<Employee> Employees { get; set; }
    }
}