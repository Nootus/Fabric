//-------------------------------------------------------------------------------------------------
// <copyright file="NTContextModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This contains the data that is cached in the call context
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Context
{
    public class NTContextModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public int CompanyId { get; set; }

        public int GroupCompanyId { get; set; }

        public int? DashboardPageId { get; set; }
    }
}
