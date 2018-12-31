using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Security.Core.Device;
using Nootus.Fabric.Web.Security.Cosmos.Middleware;
using Nootus.Fabric.Web.Security.Cosmos.Models;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Device
{
    public class AndroidService: BaseService<SecurityDbContext>
    {
        public async Task SignatureHashSave(string signatureHash)
        {
            AndroidSettings settings = new AndroidSettings() { SignatureHash = signatureHash };
            string key = SecurityAppSettings.ServiceSettings.DocumentTypes.AndroidSettings;
            await DbService.CreateReplaceDocumentAsync(key, settings, key);
            CacheService.AndroidSignatureHash = signatureHash;
        }

        public async Task<string> SignatureHashGet()
        {
            AndroidSettings settings = await DbService.GetModelByKeyAsyc<AndroidSettings>(SecurityAppSettings.ServiceSettings.DocumentTypes.AndroidSettings);
            return settings?.SignatureHash;
        }
    }
}
