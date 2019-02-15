using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AngularOrderSystem.BL.Interfaces;
using AngularOrderSystem.Common;

namespace AngularOrderSystem.BL
{
    public class AppSettingConfiguration : IAppConfiguration
    {
        private string _endPointUrl;
        private string _primaryKey;
        public AppSettingConfiguration(IConfiguration configuration)
        {
            foreach (IConfigurationSection section in configuration.GetSection(ConfigurationSectionCd.AppSetting).GetChildren())
            {
                if (section.Key == ConfigurationCd.EndpointUrl)
                {
                    _endPointUrl = section.Value;
                }

                if (section.Key == ConfigurationCd.PrimaryKey)
                {
                    _primaryKey = section.Value;
                }
            }
        }

        public string GetEndpointUrl()
        {
            return _endPointUrl;
        }

        public string GetPrimaryKey()
        {
            return _primaryKey;
        }
    }
}
