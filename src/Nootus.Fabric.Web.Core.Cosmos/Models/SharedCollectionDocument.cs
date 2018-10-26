using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Nootus.Fabric.Web.Core.Cosmos.Models
{
    public class SharedCollectionDocument<TDocument> //: Resource
    {
        private string key;

        public string DocumentType { get; set; }
        public string Key { get => key.ToLower(); set => key = value; }
        public TDocument Document { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_self")]
        public string SelfLink { get; set; }
    }
}
