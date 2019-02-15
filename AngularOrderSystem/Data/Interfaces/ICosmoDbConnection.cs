using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace AngularOrderSystem.Data.Interfaces
{
    public interface ICosmoDbConnection
    {
        Task CreateDatabaseIfNotExist(string dbName);
        Task CreateDocumentCollectionIfNotExist(string colName);
        DocumentClient GetCosmoDbClient();
    }
}
