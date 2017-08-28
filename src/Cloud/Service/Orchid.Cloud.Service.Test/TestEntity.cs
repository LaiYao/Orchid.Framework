using System;
using System.Collections.Generic;
using System.Text;
using Orchid.Core.Abstractions;

namespace Orchid.Cloud.Service.Test
{
    public class TestEntity : IIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
