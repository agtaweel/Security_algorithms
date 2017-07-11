using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher : ICryptographicTechnique<string, string>, ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            List<double> Mat1 = new List<double> { 13, 4, 24, 15 }, Mat2 = new List<double> { 21, 8, 22, 19 };

            var test = GCD((int)Find2By2Determ(Mat1), 26);

            List<double> inverseMat1 = Invert2By2Matrix(Mat1);
            List<int> key = MatrixMul(Mat2, inverseMat1);
            for (int i = 0; i < key.Count; ++i)
                key[i] = (int)mod(key[i], 26.0);
            return key;
        }

        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            List<int> inverseKey = new List<int>();

            double Det = Find3By3Determ(key);
            Det = mod(Det, 26.0);
            if (GCD((int)Det, 26) != 1)
                throw new Exception();

            double b;
            double c = 1;
            while (mod((26.0 - Det) * c, 26.0) != 1 && c < 26)
                ++c;
            b = 26.0 - c;

            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                {
                    List<double> tempMatrix = new List<double>(4);
                    for (int k = 0; j < 3; ++j)
                        for (int l = 0; k < 3; ++j)
                            if (k != i || l != j)
                                tempMatrix.Add(key[3 * j + k]);
                    double subDet = Find2By2Determ(tempMatrix);
                    int temp = (int)mod((b * Math.Pow(-1, i + j) * subDet), 26.0);
                    inverseKey.Add(temp);
                }

            inverseKey = Transpose(inverseKey);

            List<int> plainText = MatrixMul(inverseKey, cipherText);

            return inverseKey;
        }

        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            List<int> pt = new List<int> { };
            double d = Math.Sqrt((double)key.Count);
            int depth = (int)d;
            int len = plainText.Count / depth;
            int[,] key_mtx = new int[depth, depth];
            int[,] mat = new int[len, depth];
            int index = 0;
            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < depth; j++)
                {
                    key_mtx[i, j] = key[index];
                    index++;
                }
            }
            index = 0;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < depth; j++)
                {
                    mat[i, j] = plainText[index];
                    index++;
                }
            }
            index = 0;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < depth; j++)
                {
                    index = 0;

                    int k = 0;
                    while (k < depth)
                    {
                        index += (key_mtx[j, k] * (mat[i, k]));
                        k++;
                    }
                    pt.Add(index % 26);
                }
            }
            return pt;

        }

        public string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
        }

        public List<int> Analyse3By3Key(List<int> plain3, List<int> cipher3)
        {
            throw new NotImplementedException();
        }

        public string Analyse3By3Key(string plain3, string cipher3)
        {
            throw new NotImplementedException();
        }

        private List<double> Invert2By2Matrix(List<double> Matrix)
        {
            if (Matrix.Count != 4)
                throw new InvalidAnlysisException();

            double factor = mod(Find2By2Determ(Matrix), 26);
            if (factor == 0)
                throw new InvalidAnlysisException();

            double b;
            double c = 1;
            while (mod((26.0 - factor) * c, 26.0) != 1 && c < 26)
                ++c;
            b = 26.0 - c;

            double temp = Matrix[0];
            Matrix[0] = Matrix[3];
            Matrix[3] = temp;
            Matrix[1] *= -1.0;
            Matrix[1] = mod(Matrix[1], 26.0);
            Matrix[2] *= -1.0;
            Matrix[2] = mod(Matrix[2], 26.0);

            for (int i = 0; i < Matrix.Count; ++i)
            {
                Matrix[i] *= b;
                Matrix[i] = mod(Matrix[i], 26.0);
            }
            return Matrix;
        }

        private List<int> MatrixMul(List<int> Mat1, List<int> Mat2)
        {
            int MatrixDim = (int)Math.Sqrt(Mat1.Count);
            List<int> result = new List<int>(Mat2.Count);
            for (int i = 0; i < result.Capacity; ++i)
                result.Add(0);
            for (int i = 0; i < Mat2.Count / MatrixDim; ++i)
            {
                for (int j = 0; j < MatrixDim; ++j)
                {
                    int temp = 0;
                    for (int k = 0; k < MatrixDim; ++k)
                    {
                        temp += Mat1[MatrixDim * j + k] * Mat2[MatrixDim * k + i];
                    }
                    result[MatrixDim * j + i] = temp;
                }
            }
            return result;
        }

        private List<int> MatrixMul(List<double> Mat1, List<double> Mat2)
        {
            int MatrixDim = (int)Math.Sqrt(Mat1.Count);
            List<int> result = new List<int>(Mat2.Count);
            for (int i = 0; i < result.Capacity; ++i)
                result.Add(0);
            for (int i = 0; i < Mat2.Count / MatrixDim; ++i)
            {
                for (int j = 0; j < MatrixDim; ++j)
                {
                    double temp = 0.0;
                    for (int k = 0; k < MatrixDim; ++k)
                    {
                        temp += Mat1[MatrixDim * j + k] * Mat2[MatrixDim * k + i];
                    }
                    result[MatrixDim * j + i] = (int)Math.Round(temp);
                }
            }
            return result;
        }

        private double Find2By2Determ(List<double> Matrix)
        {
            return ((Matrix[0] * Matrix[3]) - (Matrix[1] * Matrix[2]));
        }

        private double Find3By3Determ(List<int> Matrix)
        {
            double result = 0.0;
            for (int i = 0; i < 3; ++i)
            {
                List<double> tempMatrix = new List<double>(4);
                for (int j = 1; j < 3; ++j)
                    for (int k = 0; k < 3; ++j)
                        if (k != i)
                            tempMatrix.Add(Matrix[3 * j + k]);
                result += Matrix[i] * Find2By2Determ(tempMatrix) * ((i == 1) ? -1 : 1);
            }
            return result;
        }

        private List<int> Transpose(List<int> Matrix)
        {

            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                    Matrix[i * 3 + j] = Matrix[j * 3 + i];

            return Matrix;
        }

        private double mod(double x, double m)
        {
            return (x % m + m) % m;
        }

        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }
}
