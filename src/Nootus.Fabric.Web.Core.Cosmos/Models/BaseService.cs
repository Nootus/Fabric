using Microsoft.Azure.Documents.Client;
using Nootus.Fabric.Web.Core.Context;
using Nootus.Fabric.Web.Core.Cosmos.Repositories;

namespace Nootus.Fabric.Web.Core.Cosmos.Models
{
    public class BaseService<TDbContext>
        where TDbContext: CosmosDbContext
    {
        protected CosmosDbService<TDbContext> DbService { get; set; }
        protected DocumentClient Client => DbService.DbContext.Client;

        public BaseService()
        {
            DbService = (CosmosDbService<TDbContext> ) NTContext.HttpContext.RequestServices.GetService(typeof(CosmosDbService<TDbContext>));
        }

        public BaseService(CosmosDbService<TDbContext> dbService)
        {
            DbService = dbService;
        }
    }
}
