using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MongoDB.Driver;
using Orchid.Core.Utilities;
using Orchid.Repo.Abstractions;

namespace Orchid.Repo.Mongo
{
    public class MongoRepositoryContext : RepositoryContext
    {
        #region | Fields |

        string _dbName = null;
        MongoClient _client = null;
        Dictionary<Type, IEnumerable<object>> _tempContainer = new Dictionary<Type, IEnumerable<object>>();
        object _locker = new object();
        bool _isCommited = true;

        internal IMongoDatabase DB = null;

        #endregion

        #region | Ctor |

        public MongoRepositoryContext([NotNull]string dbName, [NotNull]string ip, int port = 27017)
        {
            Check.NotNull(dbName, nameof(dbName));
            Check.NotNull(ip, nameof(ip));

            _client = new MongoClient($"mongodb://{ip}:{port}");
            DB = _client.GetDatabase(dbName);
        }

        #endregion

        public override void Commit()
        {
            //_client.in
        }

        public override void Dispose()
        {
        }

        public override void RegisterDeleted<T>(T value)
        {
            _isCommited = false;

        }

        public override void RegisterModified<T>(T value)
        {
            throw new NotImplementedException();
        }

        public override void RegisterNew<T>(T value)
        {
            throw new NotImplementedException();
        }

        public override void Rollback()
        {
            throw new NotImplementedException();
        }

        #region | Helper |

        IEnumerable<T> GetTempRepository<T>()
        {
            var type = typeof(T);
            if (!_tempContainer.Keys.Contains(type))
            {
                lock (_locker)
                {
                    if (!_tempContainer.Keys.Contains(type))
                    {
                        var tempRepo = new List<object>();
                        _tempContainer.Add(type, tempRepo);
                    }
                }
            }

            return _tempContainer[type].Cast<T>();
        }

        #endregion
    }
}
