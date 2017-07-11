using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographicTechnique<string, string>
    {
        /// <summary>
        /// The most common diagrams in english (sorted): TH, HE, AN, IN, ER, ON, RE, ED, ND, HA, AT, EN, ES, OF, NT, EA, TI, TO, IO, LE, IS, OU, AR, AS, DE, RT, VE
        /// </summary>
        /// <param name='plainText'></param>
        /// <param name='cipherText'></param>
        /// <returns></returns>
        public string Analyse(string plainText)
        {
            throw new NotImplementedException();
        }

        public string Analyse(string plainText, string cipherText)
        {
            throw new NotSupportedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }
        public char[,] CreateKey(string key)
        {
            List<char> keyList = key.ToList<char>();
            char[,] result = new char[5, 5];
            List<char> added = new List<char>();

            List<char> alpha = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (keyList.Count == 0)
                    {
                        result[i, j] = alpha[0];
                        alpha.RemoveAt(0);
                    }
                    else
                    {
                        if (!added.Contains(keyList[0]) || (keyList[0] == 'j' && !added.Contains('i')))
                        {
                            if (keyList[0] == 'j')
                            {
                                result[i, j] = 'i';
                                added.Add('i');
                                alpha.Remove('i');
                            }
                            else
                            {
                                result[i, j] = keyList[0];
                                added.Add(keyList[0]);
                            }
                            alpha.Remove(keyList[0]);
                            keyList.RemoveAt(0);

                        }
                        else
                        {
                            keyList.RemoveAt(0);
                            j--;
                        }
                    }
                }
            }
            return result;
        }

        public string Encrypt(string plainText, string key)
        {
            char[,] keyMatrix = CreateKey(key);
            string result = "";

            List<char> modified = PlainModify(plainText);
            if (modified.Count % 2 == 1)
            {
                modified.Add('x');
            }
            List<char> temp = new List<char>(modified.Count);
            char[] temp_1 = new char[modified.Count];
            for (int i = 0; i < modified.Count; i += 2)
            {
                Point pos1, pos2;
                pos1 = getCharIndex(modified[i], keyMatrix);
                pos2 = getCharIndex(modified[i + 1], keyMatrix);

                if (pos1.x == pos2.x)
                {

                    temp_1[i] = keyMatrix[pos1.x, (pos1.y + 1) % 5];
                    temp_1[i + 1] = (keyMatrix[pos2.x, (pos2.y + 1) % 5]);
                }
                else if (pos1.y == pos2.y)
                {
                    temp_1[i] = (keyMatrix[(pos1.x + 1) % 5, pos1.y]);
                    temp_1[i + 1] = (keyMatrix[(pos2.x + 1) % 5, pos2.y]);
                }
                else
                {
                    temp_1[i] = (keyMatrix[pos1.x, pos2.y]);
                    temp_1[i + 1] = (keyMatrix[pos2.x, pos1.y]);
                }
            }
            result = new string(temp_1);
            return result.ToUpper();
        }
        public List<char> PlainModify(string text)
        {
            List<char> result = new List<char>();
            result = text.ToList<char>();
            int length = text.Length;
            for (int i = 0; i < length; i++)
            {
                if (result[i] == 'j')
                {
                    result[i] = 'i';
                }
            }
            for (int i = 0; i < length - 1; i += 2)
            {
                if (result[i] == result[i + 1])
                {
                    result.Insert(i + 1, 'x');
                    length++;
                }
            }


            return result;
        }
        public Point getCharIndex(char x, char[,] key)
        {

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (key[i, j] == x)
                    {
                        return new Point { x = i, y = j };
                    }
                }
            }
            return null;
        }

        public class Point
        {
            public int x, y;
        }
    }
}
