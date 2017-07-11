using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{


    public class AES : CryptographicTechnique
    {
        public int getintfromhex(char digit)
        {
            int x;

            if (digit == '0')
            {
                x = 0;
            }
            else if (digit == '1')
            {
                x = 1;
            }
            else if (digit == '2')
            {
                x = 2;
            }
            else if (digit == '3')
            {
                x = 3;
            }
            else if (digit == '4')
            {
                x = 4;
            }
            else if (digit == '5')
            {
                x = 5;
            }
            else if (digit == '6')
            {
                x = 6;
            }
            else if (digit == '7')
            {
                x = 7;
            }
            else if (digit == '8')
            {
                x = 8;
            }
            else if (digit == '9')
            {
                x = 9;
            }

            else if (digit == 'A' || digit == 'a')
            {
                x = 10;
            }

            else if (digit == 'B' || digit == 'b')
            {

                x = 11;
            }
            else if (digit == 'C' || digit == 'c')
            {

                x = 12;
            }
            else if (digit == 'D' || digit == 'd')
            {

                x = 13;
            }
            else if (digit == 'E' || digit == 'e')
            {

                x = 14;
            }
            else
            {
                x = 15;
            }



            return x;

        }

        public string stringtoBinary(int n, string s)
        {
            string res = "";


            if (s[0] == '0' && s[1] == 'x')
            {
                for (int j = 2; j < n; j++)
                {
                    res += Convert.ToString(getintfromhex(s[j]), 2).PadLeft(4, '0');
                }
            }
            else
            {

                for (int j = 0; j < n; j++)
                {

                    res += Convert.ToString(getintfromhex(s[j]), 2).PadLeft(4, '0');

                }
            }

            return res;
        }

        public string XOR(string s1, string s2, int n)
        {
            string res = "";

            for (int i = 0; i < n; i++)
            {
                res += Convert.ToString(Convert.ToInt32(s1[i]) ^ Convert.ToInt32(s2[i]));

            }
            return res;
        }
        public string multiBy2(string temp, string oneB)
        {
            string Res, tempShift;
            Res = "";
            tempShift = "";

            if (temp[0] == '0')
            {
                for (int i = 1; i < 8; i++)
                {
                    tempShift += temp[i];
                }
                tempShift += '0';
                Res = tempShift;
            }
            else
            {

                for (int i = 1; i < 8; i++)
                {
                    tempShift += temp[i];
                }
                tempShift += '0';

                Res = XOR(tempShift, oneB, 8);

            }


            return Res;
        }

        public string getHexafrombinary(string bin)
        {
            string hexa;

            if (bin == "0000")
                hexa = "0";
            else if (bin == "0001")
                hexa = "1";
            else if (bin == "0010")
                hexa = "2";
            else if (bin == "0011")
                hexa = "3";
            else if (bin == "0100")
                hexa = "4";
            else if (bin == "0101")
                hexa = "5";
            else if (bin == "0110")
                hexa = "6";
            else if (bin == "0111")
                hexa = "7";
            else if (bin == "1000")
                hexa = "8";
            else if (bin == "1001")
                hexa = "9";
            else if (bin == "1010")
                hexa = "a";
            else if (bin == "1011")
                hexa = "b";
            else if (bin == "1100")
                hexa = "c";
            else if (bin == "1101")
                hexa = "d";
            else if (bin == "1110")
                hexa = "e";
            else
                hexa = "f";

            return hexa;

        }
        public string subBytes(string temp, string[,] Sbox)
        {
            string resSubBytes = "";

            for (int i = 0; i < temp.Length; i += 2)
            {
                int x, y;
                x = getintfromhex(temp[i]);
                y = getintfromhex(temp[i + 1]);
                resSubBytes += Sbox[x, y];
            }

            return resSubBytes;
        }

        public string shiftRows(string resSubBytes)
        {
            string[,] tempArr = new string[4, 4];

            int index = 0;

            for (int r = 0; r < 4; ++r)
            {
                for (int c = 0; c < 4; ++c)
                {
                    tempArr[r, c] = string.Format("{0}{1}", resSubBytes[index], resSubBytes[index + 1]);

                    index += 2;
                }
            }
            resSubBytes = "";

            for (int i = 0; i < 4; i++)
            {
                resSubBytes += tempArr[i, 0];
            }

            for (int r = 1; r < 4; ++r)
            {
                for (int c = 0; c < 4; ++c)
                {
                    resSubBytes += tempArr[((c + r) % 4), r];
                }
            }
            return resSubBytes;
        }

        public string mixcolROw(string s)
        {
            string mixCol, shiftRes, resultTemp, resultCell, temp, RCon, oneB, tempS, resultRow;
            temp = "";
            RCon = "2311123111233112";
            oneB = "00011011";
            resultTemp = "";
            resultRow = "";
            resultCell = "";
            for (int j = 0; j < 16; j++)
            {

                tempS = s.Substring((j % 4) * 8, 8);
                if (RCon[j] == '1')
                {
                    mixCol = tempS;

                }
                else
                {
                    shiftRes = multiBy2(tempS, oneB);

                    if (RCon[j] == '3')
                    {

                        mixCol = XOR(shiftRes, tempS, 8);
                    }
                    else
                    {
                        mixCol = shiftRes;
                    }
                }



                if (j == 0 || j % 4 == 0)
                {
                    temp = mixCol;
                }
                else
                {
                    resultTemp = XOR(mixCol, temp, 8);
                    temp = resultTemp;

                }

                if ((j + 1) % 4 == 0)
                {
                    resultCell = "";
                    for (int k = 0; k < temp.Length / 4; k++)
                    {
                        resultCell += getHexafrombinary(temp.Substring(k * 4, 4));
                    }
                    resultRow += resultCell;
                }


            }
            return resultRow;
        }

        public string shiftW3(string key)
        {
            string temp, tempKey;
            temp = "";
            tempKey = "";
            temp += key.Substring(24, 2);
            tempKey += key;
            key = "";

            for (int i = 0; i < 30; i++)
            {
                if (i < 24)
                {
                    key += tempKey[i];
                }
                else
                {
                    key += tempKey[i + 2];
                }
            }
            key += temp;

            return key;

        }
        public string W0(string key, string keyShift, string[,] Sbox, string rconKEY, int r)
        {
            string resSub, temp, res1, res2, tempKey, result, RES;
            resSub = temp = res1 = res2 = tempKey = RES = result = "";

            resSub = subBytes(keyShift.Substring(24, 8), Sbox);

            res1 += stringtoBinary(8, key.Substring(0, 8));

            res2 += stringtoBinary(8, resSub);

            temp += stringtoBinary(8, rconKEY.Substring(r * 8, 8));//


            result = "";
            result += XOR(res2, temp, temp.Length);

            tempKey = "";
            tempKey += XOR(res1, result, res1.Length);


            for (int i = 0; i < tempKey.Length / 4; i++)
            {
                RES += getHexafrombinary(tempKey.Substring(i * 4, 4));
            }
            return RES;
        }

        public string w1w2w3(string tempKey, string key)
        {
            string temp, res1, res2, result, RES;
            RES = "";
            temp = tempKey;

            for (int i = 1; i < 4; i++)
            {
                res1 = stringtoBinary(8, key.Substring(i * 8, 8));
                res2 = stringtoBinary(8, temp);
                result = XOR(res1, res2, res1.Length);
                for (int j = 0; j < result.Length / 4; j++)
                {
                    RES += getHexafrombinary(result.Substring(j * 4, 4));
                }
                tempKey += RES;

                temp = RES;
                RES = "";
            }
            return tempKey;
        }

        public string addRoundKey(string key, string[,] Sbox, string rconKEY, int r)
        {
            string keyShift, keyTemp;

            keyShift = shiftW3(key);
            keyTemp = key;
            key = "";
            key += W0(keyTemp, keyShift, Sbox, rconKEY, r);//
            key = w1w2w3(key, keyTemp);

            return key;

        }


        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            string temp, temp2, res1, XORres, res2, resSubBytes, resShift, resultMixCol, rconKEY, RES;
            temp = "";
            temp2 = "";
            res1 = "";
            res2 = "";
            XORres = "";
            resSubBytes = "";
            resultMixCol = "";
            RES = "";
            string[,] Sbox = new string[16, 16] {  // populate the Sbox matrix
          /* 0     1     2     3     4     5     6     7     8     9     a     b     c     d     e     f */
    /*0*/  {"63", "7c", "77", "7b", "f2", "6b", "6f", "c5", "30", "01", "67", "2b", "fe", "d7", "ab", "76"},
    /*1*/  {"ca", "82", "c9", "7d", "fa", "59", "47", "f0", "ad", "d4", "a2", "af", "9c", "a4", "72", "c0"},
    /*2*/  {"b7", "fd", "93", "26", "36", "3f", "f7", "cc", "34", "a5", "e5", "f1", "71", "d8", "31", "15"},
    /*3*/  {"04", "c7", "23", "c3", "18", "96", "05", "9a", "07", "12", "80", "e2", "eb", "27", "b2", "75"},
    /*4*/  {"09", "83","2c", "1a", "1b", "6e", "5a", "a0", "52", "3b", "d6", "b3", "29", "e3", "2f", "84"},
    /*5*/  {"53", "d1", "00", "ed", "20", "fc", "b1", "5b", "6a", "cb", "be", "39", "4a", "4c", "58", "cf"},
    /*6*/  {"d0", "ef", "aa", "fb", "43", "4d", "33", "85", "45", "f9", "02", "7f", "50", "3c", "9f", "a8"},
    /*7*/  {"51", "a3", "40", "8f", "92", "9d", "38", "f5", "bc", "b6", "da", "21", "10", "ff", "f3", "d2"},
    /*8*/  {"cd", "0c", "13", "ec","5f", "97", "44", "17", "c4", "a7", "7e", "3d", "64", "5d", "19", "73"},
    /*9*/  {"60", "81", "4f", "dc", "22", "2a", "90", "88", "46", "ee", "b8", "14", "de", "5e", "0b", "db"},
    /*a*/  {"e0", "32", "3a", "0a", "49", "06", "24", "5c", "c2", "d3", "ac", "62", "91", "95", "e4", "79"},
    /*b*/  {"e7", "c8", "37", "6d","8d", "d5", "4e", "a9", "6c", "56", "f4", "ea", "65", "7a", "ae", "08"},
    /*c*/  {"ba", "78", "25", "2e", "1c", "a6", "b4", "c6", "e8", "dd", "74", "1f", "4b", "bd", "8b", "8a"},
    /*d*/  {"70", "3e", "b5", "66", "48", "03", "f6", "0e", "61", "35", "57", "b9", "86", "c1", "1d", "9e"},
    /*e*/  {"e1", "f8", "98", "11", "69", "d9", "8e", "94", "9b", "1e", "87", "e9", "ce", "55", "28", "df"},
    /*f*/  {"8c", "a1", "89", "0d", "bf", "e6", "42", "68", "41", "99", "2d", "0f", "b0", "54", "bb", "16"} };

            rconKEY = "01000000020000000400000008000000100000002000000040000000800000001b00000036000000";

            string[] allRoundKEYS = new string[20];


            allRoundKEYS[0] = key.Substring(2, 32);
            for (int i = 1; i < 11; i++)
            {
                allRoundKEYS[i] = addRoundKey(allRoundKEYS[i - 1], Sbox, rconKEY, i - 1);

            }
            res1 += stringtoBinary(plainText.Length, plainText);

            res2 += stringtoBinary(allRoundKEYS[0].Length, allRoundKEYS[0]);

            XORres += XOR(res1, res2, res1.Length);
            temp = "";
            for (int j = 0; j < XORres.Length / 4; j++)
            {
                temp += getHexafrombinary(XORres.Substring(j * 4, 4));
            }
            for (int r = 1; r < 10; r++)
            {

                resSubBytes += subBytes(temp, Sbox);

                resShift = shiftRows(resSubBytes);

                temp = "";
                temp += stringtoBinary(resShift.Length, resShift);

                temp2 = "";
                for (int i = 0; i < 32; i += 8)
                {
                    int index = i;
                    for (int j = 0; j < 4; j++)
                    {

                        temp2 += temp.Substring(index, 8);
                        index += 32;
                    }

                }
                RES = "";

                for (int i = 0; i < temp2.Length / 32; i++)
                {
                    RES = mixcolROw(temp2.Substring(i * 32, 32));

                    resultMixCol += RES;

                }

                plainText = "";
                plainText += resultMixCol;


                resultMixCol = "";
                res1 = "";
                res2 = "";
                XORres = "";
                temp = "";
                resSubBytes = "";

                res1 += stringtoBinary(plainText.Length, plainText);
                res2 += stringtoBinary(allRoundKEYS[r].Length, allRoundKEYS[r]);
                XORres += XOR(res1, res2, res1.Length);
                temp = "";
                for (int j = 0; j < XORres.Length / 4; j++)
                {
                    temp += getHexafrombinary(XORres.Substring(j * 4, 4));
                }


            }

            resSubBytes += subBytes(temp, Sbox);

            resShift = shiftRows(resSubBytes);

            plainText = "";
            plainText += resShift;

            res1 = "";
            res2 = "";
            XORres = "";
            temp = "";




            res1 += stringtoBinary(plainText.Length, plainText);
            res2 += stringtoBinary(allRoundKEYS[10].Length, allRoundKEYS[10]);


            temp2 = "";
            for (int i = 0; i < 32; i += 8)
            {
                int index = i;
                for (int j = 0; j < 4; j++)
                {

                    temp2 += res1.Substring(index, 8);
                    index += 32;
                }

            }
            res1 = temp2;

            XORres += XOR(res1, res2, res1.Length);

            for (int j = 0; j < XORres.Length / 4; j++)
            {
                temp += getHexafrombinary(XORres.Substring(j * 4, 4));
            }
            temp = temp.ToUpper();
            temp2 = "0x";
            temp2 += temp;


            return temp2;


        }
    }
}
