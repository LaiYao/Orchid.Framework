using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using EtcdNet;

namespace Orchid.Cloud.Configuration
{
    public class EtcdConfigurationProvider : ConfigurationProvider
    {
        #region | Fields |

        private readonly EtcdClientOpitions _clientOptions = null;
        private EtcdClient _client = null;

        private const string ROOT_KEY = "orchidcfg";

        #endregion

        public EtcdConfigurationProvider(EtcdClientOpitions clientOptions)
        {
            _clientOptions = clientOptions;

            _client = new EtcdClient(_clientOptions);
        }

        public override void Set(string key, string value)
        {
            _client.SetNodeAsync(key, value);
        }

        public override bool TryGet(string key, out string value)
        {
            try
            {
                var result = _client.GetNodeValueAsync(key);
                value = result.Result;

                return true;
            }
            catch (Exception)
            {
                value = null;

                return false;
            }
        }

        public override IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            return base.GetChildKeys(earlierKeys, parentPath);
        }

        private string ComposeKey(string key) => $"{ROOT_KEY}/{key}";
    }
}
