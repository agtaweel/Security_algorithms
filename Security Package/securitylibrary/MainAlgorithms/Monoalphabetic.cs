using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
         
        public string Analyse(string plainText, string cipherText)
        {
            List<char> alpha = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] key = new char[26];
            cipherText = cipherText.ToLower();
            for (int i=0; i<plainText.Length;i++)
            {
                int index_pT = plainText[i] - 'a';
                key[index_pT] = cipherText[i];
                alpha.Remove(cipherText[i]);
            }
            for(int i=0;i<26;i++)
            {
                if (key[i] == '\0')
                {
                    key[i] = alpha[0];
                    alpha.RemoveAt(0);
                }
            }
            string result = new string(key);
            return result.ToLower();
     
        }

        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            string res = string.Empty;
            char[] charKey = key.ToCharArray();
            char[] charResult = new char[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                int index = key.IndexOf(cipherText[i]);
                charResult[i] =(char) (index+(char)'a') ;
            }
            res = new string(charResult);
            return res;
        }

        public string Encrypt(string plainText, string key)
        {
            string result = "";
            char[] charKey = key.ToCharArray();
            char[] charResult = new char[plainText.Length];
            for(int i=0; i<plainText.Length;i++)
            {
                charResult[i] = charKey[(char)plainText[i] - (char)'a'];
            }
            result = new string(charResult);
            return result;
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            throw new NotImplementedException();
        }
    }
}
