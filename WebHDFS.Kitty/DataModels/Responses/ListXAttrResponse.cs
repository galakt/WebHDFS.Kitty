using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WebHDFS.Kitty.DataModels.Responses
{
    public class ListXAttrResponse
    {
        [JsonConstructor]
        ListXAttrResponse(string xattrnames)
        {
            Names = xattrnames;
        }
        public string Names { get; }
    }
}
