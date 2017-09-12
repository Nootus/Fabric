//-------------------------------------------------------------------------------------------------
// <copyright file="CompanyModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  For CompanyDetails
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Models
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public bool GroupInd { get; set; }

        public int? ParentCompanyId { get; set; }

        public override bool Equals(object obj)
        {
            return this.CompanyId.Equals(((CompanyModel)obj).CompanyId);
        }

        public override int GetHashCode()
        {
            return this.CompanyId.GetHashCode();
        }
    }
}
