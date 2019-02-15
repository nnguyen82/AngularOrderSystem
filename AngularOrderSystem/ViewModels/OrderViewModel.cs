using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularOrderSystem.Data.Models;

namespace AngularOrderSystem.ViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public DateTime OrderDate { get; set; }

        public Product[] Products { get; set; }
    }
}
