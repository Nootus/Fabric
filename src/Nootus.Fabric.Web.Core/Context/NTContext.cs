//-------------------------------------------------------------------------------------------------
// <copyright file="NTContext.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This stores the HttpContext and NootusContext in the async call context so that they are
//  available through out the execution cycle
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Context
{
    using System.Threading;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;

    public static class NTContext
    {
        private static AsyncLocal<NTContextModel> asyncContext = new AsyncLocal<NTContextModel>();
        private static AsyncLocal<HttpContext> asyncHttpContext = new AsyncLocal<HttpContext>();

        public static NTContextModel Context
        {
            get
            {
                return asyncContext.Value;
            }

            set
            {
                NTContextModel model = value;
                if (model == null)
                {
                    model = new NTContextModel();
                }

                NTContextModel contextModel = Context;

                if (contextModel == null)
                {
                    asyncContext.Value = model;
                }
                else
                {
                    // assigning to itself for overriding the values
                    contextModel = Mapper.Map<NTContextModel, NTContextModel>(model, contextModel);
                }
            }
        }

        public static HttpContext HttpContext
        {
            get
            {
                return asyncHttpContext.Value;
            }

            set
            {
                asyncHttpContext.Value = value;
            }
        }
    }
}
