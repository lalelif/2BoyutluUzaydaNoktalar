using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ds_project_2021
{
    class Neuron
    {
        public double input;
        public double weights;
        public Neuron(int input, double weights)
        {
            this.input = input;
            this.weights = weights;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            double[,] input = new double[,] { { 6.0, 5.0 }, { 2.0, 4.0 }, { -3.0, -5.0 }, { -1.0, -1.0 }, { 1.0, 1.0 }, { -2.0, 7.0 }, { -4.0, -2.0 }, { -6.0, 3.0 } }; // dökümanda verilen veriler
            double[,] test = new double[,] { { 0.75,0.36 }, { -0.84,0.55 }, { 0.82, -0.69 }, { -0.9, -0.14}, { 0.2, 0.1 } };  //kendim belirlediğim test verileri
            int dataNum = input.GetLength(0);
            
            for (int i = 0; i < input.GetLength(0); i++) // dökümandaki verileri 10'a böldürme işlemi
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    input[i, j] /= 10;
                }
            }
            int[] target = { 1, 1, -1, -1, 1, 1, -1, -1 };
            int[] target_test = { 1, -1, -1, -1, 1 };
            Console.WriteLine("\nDatas from documentation:");
            printOut(input, target);
            Console.WriteLine("\nTest datas:");
            printOut(test, target_test);
            
            Console.ReadKey();
        }


        
        private static void printOut(double[,] list,int[]target) //veri sayısına göre eğitim işlemini yapıp yazdırmaya yarayan metod
        {
            int dataNum = list.GetLength(0);
            Random r = new Random();
            double[] result = new double[dataNum];
            double min = -1.0;
            double max = 1.0;
            double[] weights = { r.NextDouble() * (max - min) + min, r.NextDouble() * (max - min) + min, r.NextDouble() * (max - min) + min }; //[-1,1] arasında rastgele belirlenen weightler
            double learningRate = 0.05;
            int success10 = 0;
            int success100 = 0;
            int epoch = 0;
            int max_epoch = 100;
            while (epoch <= max_epoch) 
            {
                for (int i = 0; i < dataNum; i++)
                {   
                    //output verilen değerlerle hesaplanır eğer output target'tan farklıysa yeniden düzenlenerek output hesaplanır
                    int output = calculateOutput(list[i, 0], list[i, 1], weights);
                    result[i] = output;
                    int error = target[i] - output;
                    weights[0] += learningRate * error * list[i, 0];
                    weights[1] += learningRate * error * list[i, 1];
                }
                epoch++;
                if (epoch == 10)
                {
                    Console.WriteLine("Results for 10 epoches:");
                    for (int i = 0; i < dataNum; i++)
                    {
                        Console.WriteLine(result[i]);
                        if (target[i] == result[i])
                            success10 += 1;
                    }
                    double rate10 = (double)success10 / dataNum;
                    Console.WriteLine("Success Rate:{0}\nSuccess Percentace: %{1}", rate10, rate10 * 100);
                }
                if (epoch == 100)
                {
                    Console.WriteLine("\nResults for 100 epoches:");
                    for (int i = 0; i < dataNum; i++)
                    {
                        Console.WriteLine(result[i]);
                        if (target[i] == result[i])
                            success100 += 1;
                    }
                    double rate100 = (double)success100 / dataNum;
                    Console.WriteLine("Success Rate:{0}\nSuccess Percentace: %{1}", rate100, rate100 * 100);
                }
            }
        }
        private static int calculateOutput(double input1, double input2, double[] weights) // output hesaplayan metod
        {
            double sum = input1 * weights[0] + input2 * weights[1] + 1 * weights[2];

            return (sum >= 0.5) ? 1 : -1; //eşik değeri 0.5
        }

    }
}
