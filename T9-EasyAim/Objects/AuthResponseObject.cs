using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace T9_EasyAim.Objects
{
    public class AuthResponseObject
    {
        [JsonProperty]
        public string ResponseStatus { get; set; }
        public string CheatRemainigTime { get; set; }
        
    }
}
