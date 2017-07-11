using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        private List<char> alpha = new List<char> {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' }; 
        public string Encrypt(string plainText, int key)
        {
            string res = string.Empty;
            char[] lower_pT = plainText.ToLower().ToCharArray();
            
            for(int i=0;i< lower_pT.Length;i++)
            {
                int index = alpha.IndexOf(lower_pT[i]);
                index += key;
                index = index % 26;
                lower_pT[i]= alpha[index];                
            }
            res = new string(lower_pT);
            return res;
        }

        public string Decrypt(string cipherText, int key)
        {
            string res = string.Empty;

            char[] lower_cT = cipherText.ToLower().ToCharArray();
            
            for (int i = 0; i < lower_cT.Length; i++)
            {
                int index = alpha.IndexOf(lower_cT[i]);
                index -= key;
                index = index % 26;
                if(index<0)
                {
                    index += 26;
                }
                lower_cT[i] = alpha[index];
                
            }
            res = new string(lower_cT);
            return res;
        }

        public int Analyse(string plainText, string cipherText)
        {
            
            char[] lower_pT = plainText.ToCharArray();
            char[] lower_cT = cipherText.ToLower().ToCharArray();
            int index_pT = alpha.IndexOf(lower_pT[0]);
            int index_cT = alpha.IndexOf(lower_cT[0]);
            int key = index_cT - index_pT;
            if(key<0)
            {
                key += 26;
            }
            return key;
        }
    }
}
