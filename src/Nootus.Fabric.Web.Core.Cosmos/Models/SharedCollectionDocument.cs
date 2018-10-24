using Microsoft.Azure.Documents;
namespace Nootus.Fabric.Web.Core.Cosmos.Models
{
    public class SharedCollectionDocument<TDocument> : Document
    {
        public string DocumentType { get; set; }
        public string Key { get; set; }
        public TDocument Document { get; set; }
    }
}
