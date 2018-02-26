using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NNTest
{
    class Program
    {
        static public int Answer(double n)
        {
            if(n >= 0.5)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        static void Main(string[] args)
        {
            NeuralNetwork nn = new NeuralNetwork(2, 4, 1);
            double[] kek;
            for (int i = 0; i <= 100000; i++)
            {
                nn.Train(new double[] { 0, 0 }, new double[] { 0 });
                nn.Train(new double[] { 1, 0 }, new double[] { 1 });
                nn.Train(new double[] { 0, 1 }, new double[] { 1 });
                nn.Train(new double[] { 1, 1 }, new double[] { 0 });
            }
            kek = nn.Predict(new double[] { 0, 0 });
            Console.WriteLine("predict dla 0, 0 to {0}", Program.Answer(kek[0]));
            kek = nn.Predict(new double[] { 1, 0 });
            Console.WriteLine("predict dla 1, 0 to {0}", Program.Answer(kek[0]));
            kek = nn.Predict(new double[] { 0, 1 });
            Console.WriteLine("predict dla 0, 1 to {0}", Program.Answer(kek[0]));
            kek = nn.Predict(new double[] { 1, 1 });
            Console.WriteLine("predict dla 1, 1 to {0}", Program.Answer(kek[0]));
            Console.ReadLine();
        }
    }
}
