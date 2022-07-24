using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Все_что_есть
{
    internal class Class1
    {
        public static string GetMD5Hash(string Text)
        {
            using (MD5 md = MD5.Create())
            {
                byte[] array = md.ComputeHash(Encoding.UTF8.GetBytes(Text));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}
