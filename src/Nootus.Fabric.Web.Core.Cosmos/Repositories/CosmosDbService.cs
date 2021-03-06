﻿using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Nootus.Fabric.Web.Core.Cosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Core.Cosmos.Repositories
{
    public class CosmosDbService<TDbContext>
        where TDbContext: CosmosDbContext
    {
        private readonly Uri defaultCollectionUri;
        private readonly string defaultCollectionId;

        public CosmosDbService(TDbContext dbContext)
        {
            DbContext = dbContext;
            defaultCollectionId = DbContext.Settings.CollectionId;
            defaultCollectionUri = UriFactory.CreateDocumentCollectionUri(DbContext.Settings.DatabaseId, DbContext.Settings.CollectionId);
        }


        public TDbContext DbContext { get; private set; }

        private Uri CreateDocumentCollectionUri(string collectionId)
            => UriFactory.CreateDocumentCollectionUri(DbContext.Settings.DatabaseId, collectionId);
        
        private Uri CreateDocumentUri(string collectionId, string id)
            => UriFactory.CreateDocumentUri(DbContext.Settings.DatabaseId, collectionId, id);

        public async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await DbContext.Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DbContext.Settings.DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DbContext.Client.CreateDatabaseAsync(new Database { Id = DbContext.Settings.DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await DbContext.Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DbContext.Settings.DatabaseId, defaultCollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await CreateCollectionAsync();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreateCollectionAsync()
            => await DbContext.Client.CreateDocumentCollectionAsync(
                UriFactory.CreateDatabaseUri(DbContext.Settings.DatabaseId),
                new DocumentCollection { Id = defaultCollectionId },
                new RequestOptions { OfferThroughput = 400 });

        public async Task DeleteDocumentCollectionAsync()
            => await DbContext.Client.DeleteDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DbContext.Settings.DatabaseId, defaultCollectionId));
        

        public async Task<SharedCollectionDocument<TModel>> CreateDocumentAsync<TModel>(string id, string key, TModel model, string documentType)
        {
            SharedCollectionDocument<TModel> document = new SharedCollectionDocument<TModel>()
            {
                Id = id,
                Key = key,
                DocumentType = documentType,
                Model = model
            };

            return await CreateDocumentAsync(document);
        }

        public async Task<SharedCollectionDocument<TModel>> CreateDocumentAsync<TModel>(string key, TModel model, string documentType)
        {
            SharedCollectionDocument<TModel> document = new SharedCollectionDocument<TModel>()
            {
                Id = key == documentType ? key : null,
                Key = key,
                DocumentType = documentType,
                Model = model
            };

            return await CreateDocumentAsync(document);
        }

        public async Task<SharedCollectionDocument<TModel>> CreateDocumentAsync<TModel>(SharedCollectionDocument<TModel> document)
        {
            Document doc = await DbContext.Client.CreateDocumentAsync(CreateDocumentCollectionUri(defaultCollectionId), document);

            document.Id = doc.Id;
            document.SelfLink = doc.SelfLink;

            return document;
        }
                   
        public async Task ReplaceDocumentAsync<TModel>(SharedCollectionDocument<TModel> document)
            => await DbContext.Client.ReplaceDocumentAsync(document.SelfLink, document);

        public async Task ReplaceDocumentAsync<TModel>(string id, SharedCollectionDocument<TModel> document)
            => await DbContext.Client.ReplaceDocumentAsync(CreateDocumentUri(defaultCollectionId, id), document);

        public async Task ReplaceDocumentAsync<TModel>(string key, TModel model, string documentType)
        {
            var document = await GetDocumentByKeyAsyc<TModel>(key, documentType);
            document.Model = model;
            await ReplaceDocumentAsync(document);
        }

        public async Task CreateReplaceDocumentAsync<TModel>(string key, TModel model, string documentType)
        {
            var document = await GetDocumentByKeyAsyc<TModel>(key, documentType);
            if(document == null)
            {
                await CreateDocumentAsync<TModel>(key, model, documentType);
            }
            else
            {
                document.Model = model;
                await ReplaceDocumentAsync(document);
            }
        }


        public async Task<Document> DeleteDocumentAsync(string id) 
            => await DbContext.Client.DeleteDocumentAsync(CreateDocumentUri(defaultCollectionId, id));

        public async Task<Document> DeleteDocumentAsync<TModel>(SharedCollectionDocument<TModel> document)
            => await DbContext.Client.DeleteDocumentAsync(document.SelfLink);

        public async Task<TDocument> SingleOrDefaultAsync<TDocument>(Expression<Func<TDocument, bool>> whereExpression)
            => await Task.FromResult(DbContext.Client.CreateDocumentQuery<TDocument>(defaultCollectionUri).Where(whereExpression).AsEnumerable().SingleOrDefault());

        public async Task<List<TDocument>> ToListAsync<TDocument>(Expression<Func<TDocument, bool>> whereExpression)
            => await Task.FromResult(DbContext.Client.CreateDocumentQuery<TDocument>(defaultCollectionUri).Where(whereExpression).ToList());

        public async Task<SharedCollectionDocument<TModel>> GetDocumentByKeyAsyc<TModel>(string key, string documentType)
            => await SingleOrDefaultAsync<SharedCollectionDocument<TModel>>(w => w.Key == key.ToLower() && w.DocumentType == documentType);

        public async Task<TModel> GetModelByKeyAsyc<TModel>(string key, string documentType)
            where TModel: class
            => (await GetDocumentByKeyAsyc<TModel>(key, documentType))?.Model;

        public async Task<TModel> GetModelByKeyAsyc<TModel>(string key)
            where TModel : class
            => (await GetDocumentByKeyAsyc<TModel>(key, key))?.Model;

        public SharedCollectionDocument<TModel> CopyModel<TModel>(SharedCollectionDocument<TModel> source, 
            SharedCollectionDocument<TModel> destination, TModel model, string key)
        {
            destination.Model = model;
            destination.Key = key;
            destination.DocumentType = source.DocumentType;
            destination.Id = source.Id;
            destination.SelfLink = source.SelfLink;

            return destination;
        }
    }
}
