using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Orchid.SeedWork.Core
{
    public class EncryptUtilities
    {
        #region | Fields |

        ICryptoTransform _encryptor;

        ICryptoTransform _decryptor;

        const int _buffSize = 256;

        #endregion

        #region | Ctor |

        public EncryptUtilities(string key, string algName = "TripleDES")
        {
            var provider = SymmetricAlgorithm.Create(algName);
            var minKeySize = provider.LegalKeySizes.Min(_ => _.MinSize) / 8;
            var maxKeySize = provider.LegalKeySizes.Max(_ => _.MaxSize) / 8;

            if (key.Length < maxKeySize)
            {
                key = key.PadLeft(maxKeySize, '_');
            }

            provider.Key = Encoding.UTF8.GetBytes(key);
            provider.IV = new byte[] { 0x1F, 0x2E, 0x3D, 0x4C, 0x5B, 0x6A, 0x70, 0x89 };

            _encryptor = provider.CreateEncryptor();
            _decryptor = provider.CreateEncryptor();
        }

        #endregion

        #region | Methods |

        public string Encrypt(string dataToEncrypt)
        {
            var dataToEncryptBuff = Encoding.UTF8.GetBytes(dataToEncrypt);
            var dataToEncryptStream = new MemoryStream(dataToEncryptBuff);

            var encryptedStream = new MemoryStream();
            var cryptStream = new CryptoStream(encryptedStream, _encryptor, CryptoStreamMode.Write);

            var buff = new byte[_buffSize];
            var readLength = 0;
            do
            {
                readLength = dataToEncryptStream.Read(buff, 0, _buffSize);
                cryptStream.Write(buff, 0, readLength);

            } while (readLength > 0);

            cryptStream.FlushFinalBlock();

            return Convert.ToBase64String(encryptedStream.ToArray());
        }

        public string Decrypt(string dataToDecrypt)
        {
            var dataToDecryptBuff = Convert.FromBase64String(dataToDecrypt);
            var dataToDecryptStream = new MemoryStream(dataToDecryptBuff);

            var decryptedStream = new MemoryStream();
            var cryptStream = new CryptoStream(dataToDecryptStream, _decryptor, CryptoStreamMode.Read);

            var buff = new byte[_buffSize];
            var readLength = 0;
            do
            {
                readLength = cryptStream.Read(buff, 0, _buffSize);
                decryptedStream.Write(buff, 0, readLength);

            } while (readLength > 0);

            return Encoding.UTF8.GetString(decryptedStream.GetBuffer());
        }

        #endregion
    }
}
