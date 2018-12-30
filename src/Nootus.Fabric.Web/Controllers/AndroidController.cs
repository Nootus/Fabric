using Microsoft.AspNetCore.Mvc;
using Nootus.Fabric.Web.Core.Helpers.Web;
using Nootus.Fabric.Web.Core.Models.Web;
using Nootus.Fabric.Web.Security.Core.Common;
using Nootus.Fabric.Web.Security.Cosmos.Device;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AndroidController: ControllerBase
    {
        private AndroidService service;

        public AndroidController(AndroidService service)
            => this.service = service;

        public async Task<AjaxModel<NTModel>> SignatureHashSave(string hash)
            => await AjaxHelper.SaveAsync(m => this.service.SignatureHashSave(hash), SecurityMessages.SignatureHashSuccess);
    }
}
