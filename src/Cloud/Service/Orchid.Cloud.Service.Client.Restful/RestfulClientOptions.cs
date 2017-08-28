using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Orchid.Cloud.Service.Client;

namespace Orchid.Cloud.Service.Client.Restful
{
    public class RestfulClientOptions
    {
        public IEnumerable<Uri> Endpoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Format { get; set; }

        public string Token { get; set; }
    }
}