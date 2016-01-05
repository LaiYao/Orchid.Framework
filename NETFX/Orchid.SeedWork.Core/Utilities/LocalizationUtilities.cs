using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.CompilerServices;
using Orchid.SeedWork.Core.Contracts;

namespace Orchid.SeedWork.Core
{
    public static class LocalizationUtilities
    {
        public static string GetLocalizedString(string uid)
        {
            return uid;
        }

        public static string GetLocalizedString(string uid, string filePath)
        {
            return uid;
        }

        public static string GetLocalizedString(string uid, string filePath, string CultureName)
        {
            return uid;

            //System.Resources.ResourceReader reader = new System.Resources.ResourceReader(FilePath + filename);

            //string resourcetype;
            //byte[] resourcedata;
            //string result = string.Empty;

            //try
            //{
            //    reader.GetResourceData(Key, out resourcetype, out resourcedata);
            //    //去掉第一个字节，无用
            //    byte[] arr = new byte[resourcedata.Length - 1];
            //    for (int i = 0; i < arr.Length; i++)
            //    {
            //        arr[i] = resourcedata[i + 1];
            //    }
            //    result = System.Text.Encoding.UTF8.GetString(arr);

            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    reader.Close();
            //}

            //return result;
        }
    }
}
