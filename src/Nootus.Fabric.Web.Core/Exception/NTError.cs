//-------------------------------------------------------------------------------------------------
// <copyright file="NTError.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Used to pass error to the Angular
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Exception
{
    public class NTError
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }
}
