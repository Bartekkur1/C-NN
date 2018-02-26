using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNTest
{
    class Matrix
    {
        public int rows, cols;
        public double[,] data;
        Random r = new Random();

        public Matrix(int _rows, int _cols)
        {
            rows = _rows;
            cols = _cols;
            data = new double[rows, cols];
        }

        static public Matrix SigmoidElements(Matrix a)
        {
            for (int x = 0; x <= a.rows - 1; x++)
            {
                for (int y = 0; y <= a.cols - 1; y++)
                {
                    a.data[x, y] = ActivationFunction.Sigmoid(a.data[x, y]);
                }
            }
            return a;
        }

        static public Matrix DsigmoidElements(Matrix a)
        {
            for (int x = 0; x <= a.rows - 1; x++)
            {
                for (int y = 0; y <= a.cols - 1; y++)
                {
                    a.data[x, y] = ActivationFunction.Dsigmoid(a.data[x, y]);
                }
            }
            return a;
        }

        static public Matrix FromArray(double[] arr)
        {
            Matrix temp = new Matrix(arr.Length, 1);
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                temp.data[i, 0] = arr[i];
            }
            return temp;
        }

        static public Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.cols != b.cols)
            {
                Console.WriteLine("substract - zly rozmiar");
                return null;
            }
            else
            {
                Matrix temp = new Matrix(a.rows, a.cols);
                for (int x = 0; x <= a.rows - 1; x++)
                {
                    for (int y = 0; y <= a.cols - 1; y++)
                    {
                        temp.data[x, y] = a.data[x, y] - b.data[x, y];
                    }
                }
                return temp;
            }
        }

        static public Matrix Transpose(Matrix a)
        {
            Matrix temp = new Matrix(a.cols, a.rows);
            for (int x = 0; x <= a.rows - 1; x++)
            {
                for (int y = 0; y <= a.cols - 1; y++)
                {
                    temp.data[y, x] = a.data[x, y];
                }
            }
            return temp;
        }

        static public Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.cols != b.rows)
            {
                Console.WriteLine("Multiply - zly rozmiar");
                return null;
            }
            else
            {
                Matrix temp = new Matrix(a.rows, b.cols);
                double sum = 0;

                for (int x = 0; x <= temp.rows - 1; x++)
                {
                    for (int y = 0; y <= temp.cols - 1; y++)
                    {
                        for (int i = 0; i < a.cols; i++)
                        {
                            sum += a.data[x, i] * b.data[i, y];
                        }
                        temp.data[x, y] = sum;
                        sum = 0;
                    }
                }
                return temp;
            }
        }

        public double[] ToArray()
        {
            double[] kek = new double[cols * rows];
            int indx = 0;
            for (int x = 0; x <= rows - 1; x++)
            {
                for (int y = 0; y <= cols - 1; y++)
                {
                    kek[indx] = data[x, y];
                    indx++;
                }
            }
            return kek;
        }

        public void RandomizeInt(int min = 0, int max = 10)
        {
            for (int x = 0; x <= rows - 1; x++)
            {
                for (int y = 0; y <= cols - 1; y++)
                {
                    data[x, y] = r.Next(min, max);
                }
            }
        }

        public void RandomizeF(float min = -1, float max = 1)
        {
            for (int x = 0; x <= rows - 1; x++)
            {
                for (int y = 0; y <= cols - 1; y++)
                {
                    data[x, y] = min + r.NextDouble() * (max - min);
                }
            }
        }

        public void Print()
        {
            for (int x = 0; x <= rows - 1; x++)
            {
                for (int y = 0; y <= cols - 1; y++)
                {
                    Console.Write(data[x, y] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Add(Matrix a)
        {
            if (rows != a.rows || cols != a.cols)
            {
                Console.WriteLine("Add - zly rozmiar");
            }
            else
            {
                for (int x = 0; x <= rows - 1; x++)
                {
                    for (int y = 0; y <= cols - 1; y++)
                    {
                        data[x, y] += a.data[x, y];
                    }
                }
            }
        }

        public void Multiply(Matrix a)
        {
            if (rows != a.rows || cols != a.cols)
            {
                Console.WriteLine("multiply - zly rozmiar");
            }
            else
            {
                for (int x = 0; x <= rows - 1; x++)
                {
                    for (int y = 0; y <= cols - 1; y++)
                    {
                        data[x, y] *= a.data[x, y];
                    }
                }
            }
        }

        public void Multiply(double n)
        {
            for (int x = 0; x <= rows - 1; x++)
            {
                for (int y = 0; y <= cols - 1; y++)
                {
                    data[x, y] *= n;
                }
            }
        }
    }
}
