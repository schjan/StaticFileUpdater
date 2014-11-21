using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StaticFileUpdater.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PatchFiles
    {
        [JsonProperty]
        public IList<string> Updated { get; set; }

        [JsonProperty]
        public IList<string> Deleted { get; set; }

        [JsonProperty]
        public IList<string> Added { get; set; }

        public PatchFiles()
        {
            Updated = new List<string>();
            Deleted = new List<string>();
            Added = new List<string>();
        }
    }
}
