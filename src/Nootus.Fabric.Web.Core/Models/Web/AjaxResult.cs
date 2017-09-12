//-------------------------------------------------------------------------------------------------
// <copyright file="AjaxResult.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This enum contains the request status. Based on the status, Angular will show message to the
//  user
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Web
{
    public enum AjaxResult
    {
        Success = 0,
        Exception = 1,
        ValidationException = 2
    }
}
