using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

/*Created by:  David Chapman
 * CSCI 5330 Algorithms Analysis and Design - Randomized select and Monte-Carlo for integration
 * 2/15/2015
 * Revisions:
 * 
 * 
 * <Summary>
 * This application demonstrates the randomized select method and test the accuracy of the
 * the Monte-Carlo method for integration using sin^2.
 * <Summary>
 * 
 * 
 * */

namespace DChapman_Assignment_2
{
    class Program
    {
        //Swap method used in rpart
        public static void swap(int[] a, int p, int i)
        {
            int temp = a[i];
            a[i] = a[p];
            a[p] = temp;
            
        }
        //Randomized partition method
        public static int rpart(int[] a, int p, int r)
        {
            Random Rnd = new Random();
            int i = Rnd.Next(p, r);
            swap(a,p,i);
            return partition(a, p, r);

        }
        //Randomized select method
        public static int randSelect(int[] a, int p, int r, int i)
        {
            if (p == r) return a[p];
            int q = rpart(a, p, r);
            int k = q - p + 1;
            if (i == k) return a[q];
            else if (i < k) return randSelect(a, p, q - 1, i);
            else return randSelect(a, q + 1, r, i - k);
        }
        //Partition used for Randomized select and Quick sort methods
        public static int partition(int[] a, int left, int right)
        {
            int pivot = a[left];
            while (true)
            {
                while (a[left] < pivot)
                    left++;
                while (a[right] > pivot)
                    right--;
                //This code added for duplicate integers
                if (a[right] == pivot && a[left] == pivot) left++;

                if (left < right)
                {
                    int temp = a[right];
                    a[right] = a[left];
                    a[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
        //Quick Sort Method
        public static void quickSort(int[] a, int left, int right)
        {
            //quickSort does not return a long because of recursion
            if (left < right)
            {
                int pivot = partition(a, left, right);
                if (pivot > 1) quickSort(a, left, pivot - 1);

                if (pivot + 1 < right) quickSort(a, pivot + 1, right);
            }

        }

        //Monte-Carlo method for sin^2
        public static double MonteCarlo(int n)
        {
            double x = 0;
            double f;
            
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                double r = rnd.NextDouble();
                f = Math.Pow(Math.Sin(r), 2);
                x += f;
                
            }
            return (x / n);
        }
        static void Main(string[] args)
        {
            Console.Title = "Algorithms HW2:  Rand-Select and Monte-Carlo";
            Console.WindowHeight = 30;
            Console.WindowWidth = 120;

            //Randomized Select example:
            Random rand = new Random();
            Stopwatch timer = new Stopwatch();
            //Change the size of the arrays here:
            int a1 = 10;
            int a2 = 10000;
            int a3 = 50000;
            int a4 = 100000;
            int a5 = 250000;

            //Build arrays for testing:
            //int[] testArray = new int[100];
            int[] qArray1 = new int[a1];
            int[] qArray2 = new int[a2];
            int[] qArray3 = new int[a3];
            int[] qArray4 = new int[a4];
            int[] qArray5 = new int[a5];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*****************************************************************************************************", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*****************************************************************************************************", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*                                                                                                   *", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*     This program is testing the speed (in milliseconds) of the randomized  select algorithm       *", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*                                                                                                   *", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*****************************************************************************************************", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*****************************************************************************************************", " ", " ", " "));
           
            //Add random integers between 100 and 10000 to each array:
            for (int i = 0; i < qArray1.Length; i++) qArray1[i] = rand.Next(100, 10000);
            for (int i = 0; i < qArray2.Length; i++) qArray2[i] = rand.Next(100, 10000);
            for (int i = 0; i < qArray3.Length; i++) qArray3[i] = rand.Next(100, 10000);
            for (int i = 0; i < qArray4.Length; i++) qArray4[i] = rand.Next(100, 10000);
            for (int i = 0; i < qArray5.Length; i++) qArray5[i] = rand.Next(100, 10000);

            //test used for small array (Array1):
            int test = rand.Next(9);
            //ith determines what element we will find in the array:
            int ith = rand.Next(10, 1000);

            //Run ranSelect on each array and record time:
            timer.Restart();
            int q1 = randSelect(qArray1, 0, qArray1.Length - 1, test);
            long randSelTime1 = timer.ElapsedMilliseconds;
            timer.Reset();
            
            timer.Restart();
            int q2 = randSelect(qArray2, 0, qArray2.Length - 1, ith);
            long randSelTime2 = timer.ElapsedMilliseconds;
            timer.Reset();
            
            timer.Restart();
            int q3 = randSelect(qArray3, 0, qArray3.Length - 1, ith);
            long randSelTime3 = timer.ElapsedMilliseconds;
            timer.Reset();
            
            timer.Restart();
            int q4 = randSelect(qArray4, 0, qArray4.Length - 1, ith);
            long randSelTime4 = timer.ElapsedMilliseconds;
            timer.Reset();
            
            timer.Restart();
            int q5 = randSelect(qArray5, 0, qArray5.Length - 1, ith);
            long randSelTime5 = timer.ElapsedMilliseconds;
            timer.Reset();
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();

            //List elements in small array (Array1) for testing:
            Console.Write("Array1 elements:  ");
            for (int i = 0; i < qArray1.Length; i++)
            {
                Console.Write(qArray1[i] + "  ");
            }
            Console.WriteLine();

            //Show the selected element in each array:
            Console.WriteLine("The {0} element in Array1 is {1}.", test, q1);
            Console.WriteLine();
            Console.WriteLine("The {0} element in Array2 is {1}.", ith, q2);
            Console.WriteLine("The {0} element in Array3 is {1}.", ith, q3);
            Console.WriteLine("The {0} element in Array4 is {1}.", ith, q4);
            Console.WriteLine("The {0} element in Array5 is {1}.", ith, q5);
            Console.WriteLine();

            //Show results:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(String.Format("{0,25} | {1,15} | {2,15} | {3,15} | {4,15} | {5,15}", "", "Array1(" + qArray1.Length + ")", "Array2(" + qArray2.Length + ")", "Array3(" + qArray3.Length + ")", "Array4(" + qArray4.Length + ")", "Array5(" + qArray5.Length + ")"));
            Console.WriteLine(String.Format("{0,25} | {1,15} | {2,15} | {3,15} | {4,15} | {5,15}","Time for Random Select:", randSelTime1, randSelTime2, randSelTime3, randSelTime4, randSelTime5 ));

            //Quick Sort Examples: (for comparison)
            timer.Restart();
            quickSort(qArray1, 0, qArray1.Length - 1);
            long quickTime1 = timer.ElapsedMilliseconds;
            timer.Reset();

            timer.Restart();
            quickSort(qArray2, 0, qArray2.Length - 1);
            long quickTime2 = timer.ElapsedMilliseconds;
            timer.Reset();

            timer.Restart();
            quickSort(qArray3, 0, qArray3.Length - 1);
            long quickTime3 = timer.ElapsedMilliseconds;
            timer.Reset();

            timer.Restart();
            quickSort(qArray4, 0, qArray4.Length - 1);
            long quickTime4 = timer.ElapsedMilliseconds;
            timer.Reset();

            timer.Restart();
            quickSort(qArray5, 0, qArray5.Length - 1);
            long quickTime5 = timer.ElapsedMilliseconds;
            timer.Reset();
            long totQTime = quickTime1 + quickTime2 + quickTime3 + quickTime4 + quickTime5;
            Console.WriteLine(String.Format("{0,25} | {1,15} | {2,15} | {3,15} | {4,15} | {5,15} ", "Time for Quick Sort:", quickTime1, quickTime2, quickTime3, quickTime4, quickTime5));
            
            /* Code used to compare Random Select of unsorted versus sorted arrays:
            timer.Restart();
            q1 = randSelect(qArray1, 0, qArray1.Length - 1, test);
            randSelTime1 = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Restart();
            q2 = randSelect(qArray2, 0, qArray2.Length - 1, ith);
            randSelTime2 = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Restart();
            q3 = randSelect(qArray3, 0, qArray3.Length - 1, ith);
            randSelTime3 = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Restart();
            q4 = randSelect(qArray4, 0, qArray4.Length - 1, ith);
            randSelTime4 = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Restart();
            q5 = randSelect(qArray5, 0, qArray5.Length - 1, ith);
            randSelTime5 = timer.ElapsedMilliseconds;
            timer.Reset();
            Console.WriteLine(String.Format("{0,25} | {1,15} | {2,15} | {3,15} | {4,15} | {5,15}", "Time for Random Select (sorted arrays):", randSelTime1, randSelTime2, randSelTime3, randSelTime4, randSelTime5));
            */
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue.");
            Console.ReadKey();

            //Monte-Carlo Example:

            //Integral from 0 to 1 of sin^2 as produced on wolfram alpha:
            const double precise = 0.2726756432935795761509950;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*****************************************************************************************************", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*****************************************************************************************************", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*                                                                                                   *", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*       This program is testing the accuracy of the Monte-Carlo algortithm for Sin^2                *", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*                                                                                                   *", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*****************************************************************************************************", " ", " ", " "));
            Console.WriteLine(String.Format("{0,0} {1,15} {2,15} {3,15} {4,15}", " ", "*****************************************************************************************************", " ", " ", " "));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(String.Format("{0,15} | {1,25} | {2,40}", "N", "Monte-Carlo Approximation", "Error"));

            /*Run MonteCarlo for 5 iterations where N is multiplyed by 10 each time.
             * Each iteration, the result is compared to the constant precise.
             * */
            int value = 1000;
            for (int i = 0; i <= 4; i++)
            {
                Console.WriteLine(String.Format("{0,15} | {1,25} | {2,40}", value, MonteCarlo(value), Math.Abs(MonteCarlo(value) - precise)));
                value *= 10;
            }
            Console.ReadKey();
        }
    }
}
