using System;
using Orchid.SeedWork.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orchid.SeedWork.Core.UnitTest
{
    [TestClass]
    public class EncryptUtilitiesTest
    {
        [TestMethod]
        public void TestEncrypt()
        {
            // Arrange
            var dataToEncrypt = "12346789";
            var securityStamp = DateTime.Now.Ticks.ToString();
            var encryptUtilities = new EncryptUtilities(securityStamp);

            // Action
            var encryptedData = encryptUtilities.Encrypt(dataToEncrypt);
            var decryptedData = encryptUtilities.Decrypt(encryptedData);

            // Asset
            Assert.AreEqual(dataToEncrypt, decryptedData);
        }
    }
}
