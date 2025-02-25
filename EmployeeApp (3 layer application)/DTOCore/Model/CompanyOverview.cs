using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class CompanyOverview
    {
        public CompanyOverview(int companyId, string companyName)
        {
            CompanyId = companyId;
            CompanyName = companyName;
        }

        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

    }
}
