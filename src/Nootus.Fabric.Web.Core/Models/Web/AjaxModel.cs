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
    using Nootus.Fabric.Web.Core.Models.Widget;

    public class AjaxModel<T>
    {
        public AjaxResult Result { get; set; }

        public string Message { get; set; }

        public T Model { get; set; }

        public DashboardModel Dashboard { get; set; }
    }
}
