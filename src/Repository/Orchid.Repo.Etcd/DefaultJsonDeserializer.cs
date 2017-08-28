using EtcdNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Repo.Etcd
{
    public class DefaultJsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
