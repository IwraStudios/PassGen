using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity;
using System.Reflection;
using System.IO;

namespace Gravity.PassGen
{
    public class PassGenUtils
    {
        public string GenRandomPass(string Mask, int length1)
        {
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length1--)
            {
                char c = Mask[rnd.Next(0, Mask.Length)];
                //if ("+-&|!(){}[]^\"~*?:\\".Contains(c)) res.Append("\\\\");
                res.Append(c);
            }
            return res.ToString();
        }

        public string GenTrueRandomPass(string Mask, int length1) //using true random lib
        {
          using Gravity.TrueRandom{
            
          }
            return null;
        }
    }
    public class PassGenConst
    {
        public const string Mask_aZ09 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const string Mask_aZ09_SPcompatible = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=";
        public const string Mask_aZ09_SPall = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#$%&'()*+,-./:;<=>?@[]^_`{|}~";
    }
}
