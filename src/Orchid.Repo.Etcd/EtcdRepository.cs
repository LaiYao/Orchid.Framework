using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orchid.Repo.Abstractions;
using EtcdNet;
using Orchid.Core.Abstractions;
using Orchid.Core.Utilities;
using Newtonsoft.Json;
using System.Linq;

namespace Orchid.Repo.Etcd
{
    public class EtcdRepository<T> : IRepository<T>, IDisposable where T : IHasKey<string>, new()
    {
        #region | Field |

        private string _rootPath;
        private EtcdClient _client;

        #endregion

        #region | Ctor |

        public EtcdRepository(string rootPath, EtcdRepositoryOptions options)
        {
            Check.NotEmpty(rootPath, nameof(rootPath));
            Check.NotNull(options, nameof(options));

            _rootPath = rootPath;
            _client = new EtcdClient(new EtcdClientOpitions
            {
                IgnoreCertificateError = options.IgnoreCertificateError,
                JsonDeserializer = new DefaultJsonDeserializer(),
                Password = options.Password,
                Urls = options.Urls,
                UseProxy = options.UseProxy,
                Username = options.Username,
                X509Certificate = options.X509Certificate
            });
        }

        #endregion

        #region | IRepository |

        public IEnumerable<T> AllItems => throw new NotImplementedException();

        public void Add(T value)
        {
            var resultTask = _client.CreateNodeAsync($"{_rootPath}/{value.Id}", JsonConvert.SerializeObject(value));
            if (resultTask.Exception != null)
            {
                throw resultTask.Exception;
            }
        }

        public void Remove(T value)
        {
            var resultTask = _client.DeleteNodeAsync($"{_rootPath}/{value.Id}");
            if (resultTask.Exception != null)
            {
                throw resultTask.Exception;
            }
        }

        public void Update(T value)
        {
            var resultTask = _client.SetNodeAsync($"{_rootPath}/{value.Id}", JsonConvert.SerializeObject(value));
            if (resultTask.Exception != null)
            {
                throw resultTask.Exception;
            }
        }

        public bool Any(Func<T, bool> cretiria)
        {
            var allEntities = FindAll();
            return allEntities.Any(cretiria);
        }

        public async Task<bool> AnyAsync(Func<T, bool> cretiria)
        {
            var allEntities = await FindAllAsync();
            return allEntities.Any(cretiria);
        }

        public IEnumerable<T> Find(Func<T, bool> cretiria)
        {
            throw new NotImplementedException();
        }

        public IPagingResult<T> Find<TOrderKey>(Func<T, bool> cretiria, Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll()
        {
            var resultTask = _client.GetNodeAsync(_rootPath);
            return resultTask.Result.Node.Nodes.Select(_ => JsonConvert.DeserializeObject<T>(_.Value));
        }

        public IPagingResult<T> FindAll<TOrderKey>(Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var resultTask = _client.GetNodeAsync(_rootPath, false, true);
            resultTask.Wait();
            var allEntities = new List<T>();
            foreach (var serviceNode in resultTask.Result.Node.Nodes)
            {
                var entry = JsonConvert.DeserializeObject<T>(serviceNode.Value);
                allEntities.Add(entry);
            }

            var pagingItems = allEntities.OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);
            return new PagingResult<T>(pagingItems, allEntities.Count, (int)Math.Ceiling((decimal)allEntities.Count / countPerPage));
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            var resultTask = await _client.GetNodeAsync(_rootPath);
            return resultTask.Node.Nodes.Select(_ => JsonConvert.DeserializeObject<T>(_.Value));
        }

        public async Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var resultTask = await _client.GetNodeAsync(_rootPath, false, true);
            var allEntities = new List<T>();
            foreach (var serviceNode in resultTask.Node.Nodes)
            {
                var entry = JsonConvert.DeserializeObject<T>(serviceNode.Value);
                allEntities.Add(entry);
            }

            var pagingItems = allEntities.OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);
            return new PagingResult<T>(pagingItems, allEntities.Count, (int)Math.Ceiling((decimal)allEntities.Count / countPerPage));
        }

        public async Task<IEnumerable<T>> FindAsync(Func<T, bool> cretiria)
        {
            var all = await FindAllAsync();
            return all.Where(cretiria);
        }

        public async Task<IPagingResult<T>> FindAsync<TOrderKey>(Func<T, bool> cretiria, Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var resultTask = await _client.GetNodeAsync(_rootPath, false, true);
            var allEntities = new List<T>();
            foreach (var serviceNode in resultTask.Node.Nodes)
            {
                var entry = JsonConvert.DeserializeObject<T>(serviceNode.Value);
                allEntities.Add(entry);
            }

            var pagingItems = allEntities.Where(cretiria).OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);
            return new PagingResult<T>(pagingItems, allEntities.Count, (int)Math.Ceiling((decimal)allEntities.Count / countPerPage));
        }

        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
