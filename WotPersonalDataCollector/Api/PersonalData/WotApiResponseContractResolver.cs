﻿using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace WotPersonalDataCollector.Api.PersonalData
{
    public class WotApiResponseContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMappings { get; set; }

        public WotApiResponseContractResolver(string userId)
        {
            PropertyMappings = new Dictionary<string, string>
            {
                {"WotUser", userId},
            };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            string resolvedName = null;
            var resolved = this.PropertyMappings.TryGetValue(propertyName, out resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}
