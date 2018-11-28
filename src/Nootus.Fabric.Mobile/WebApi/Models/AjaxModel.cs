using Nootus.Fabric.Mobile.Exception;
using System.Collections.Generic;

namespace Nootus.Fabric.Mobile.WebApi.Models
{
    public class AjaxModel<T>
    {
        public AjaxResult Result { get; set; }
        public string Message { get; set; }
        public List<NTError> Errors { get; set; }
        public T Model { get; set; }
    }
}
