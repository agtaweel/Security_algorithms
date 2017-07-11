using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            int key = 0;
            string cipherText1 = cipherText.ToLower();
            List<int> keys = new List<int>();
            int j = 0;
            //for (int i = 0; i < cipherText1.Length; i++)
            //{
            //   while(j<plainText.Length)
            //    {
            //        if (plainText[j] == cipherText1[i])
            //        {
            //            keys.Add(key);
            //            key = 0;
            //            j++;
            //            break;
            //        }
            //        else
            //        {
            //            key++;
            //            j++;
            //        }
            //    }
            //}
            //key = keys.Max();
            int count = 0;
            for (int i = 1; i < plainText.Length; i++)
            {
                j = count;
                while (j < cipherText1.Length)
                {
                    if (plainText[i] != cipherText1[j])
                    {
                        key++;
                        j++;
                    }
                    else if (j == cipherText1.Length)
                        j = 0;
                    else
                    {
                        count++;
                        keys.Add(key);
                        key = 0;
                        break;
                    }
                }
            }
            
            count = 0;
            List<int> counters = new List<int>();
            List<int> k = new List<int>();
            while (keys.Count != 0)
            {
                int x = keys.Max();
                if (x == 0)
                    break;
                k.Add(x);
                for (int i = 0; i < keys.Count; i++)
                {
                    if (i != keys.IndexOf(x) && x == keys[i])
                        count++;
                }
                counters.Add(count);
                keys.Remove(x);
            }
            int index = counters.IndexOf(counters.Max());
            key = k[index];
            return key;
        }

        public string Decrypt(string cipherText, int key)
        {
            string ct = string.Empty;
            for (int i = 0; i < cipherText.Length / key; i++)
            {
                if (cipherText.Length % key != 0)
                {
                    cipherText += " ";
                }
                else
                {
                    break;
                }
            }
            int col = cipherText.Length / key;
            char[,] mat = new char[col, key];
            int c = 0;
            while (c < cipherText.Length)
            {
                for (int j = 0; j < key; j++)
                {
                    for (int i = 0; i < col; i++)
                    {
                        if (c < cipherText.Length)
                        {
                            mat[i, j] = cipherText[c];
                            c++;

                        }
                        else
                        {
                            break;
                        }
                    }

                }
            }
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < key; j++)

                {
                    if (mat[i, j] == ' ')
                    {
                        break;
                    }
                    ct += mat[i, j];
                }
            }

            return ct;
        }

        public string Encrypt(string plainText, int key)
        {
            string pt = string.Empty;
            for (int i = 0; i < plainText.Length / key; i++)
            {
                if (plainText.Length % key != 0)
                {
                    plainText += " ";
                }
                else
                {
                    break;
                }
            }

            int col = plainText.Length / key;
            char[,] mat = new char[col, key];
            int c = 0;
            while (c < plainText.Length)
            {
                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < key; j++)
                    {
                        if (c < plainText.Length)
                        {
                            mat[i, j] = plainText[c];
                            c++;
                        }
                        else
                        {
                            break;
                        }
                    }

                }
            }
            for (int j = 0; j < key; j++)
            {
                for (int i = 0; i < col; i++)
                {
                    if (mat[i, j] == ' ')
                    {
                        break;
                    }
                    pt += mat[i, j];
                }
            }
            return pt;
        }
    }
}
