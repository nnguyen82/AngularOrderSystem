using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using AngularOrderSystem.Common;
using AngularOrderSystem.Data.Interfaces;
using AngularOrderSystem.Data.Models;

namespace AngularOrderSystem.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ICosmoDbConnection _connection;
        public OrderRepository(ICosmoDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> Add(Order item)
        {
            var client = _connection.GetCosmoDbClient();
            await _connection.CreateDatabaseIfNotExist(ConfigurationCd.CosmoDBName);
            await _connection.CreateDocumentCollectionIfNotExist(CollectionCd.Order);

            try
            {
                item.Id = Guid.NewGuid();

                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Order, item.Id.ToString()));

                return item.Id.ToString();
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(ConfigurationCd.CosmoDBName, CollectionCd.Order), item);
                    return item.Id.ToString();
                }
                else
                {
                    throw new Exception(de.ToString());
                }
            }
        }

        public async Task<bool> Delete(string Id)
        {
            var client = _connection.GetCosmoDbClient();

            try
            {
                //Check if order exist
                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Order, Id));

                //Delete if exist
                await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Order, Id));

                return true;
            }
            catch (DocumentClientException de)
            {
                throw new Exception(de.ToString());
            }
        }

        public IQueryable<Order> Query(Expression<Func<Order, bool>> filter = null)
        {
            var client = _connection.GetCosmoDbClient();
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Order> query = client.CreateDocumentQuery<Order>(
                    UriFactory.CreateDocumentCollectionUri(ConfigurationCd.CosmoDBName, CollectionCd.Order), queryOptions);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public async Task Update(Order item)
        {
            var client = _connection.GetCosmoDbClient();

            try
            {
                //Check if order exist
                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Order, item.Id.ToString()));

                //Update if exist
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Order, item.Id.ToString()), item);
            }
            catch (DocumentClientException de)
            {
                throw new Exception(de.ToString());
            }
        }
    }
}
