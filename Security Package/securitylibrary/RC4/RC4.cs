using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RC4
{

    public class RC4 : CryptographicTechnique
    {

        private static int BinaryToDecimal(string bin)
        {
            int binLength = bin.Length;
            double dec = 0;

            for (int i = 0; i < binLength; ++i)
            {
                dec += ((byte)bin[i] - 48) * Math.Pow(2, ((binLength - i) - 1));
            }

            return (int)dec;
        }

        public static string ToHexString(string str)
        {

            StringBuilder sb = new StringBuilder();

            foreach (char c in str)
                sb.AppendFormat("{0:x2} ", (int)c);

            string res = sb.ToString();
            string final = "0x";
            for (int i = 0; i < res.Length; i++)
            {

                if (res[i] != ' ')
                    final += res[i];
            }


            return final;
        }

        public static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.ASCII.GetString(bytes);
        }

        static string HEXRC4(string Ptext, string key)
        {


            string result, RES;
            int temp, Stemp;
            int[] S = new int[300];

            int x = 0;
            int y = 0;
            result = "";
            RES = "";

            result = FromHexString(Ptext.Substring(2));
            key = FromHexString(key.Substring(2));
            Console.WriteLine(result);
            Console.WriteLine(key);
            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
            {
                S[i] = i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;

                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }

            for (int m = 0; m < result.Length; m++)
            {
                x = (x + 1) % 256;
                y = (y + S[x]) % 256;

                temp = S[x];
                S[x] = S[y];
                S[y] = temp;

                Stemp = S[(S[x] + S[y]) % 256];

                RES += Convert.ToString((result[m] ^ Stemp), 2).PadLeft(8, '0');

            }


            string ascii = "";

            for (int i = 0; i < RES.Length; i += 8)
            {
                ascii += (char)BinaryToDecimal(RES.Substring(i, 8));
            }

            string final = ToHexString(ascii);


            return final;
        }

        public override string Decrypt(string cipherText, string key)
        {
            string result;
            int temp, Stemp;
            int[] S = new int[300];

            int x = 0;
            int y = 0;
            result = "";
            if (cipherText[0] == '0' && cipherText[1] == 'x')
            {
                return HEXRC4(cipherText, key);

            }
            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
            {
                S[i] = i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;

                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }

            for (int m = 0; m < cipherText.Length; m++)
            {
                x = (x + 1) % 256;
                y = (y + S[x]) % 256;

                temp = S[x];
                S[x] = S[y];
                S[y] = temp;

                Stemp = S[(S[x] + S[y]) % 256];

                result += Convert.ToString((cipherText[m] ^ Stemp), 2).PadLeft(8, '0');

            }


            string ascii = "";

            for (int i = 0; i < result.Length; i += 8)
            {
                ascii += (char)BinaryToDecimal(result.Substring(i, 8));
            }

            return ascii;

        }

        public override string Encrypt(string plainText, string key)
        {
            string result;
            int temp, Stemp;
            int[] S = new int[300];

            int x = 0;
            int y = 0;
            result = "";
            if (plainText[0] == '0' && plainText[1] == 'x')
            {
                return HEXRC4(plainText, key);
            }

            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
            {
                S[i] = i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;

                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }

            for (int m = 0; m < plainText.Length; m++)
            {
                x = (x + 1) % 256;
                y = (y + S[x]) % 256;

                temp = S[x];
                S[x] = S[y];
                S[y] = temp;

                Stemp = S[(S[x] + S[y]) % 256];

                result += Convert.ToString((plainText[m] ^ Stemp), 2).PadLeft(8, '0');

            }


            string ascii = "";

            for (int i = 0; i < result.Length; i += 8)
            {
                ascii += (char)BinaryToDecimal(result.Substring(i, 8));
            }

            return ascii;

        }
    }
}
