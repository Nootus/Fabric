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
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using System.Threading;

    public static class NTContext
    {
        private static AsyncLocal<NTContextModel> AsyncContext = new AsyncLocal<NTContextModel>();
        private static AsyncLocal<HttpContext> AsyncHttpContext = new AsyncLocal<HttpContext>();
        public static NTContextModel Context
        {
            get
            {
                return AsyncContext.Value;
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
                    AsyncContext.Value = model;
                }
                else
                {
                    contextModel = Mapper.Map<NTContextModel, NTContextModel>(model, contextModel);
                }
            }
        }

        public static HttpContext HttpContext
        {
            get
            {
                return AsyncHttpContext.Value;
            }

            set
            {
                AsyncHttpContext.Value = value;
            }
        }
    }
}
