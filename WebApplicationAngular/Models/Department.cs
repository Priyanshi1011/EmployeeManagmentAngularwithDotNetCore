using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAngular.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        [Required] public string DepartmentName { get; set; }
    }
}
