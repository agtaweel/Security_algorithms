using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        char[] alpha = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public void get_mat(char[,] mat)
        {
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    mat[j, i] = alpha[(j + i) % 26];
                }

            }

        }

        public string Analyse(string plainText, string cipherText)
        {
            string key = string.Empty;
            string key1 = string.Empty;
            char[,] mat = new char[26, 26];
            get_mat(mat);
            string key_txt = string.Empty;
            string cipherText1 = cipherText.ToLower();
            int x = 0;
            while (x < cipherText1.Length)
            {
                for (int y = 0; y < 26; y++)
                {
                    if (x == plainText.Length)
                    {
                        break;
                    }

                    else if (plainText[x] == mat[y, 0])
                    {
                        for (int z = 0; z < 26; z++)
                        {
                            if (x == plainText.Length)
                            {
                                break;
                            }
                            else if (cipherText1[x] == mat[y, z])
                            {
                                key += mat[0, z];
                                x++;
                                break;
                            }
                        }
                    }

                }
            }
            int c = 0;
            int j = 0;
            List<int> counters = new List<int>();
            for (int i = 0; i < key.Length; i++)
            {
                while (j < key.Length)
                {
                    if (key[i] == plainText[j])
                    {
                        c++;
                        j++;
                        break;
                    }
                    else
                    {
                        j = 0;

                        c = 0;
                        break;
                    }

                }
                counters.Add(c);
            }
            c = counters.Max();
            int c1 = key.Length - c;
            for (int i = 0; i < c1; i++)
            {
                key1 += key[i];
            }

            return key1;
        }

        public string Decrypt(string cipherText, string key)
        {
            string ct = string.Empty;
            char[,] mat = new char[26, 26];
            get_mat(mat);
            string key_txt = string.Empty;
            string cipherText1 = cipherText.ToLower();
            if (key.Length != cipherText1.Length)
            {
                key_txt = key;

            }
            int x = 0;
            int k = 0;
            string s = string.Empty;
            while (ct.Length < cipherText1.Length)
            {
                if (x == key_txt.Length)
                {
                    s = key_txt;
                    key_txt = string.Empty;
                    for (int i = ct.Length - s.Length; i < ct.Length; i++)
                        key_txt += ct[i];
                    x = 0;
                }
                else
                {
                    for (int y = 0; y < 26; y++)
                    {
                        if (x == key_txt.Length)
                        {
                            break;
                        }

                        else if (key_txt[x] == mat[0, y])
                        {
                            for (int z = 0; z < 26; z++)
                            {
                                if (x == key_txt.Length || k == cipherText1.Length)
                                {
                                    break;
                                }
                                else if (cipherText1[k] == mat[z, y])
                                {
                                    ct += mat[z, 0];
                                    x++;
                                    k++;
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            return ct;
        }

        public string Encrypt(string plainText, string key)
        {
            char[,] mat = new char[26, 26];
            get_mat(mat);
            string key_txt = string.Empty;
            string pt = string.Empty;
            if (key.Length != plainText.Length)
            {
                key_txt = key;
                for (int i = 0; i < (plainText.Length - key.Length); i++)
                {
                    key_txt += plainText[i];
                }
            }
            int x = 0;
            while (x < plainText.Length)
            {
                //int y = 0;
                for (int y = 0; y < 26; y++)
                {
                    if (x == key_txt.Length)
                    {
                        break;
                    }

                    else if (plainText[x] == mat[y, 0])
                    {
                        for (int z = 0; z < 26; z++)
                        {
                            if (x == key_txt.Length)
                            {
                                break;
                            }
                            else if (key_txt[x] == mat[0, z])
                            {
                                pt += mat[y, z];
                                x++;
                                break;
                            }
                        }


                    }
                }
            }
            return pt;
        }
    }
}
