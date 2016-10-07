using System.Text;
using System;
using System.Collections.Generic;

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

        /*public string GenTrueRandomPass(string Mask, int length1) //TODO: add plugin to PassGen
        {
            return null;
        } */
        public void LegacyParser(PassGenParameters parameters, out PassGenMaskOptions maskop)
        {
            maskop = new PassGenMaskOptions();
            maskop.Init();
            List<char[]> chars = new List<char[]>();
            for (int i = 0; i > parameters.LegacyRules.Length; i = i + 2)
            {
                chars.Add(parameters.LegacyRules.Substring(i, 2).ToCharArray());
            }
            bool Record = false;
            foreach (char[] ch in chars)
            {
                if (Record && ch[0] != '}')
                {
                    maskop.Mask.Add(ch[1].ToString().ToLower() + ch[1].ToString().ToUpper());
                    continue;
                }
                switch (ch[0])
                {
                    case '?':
                        switch (ch[1])
                        {
                            case 'l': //lowercase
                                maskop.Mask.Add(maskop.DefaultMasks[0]); //abcdefghijklmnopqrstuvwxyz
                                break;
                            case 'u': //uppercase
                                maskop.Mask.Add(maskop.DefaultMasks[1]); //ABCDEFGHIJKLMNOPQRSTUVWXYZ
                                break;
                            case 'd': //decimal
                                maskop.Mask.Add(maskop.DefaultMasks[2]); //1234567890
                                break;
                            case 'c'://any Case
                                maskop.Mask.Add(maskop.DefaultMasks[0] + maskop.DefaultMasks[1]);
                                break;
                            case 't'://Three most common masks
                                maskop.Mask.Add(maskop.DefaultMasks[0] + maskop.DefaultMasks[1] + maskop.DefaultMasks[2]);
                                break;
                            case 's': //special
                                maskop.Mask.Add(maskop.DefaultMasks[3]); // !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~
                                break;
                            case 'a': //all
                                maskop.Mask.Add(maskop.DefaultMasks[0] + maskop.DefaultMasks[1] + maskop.DefaultMasks[2] + maskop.DefaultMasks[3]); //abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~
                                break;

                        }
                        break;
                    case '{':
                        Record = true;
                        break;
                    case '}':
                        Record = false;
                        break;
                }
            }
        }
    }

    public class PassGenGenerator
    {
        public string MaskString(string s, PassGenParameters parameters)
        {
            PassGenMaskOptions maskop;
            if (parameters.UseLegacyRules)
            {
               new PassGenUtils().LegacyParser(parameters, out maskop);
            }
            else
            {
                maskop = new PassGenMaskOptions();
                maskop.Init();
                maskop.CustomMasks = new string[1];
                maskop.CustomMasks[0] = parameters.changeMask;
                for (int i = 0; i > parameters.additiveAmount; i++){ maskop.Mask.Add(parameters.additiveMask); }
                foreach (char c in s.ToCharArray())
                {
                    maskop.Mask.Add(maskop.CustomMasks[0]);
                }
                for (int i = 0; i > parameters.additiveAmount; i++) { maskop.Mask.Add(parameters.additiveMask); }
            }
            ///Start Algorithm of Mask(input) Single time
            
            return null;
        }
        
        public string MaskFromSeed(int seed, PassGenMaskOptions maskopt, out uint apos)
        {
            apos = 1;
            foreach(string s in maskopt.Mask) apos = apos * (uint)s.Length;
            return MaskFromSeed(seed, maskopt);
        }
        public string MaskFromSeed(int seed, PassGenMaskOptions maskopt)
        {
            return null;
        }      
    }


    public class PassGenParameters
    {
        public bool UseLegacyRules;
        public string LegacyRules;
        public byte additiveAmount; //max added characters
        public string additiveMask;
        public byte changeAmount; //max Simultanius changed letters, additive not counted
        public string changeMask;
    }
    public class PassGenMaskOptions
    {
        public List<string> Mask = new List<string>();
        public string[] CustomMasks; //max 100
        public string[] DefaultMasks; //Init first
        public void Init()
        {
            DefaultMasks = new string[6];
            DefaultMasks[0] = "abcdefghijklmnopqrstuvwxyz";
            DefaultMasks[1] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            DefaultMasks[2] = "1234567890";
            DefaultMasks[3] = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
        }
    }

    public class PassGenConst
    {
        public const string Mask_aZ09 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const string Mask_aZ09_SPcompatible = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=";
        public const string Mask_aZ09_SPall = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#$%&'()*+,-./:;<=>?@[]^_`{|}~";
    }
}
