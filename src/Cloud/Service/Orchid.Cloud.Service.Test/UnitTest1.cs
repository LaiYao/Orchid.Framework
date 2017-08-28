using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orchid.Cloud.Service.Client;
using Orchid.Cloud.Service.Client.Restful;

namespace Orchid.Cloud.Service.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var client = ProxyFactory.CreateProxy<ITestService>(new RestfulClient(new RestfulClientOptions { }))
                .Config(_ => _.TestMethod(default(int), default(int)), new DefaultInvocationOptions { FailCallback = (_, __) => { Console.WriteLine(__.Message); return null; } })
                .Build();

            // Action
            var result = client.Object.TestMethod(2, 3);

            // Assert
            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void TestRestfuleClient()
        {
            // Arrange
            var client = ProxyFactory.CreateProxy<TestRestfulService>(new RestfulClient(new RestfulClientOptions { }))
                .Build();

            // Action
            client.Object.Put(1, new TestEntity { Name = "Leo", Age = 18 });

            // Assert
            //Assert.AreEqual(result, 5);
        }
    }

    [ServiceContract]
    public interface ITestService
    {
        void TestMethod(int a);
        int TestMethod(int a, int b);
    }
}
