using System;
using System.Collections.Generic;
using System.Linq;

namespace LevenshteinTest
{
    public class Program
    {
        public static void Main()
        {
            List<char[]> dictionary = new List<char[]>() { "word".ToCharArray(), "word1".ToCharArray(), "ward".ToCharArray(), "weird".ToCharArray(), "olfw".ToCharArray(), "asdasdqweqweasdasdqwe123123".ToCharArray() };
            char[] target = "wolf".ToCharArray();

            (char[], int)[] chart = new (char[], int)[dictionary.Count()];
            Array.Clear(chart, 0, chart.Length);

            for (int j = 0; j < dictionary.Count(); j++)
            {
                var distance = LevenshteinDistance(target, dictionary[j]);
                chart[j] = (dictionary[j], distance);
            }
            Console.WriteLine($"Comparing to target word: {new string(target)}");
            PrintChart(chart);
            Console.ReadLine();
        }

        public static int LevenshteinDistance(char[] m, char[] n)
        {
            //I originally thought "If the strings are equal length, just use Hamming Distance since it's more efficient"

            /*Consider 'mann' and 'annm', Hamming distance would be 3 since there are 3 char pairs that don't match,
              but Levenshtein would show only 2, since 'mann' -> 'ann' -> 'annm' */

            //if(m.Length == n.Length)
            //{
            //    return HammingDistance(m, n);
            //}

            //instantiate a new matrix [m+1, n+1]
            int[,] matrix = new int[m.Length + 1, n.Length + 1];
            //zero fill the matrix
            Array.Clear(matrix, 0, matrix.Length);

            //first column of the X axis compute the Distance of m from an empty string
            /* X   m a n n
             *   0 1 2 3 4
             * a -------->
             * n  
             * n  
             * m  
             */
            for (int i = 0; i < m.Length + 1; i++)
            {
                matrix[i, 0] = i;
            }

            //first column of the Y axis compute the Distance of n from an empty string
            /* X   m a n n
             *   0 1 2 3 4
             * a 1 |
             * n 2 |
             * n 3 |
             * m 4 V
             */
            for (int j = 0; j < n.Length + 1; j++)
            {
                matrix[0, j] = j;
            }

            for (int j = 1; j < n.Length + 1; j++)
            {
                //move accross and down rows 
                for (int i = 1; i < m.Length + 1; i++)
                {
                    int substitutionCost = 0;

                    if (m[i - 1] != n[j - 1])
                    {
                        substitutionCost = 1;
                    }
                    var min = (matrix[i - 1, j - 1] + substitutionCost); //substitution
                    min = min < matrix[i - 1, j] + 1 ? min : matrix[i - 1, j] + 1; //deletion
                    min = min < matrix[i, j - 1] + 1 ? min : matrix[i, j - 1] + 1; //insertion
                    matrix[i, j] = min;
                }
            }
            //This would look something like:
            /* X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |X   m a n n |
             *   0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |  0 1 2 3 4 |
             * a 1 1------>|a 1 1 1---->|a 1 1 1 2-->|a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |a 1 1 1 2 3 |
             * n 2         |n 2         |n 2         |n 2         |n 2 2------>|n 2 2 2---->|n 2 2 2 1-->|n 2 2 2 1 1 |n 2 2 2 1 1 |n 2 2 2 1 1 |n 2 2 2 1 1 |n 2 2 2 1 1 |n 2 2 2 1 1 |n 2 2 2 1 1 |n 2 2 2 1 1 |n 2 2 2 1 1 |
             * n 3         |n 3         |n 3         |n 3         |n 3         |n 3         |n 3         |n 3         |n 3 3------>|n 3 3 3---->|n 3 3 3 1-->|n 3 3 3 1 1 |n 3 3 3 1 1 |n 3 3 3 1 1 |n 3 3 3 1 1 |n 3 3 3 1 1 |
             * m 4         |m 4         |m 4         |m 4         |m 4         |m 4         |m 4         |m 4         |m 4         |m 4         |m 4         |m 4         |m 4 3------>|m 4 3 4---->|m 4 3 4 2-->|m 4 3 4 2 2 |
             */

            //Last value is what we're after!
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
        public static void PrintChart((char[], int)[] chart)
        {

            for (int i = 0; i < chart.Length; i++)
            {
                Console.WriteLine($"{new string(chart[i].Item1)}: {chart[i].Item2}");
            }
        }
      
    }
}
