using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularOrderSystem.Common
{
    public struct ConfigurationCd
    {
        public const string EndpointUrl = "EndpointUrl";
        public const string PrimaryKey = "PrimaryKey";
        public const string CosmoDBName = "OrderProcessingDB";
    }

    public struct ConfigurationSectionCd
    {
        public const string AppSetting = "AppSetting";
    }

    public struct CollectionCd
    {
        public const string Product = "Products";
        public const string Order = "Orders";
    }
}
