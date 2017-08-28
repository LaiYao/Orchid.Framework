using System;
using System.Collections.Generic;
using System.Text;
using Orchid.Cloud.Service.Client.Restful;

namespace Orchid.Cloud.Service.Test
{
    public interface TestRestfulService : IDefaultRestfulService<TestEntity, int>
    {
    }
}
