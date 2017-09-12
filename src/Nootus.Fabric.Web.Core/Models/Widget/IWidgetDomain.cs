//-------------------------------------------------------------------------------------------------
// <copyright file="IWidgetDomain.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Interface for all Widget related domains, such as Quarry
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Widget
{
    using System.Threading.Tasks;

    public interface IWidgetDomain
    {
        Task<DashboardModel> DashboardGet(int pageId);
    }
}
