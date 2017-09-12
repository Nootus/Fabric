//-------------------------------------------------------------------------------------------------
// <copyright file="CoreMappingProfile.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Specifying the AutoMapepr Mapping details
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Mapping
{
    using AutoMapper;
    using Nootus.Fabric.Web.Core.Context;

    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            this.CreateMap<NTContextModel, NTContextModel>();
        }

        public override string ProfileName
        {
            get
            {
                return "CoreMappingProfile";
            }
        }
    }
}
