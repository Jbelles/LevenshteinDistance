using System;
using System.Collections.Generic;
using System.Linq;

namespace LevenshteinTest
{
    public class Program
    {
        public static void Main()
        {
            List<char[]> dictionary = new List<char[]>(){ "name1".ToCharArray(), "bryce".ToCharArray(), "kevin".ToCharArray(), "dorian".ToCharArray()};

            int[,] chart = new int[dictionary.Count(), dictionary.Count()];
            double[] avg = new double[dictionary.Count()];

            Array.Clear(chart, 0, chart.Length);
            for (int i = 0; i < dictionary.Count(); i++)
            {
                for(int j = 0; j < dictionary.Count(); j++)
                {
                    if (i != j)
                    {
                        var distance = LevenshteinDistance(dictionary[i], dictionary[j]);
                        chart[i, j] = distance;
                        if (j == 0)
                        avg[i] += distance;
                    }
                }
            }
            for(int i = 0; i < avg.Length; i++)
            {
                avg[i] /= dictionary.Count();
            }
            PrintNames(dictionary);
            PrintChart(chart);
            Console.ReadLine();
        }
        public static void PrintNames(List<char[]> Records)
        {
            foreach(char[] record in Records)
            {
                Console.Write("[" + new string(record) + "] ");
            }
            Console.WriteLine();
        }
        public static void PrintChart(int[,] chart)
        {

            for (int i = 0; i < chart.GetLength(0); i++)
            {
                for (int j = 0; j < chart.GetLength(0); j++)
                {
                    Console.Write($"[   {chart[i, j]}   ]");
                }
                Console.WriteLine();
            }
        }
        public static int LevenshteinDistance(char[] m, char[] n)
        {
            if(m.Length == n.Length)
            {
                return HammingDistance(m, n);
            }
            int[,] matrix = new int[m.Length+1, n.Length+1];
            Array.Clear(matrix, 0, matrix.Length);

            for (int i = 0; i < m.Length + 1; i++)
            {
                matrix[i, 0] = i;
            }

            for (int j = 0; j < n.Length + 1; j++)
            {
                matrix[0, j] = j;
            }

            for (int j = 1; j < n.Length+1; j++)
            {

                for (int i = 1; i < m.Length+1; i++)
                {
                    int substitutionCost = 0;

                    if (m[i-1] != n[j-1])
                    {
                        substitutionCost = 1;
                    }
                    var min = (matrix[i - 1, j - 1] + substitutionCost);
                    min = min < matrix[i - 1, j] + 1 ? min : matrix[i - 1, j] + 1;
                    min = min < matrix[i, j - 1] + 1 ? min : matrix[i, j - 1] + 1;
                    matrix[i, j] = min;
                }
            }

            return matrix[m.Length, n.Length];
        }

        public static int HammingDistance(char[] a, char[] b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception();
            }

            int sum = 0;

            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] == b[i] ? 0 : 1;
            }
            return sum;
        }
    }
}
