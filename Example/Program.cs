﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.PassGen;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string lecagy = Console.ReadLine();
            PassGenGenerator gen = new PassGenGenerator();
            PassGenParameters para = new PassGenParameters() { UseLegacyRules = true, LegacyRules = lecagy };
            PassGenMaskOptions mask;
            PassGenUtils.LegacyParser(lecagy, out mask);
            for(uint i = 0; i < 100; i++)
            {
                Console.WriteLine(gen.MaskFromSeed(i, mask));
            }
            PassGenWinUtils win = new PassGenWinUtils();
            win.SaveAllToFile(Console.ReadLine(), mask, false);
            Console.WriteLine("done");
            Console.ReadLine();
        }
         
    }
}
