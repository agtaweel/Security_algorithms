using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            List<int> key = new List<int> { };
            int c = 0;
            for(int i=1;i<plainText.Length;i++)
            {
                if (plainText[i]!=cipherText[i])
                {
                    c++;
                }
            }
            return key;
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            string ct = string.Empty;
            int rows = key.Count;
            for (int i = 0; i < cipherText.Length / rows; i++)
            {
                if (cipherText.Length % rows != 0)
                {
                    cipherText += " ";
                }
                else
                {
                    break;
                }
            }
            int col = cipherText.Length / rows;
            char[,] mat = new char[rows, col];
            int c = 0;
            while (c < cipherText.Length)
            {
                for (int j = 0; j < rows; j++)
                {
                    for (int i = 0; i < col; i++)
                    {
                        if (c < cipherText.Length)
                        {
                            mat[key.IndexOf(j+1), i] = cipherText[c];
                            c++;
                        }
                    }
                }
                for (int j = 0; j < col; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (mat[key[i] - 1, j] == ' ')
                        {
                            break;
                        }
                        ct += mat[i, j];

                    }

                }
            }
            return ct;
        }


        public string Encrypt(string plainText, List<int> key)
        {
            string pt = string.Empty;
            int rows = key.Count;
            for (int i = 0; i < plainText.Length / rows; i++)
            {
                if (plainText.Length % rows != 0)
                {
                    plainText += " ";
                }
                else
                {
                    break;
                }
            }
            int col = plainText.Length / rows;
            char[,] mat = new char[rows, col];
            int c = 0;
            while (c < plainText.Length)
            {
                for (int j = 0; j < col; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (c < plainText.Length)
                        {
                            mat[i, j] = plainText[c];
                            c++;
                        }
                    }
                }
                for (int j = 0; j < rows; j++)
                {
                    for (int i = 0; i < col; i++)
                    {
                        if (mat[key[j] - 1, i] == ' ')
                        {
                            break;
                        }
                        pt += mat[key.IndexOf(j + 1), i];

                    }

                }
            }
            return pt;
        }
    }
}

