//-------------------------------------------------------------------------------------------------
// <copyright file="JsonIgnoreTimeZone.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Ignoring timezone while searlizing Json
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Helpers
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Nootus.Fabric.Web.Core.Context;

    public class JsonIgnoreTimeZone : IsoDateTimeConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime value = (DateTime)base.ReadJson(reader, objectType, existingValue, serializer);
            if (NTContext.HttpContext.Request.Headers.ContainsKey("BrowserTimezoneOffset"))
            {
                int browserTimezoneOffset = Convert.ToInt32(NTContext.HttpContext.Request.Headers["BrowserTimezoneOffset"]);

                // converting the value to UTC and subtracting the browser offset to get the browser local time
                value = TimeZoneInfo.ConvertTimeToUtc(value).AddMinutes(-browserTimezoneOffset);
            }

            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            this.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
            base.WriteJson(writer, value, serializer);
        }
    }
}
