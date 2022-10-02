using System.Collections.Generic;
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
            var resolved = this.PropertyMappings.TryGetValue(propertyName, out var resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}
