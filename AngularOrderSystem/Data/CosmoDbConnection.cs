using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using AngularOrderSystem.BL.Interfaces;
using AngularOrderSystem.Data.Interfaces;
using AngularOrderSystem.Common;

namespace AngularOrderSystem.Data
{
    public class CosmoDbConnection : ICosmoDbConnection
    {
        private DocumentClient _documentClient;
        public CosmoDbConnection(IAppConfiguration appConfiguration)
        {
            _documentClient = new DocumentClient(new Uri(appConfiguration.GetEndpointUrl()), appConfiguration.GetPrimaryKey());
        }

        public async Task CreateDatabaseIfNotExist(string dbName)
        {
            var client = GetCosmoDbClient();

            try
            {
                await client.CreateDatabaseIfNotExistsAsync(new Database { Id = dbName });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task CreateDocumentCollectionIfNotExist(string colName)
        {
            var client = GetCosmoDbClient();

            try
            {
                await _documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(ConfigurationCd.CosmoDBName), new DocumentCollection { Id = colName });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public DocumentClient GetCosmoDbClient()
        {
            return _documentClient;
        }
    }
}
