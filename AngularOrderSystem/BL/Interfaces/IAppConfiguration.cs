using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularOrderSystem.BL.Interfaces
{
    public interface IAppConfiguration
    {
        string GetEndpointUrl();
        string GetPrimaryKey();
    }
}
