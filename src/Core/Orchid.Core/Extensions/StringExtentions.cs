using System;
//using System.Security.Cryptography;
using System.Text;

namespace Orchid.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsCaseSensitiveEqual(this string value, string comparing)
        => string.CompareOrdinal(value, comparing) == 0;

        public static bool IsCaseInsensitiveEqual(this string value, string comparing)
        => string.Compare(value, comparing, StringComparison.OrdinalIgnoreCase) == 0;

        public static bool IsEmpty(this string value)
        => string.IsNullOrWhiteSpace(value);

        public static bool HasValue(this string value)
        => !value.IsEmpty();

        //public static string Hash(this string value, Encoding encoding, bool toBase64 = false)
        //{
        //    if (value.IsEmpty())
        //        return value;

        //    using (var md5 = MD5.Create())
        //    {
        //        byte[] data = encoding.GetBytes(value);

        //        if (toBase64)
        //        {
        //            byte[] hash = md5.ComputeHash(data);
        //            return Convert.ToBase64String(hash);
        //        }
        //        else
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            byte[] hashBytes = md5.ComputeHash(data);
        //            foreach (byte b in hashBytes)
        //            {
        //                sb.Append(b.ToString("x2").ToLower());
        //            }

        //            return sb.ToString();
        //        }
        //    }
        //}

        public static string Mask(this string value, int length)
        {
            if (value.HasValue())
                return value.Substring(0, length) + new String('*', value.Length - length);
            return value;
        }
    }
}