//-------------------------------------------------------------------------------------------------
// <copyright file="AjaxModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  DTO object to transfer data between WebAPI and Angular.
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Web
{
    using System.Collections.Generic;
    using Nootus.Fabric.Web.Core.Exception;
    using Nootus.Fabric.Web.Core.Models.Widget;

    public class AjaxModel<T>
    {
        public AjaxResult Result { get; set; }

        public string Message { get; set; }

        public List<NTError> Errors { get; set; }

        public T Model { get; set; }

        public DashboardModel Dashboard { get; set; }
    }
}
