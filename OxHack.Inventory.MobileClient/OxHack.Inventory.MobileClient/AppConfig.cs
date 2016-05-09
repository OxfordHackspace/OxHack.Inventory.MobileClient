using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.MobileClient
{
    public class AppConfig
    {
        [JsonConstructor]
        private AppConfig(Uri apiUri)
        {
            this.ApiUri = apiUri;
        }

        internal static AppConfig CreateFromConfigFile()
        {
            var stream = typeof(AppConfig).GetTypeInfo().Assembly.GetManifestResourceStream(typeof(AppConfig).Namespace + ".appConfig.json");

            string json;
            using (var reader = new System.IO.StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var config = JsonConvert.DeserializeObject<AppConfig>(json);

            return config;
        }

        public Uri ApiUri
        {
            get;
        }
    }
}
