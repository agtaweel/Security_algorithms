using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
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
            List<int> counters = new List<int>();
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 1; j < key.Length; j++)
                {
                    if (key[i] == key[j])
                    {
                        c++;
                    }

                }
                counters.Add(c);
                c = 0;
            }
            c = counters.Max();
            for (int i = 0; i < c; i++)
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
            for (int i = 0; i < cipherText.Length; i++)
            {
                key_txt += key[i % key.Length];
            }
            int x = 0;
            while (x < cipherText1.Length)
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
                            if (x == key_txt.Length)
                            {
                                break;
                            }
                            else if (cipherText1[x] == mat[z, y])
                            {
                                ct += mat[z, 0];
                                x++;
                                break;
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
            for (int i = 0; i < plainText.Length; i++)
            {
                key_txt += key[i % key.Length];
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