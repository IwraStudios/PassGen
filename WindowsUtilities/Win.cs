using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.PassGen;

namespace Gravity.PassGen
{
    public class PassGenWinUtils
    {
        public void SaveAllToFile(string filepath, PassGenMaskOptions maskopt, bool append = false)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, append))
            {
                PassGenGenerator gen = new PassGenGenerator();
                ulong a = gen.MaskPossibility(maskopt);
                for (ulong i = 0; i < a; i++)
                {
                    file.WriteLine(gen.MaskFromSeed(i, maskopt));
                }
            }
        }
    }
}
