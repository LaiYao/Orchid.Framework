using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Orchid.Cloud.Service;
using Orchid.Core.Abstractions;

namespace Orchid.Cloud.Service.Client.Restful
{
    [ServiceContract]
    public interface IDefaultRestfulService<TEntity, TKey> where TEntity : IHasKey<TKey>
    {
        IEnumerable<TEntity> Get();

        TEntity Get(TKey id);

        [HttpMethod(HttpMethod.POST)]
        void Post([Body]TEntity entity);

        [HttpMethod(HttpMethod.PUT)]
        void Put(TKey id, [Body]TEntity entity);

        [HttpMethod(HttpMethod.DELETE)]
        void Delete(TKey id);
    }
}