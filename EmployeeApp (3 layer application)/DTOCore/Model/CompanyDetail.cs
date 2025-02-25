using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class CompanyDetail
    {
        public string CompanyName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
