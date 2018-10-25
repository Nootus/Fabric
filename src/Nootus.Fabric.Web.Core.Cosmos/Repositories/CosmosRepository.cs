using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Core.Cosmos.Repositories
{
    public class CosmosRepository<TDbContext>
        where TDbContext: CosmosDbContext
    {

        public TDbContext DbContext { get; protected set; }
        protected string DefaultCollectionId { get; set; }

        protected Uri DefaultCollectionUri { get; set; }

        public Uri CreateDocumentCollectionUri(string collectionId)
            => UriFactory.CreateDocumentCollectionUri(DbContext.Settings.DatabaseId, collectionId);
        

        public Uri CreateDocumentUri(string collectionId, string id)
            =>UriFactory.CreateDocumentUri(DbContext.Settings.DatabaseId, collectionId, id);
        
        

        public async Task<Document> CreateDocumentAsync(object document)
            => await DbContext.Client.CreateDocumentAsync(DefaultCollectionUri, document);
        

        public async Task<Document> UpdateItemAsync(Document document)
            => await DbContext.Client.ReplaceDocumentAsync(document);
   

        public async Task<Document> DeleteItemAsync(string id) 
            => await DbContext.Client.DeleteDocumentAsync(CreateDocumentUri(DefaultCollectionId, id));
    }
}
