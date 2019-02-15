using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularOrderSystem.Data.Models;

namespace AngularOrderSystem.Data.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
