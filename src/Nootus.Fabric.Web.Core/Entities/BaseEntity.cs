//-------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This is the base class for all database related entities. Also used in setting audit values
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Entities
{
    using System;
    using Nootus.Fabric.Web.Core.Context;

    public class BaseEntity
    {
        public BaseEntity()
        {
            NTContextModel contextModel = NTContext.Context;

            if (contextModel != null)
            {
                this.CreatedUserId = contextModel.UserName;
                this.LastModifiedUserId = contextModel.UserName;
                this.CompanyId = contextModel.CompanyId;
            }

            this.CreatedDate = DateTime.UtcNow;
            this.LastModifiedDate = DateTime.UtcNow;
            this.DeletedInd = false;
        }

        public string CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedUserId { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public bool DeletedInd { get; set; }

        public int CompanyId { get; set; }

        public virtual void UpdateAuditFields()
        {
            NTContextModel contextModel = NTContext.Context;

            this.LastModifiedDate = DateTime.UtcNow;
            this.LastModifiedUserId = contextModel.UserName;
        }
    }
}
