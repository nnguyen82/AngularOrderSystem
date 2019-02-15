using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularOrderSystem.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Descriptions { get; set; }

        public double Price { get; set; }
    }
}
