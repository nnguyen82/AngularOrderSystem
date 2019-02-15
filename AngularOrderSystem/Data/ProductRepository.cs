using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using AngularOrderSystem.Common;
using AngularOrderSystem.Data.Interfaces;
using AngularOrderSystem.Data.Models;

namespace AngularOrderSystem.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICosmoDbConnection _connection;
        public ProductRepository(ICosmoDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> Add(Product item)
        {
            var client = _connection.GetCosmoDbClient();
            await _connection.CreateDatabaseIfNotExist(ConfigurationCd.CosmoDBName);
            await _connection.CreateDocumentCollectionIfNotExist(CollectionCd.Product);

            try
            {
                item.Id = Guid.NewGuid();

                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Product, item.Id.ToString()));

                return item.Id.ToString();
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(ConfigurationCd.CosmoDBName, CollectionCd.Product), item);
                    return item.Id.ToString();
                }
                else
                {
                    throw new Exception(de.ToString());
                }
            }
        }

        public async Task Update(Product item)
        {
            var client = _connection.GetCosmoDbClient();

            try
            {
                //Check if product exist
                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Product, item.Id.ToString()));

                //Update if exist
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Product, item.Id.ToString()), item);
            }
            catch (DocumentClientException de)
            {
                throw new Exception(de.ToString());
            }
        }

        public async Task<bool> Delete(string Id)
        {
            var client = _connection.GetCosmoDbClient();

            try
            {
                //Check if product exist
                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Product, Id));

                //Delete if exist
                await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationCd.CosmoDBName, CollectionCd.Product, Id));

                return true;
            }
            catch (DocumentClientException de)
            {
                throw new Exception(de.ToString());
            }
        }

        public IQueryable<Product> Query(Expression<Func<Product, bool>> filter = null)
        {
            var client = _connection.GetCosmoDbClient();
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Product> query = client.CreateDocumentQuery<Product>(
                    UriFactory.CreateDocumentCollectionUri(ConfigurationCd.CosmoDBName, CollectionCd.Product), queryOptions);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }
}
