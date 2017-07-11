using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {

        static int GetMultiplicativeInverse(int number, int baseN)
        {
            int x1, x2, y1, d, x3, temp1, temp2, x, y;

            d = 0;
            x1 = 0;
            x2 = 1;
            y1 = 1;
            x3 = baseN;

            while (number > 0)
            {
                temp1 = x3 / number;
                temp2 = x3 - (temp1 * number);
                x3 = number;
                number = temp2;

                x = x2 - (temp1 * x1);
                y = d - (temp1 * y1);

                x2 = x1;
                x1 = x;
                d = y1;
                y1 = y;

                if (x3 == 1)
                {
                    if (d < 0)
                        return d + baseN;
                    else
                        return d;
                }
            }
            return 0;
        }
        public int Encrypt(int p, int q, int M, int e)
        {

            int n = p * q;
            double res, final;
            final = 1;

            while (e > 5)
            {
                e = e - 5;
                res = Math.Pow(M, 5);
                res = res % n;
                final = (final * res);
                final = (final % n);
            }

            res = Math.Pow(M, e);
            res = res % n;
            final = (final * res);
            final = (final % n);
            if (final < 0)
                final += n;

            return (int)final;


        }

        public int Decrypt(int p, int q, int C, int e)
        {
            double final;
            double n = p * q;
            double Q = (p - 1) * (q - 1);
            double d, res;

            final = 1;
            d = GetMultiplicativeInverse(e, (int)Q);

            while (d > 5)
            {
                d = (int)d - 5;
                res = Math.Pow(C, 5);
                res = res % n;
                final = (final * res);
                final = (final % n);
            }

            res = Math.Pow(C, d);
            res = res % n;
            final = (final * res);
            final = (final % n);
            if (final < 0)
                final += n;

            return (int)final;
        }
    }
}
