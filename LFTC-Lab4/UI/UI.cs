using LFTC_Lab4.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LFTC_Lab4.UI
{
    public static class UI
    {
        public static void printMenu()
        {
            var grammar = Grammar.FromFile("seminar.txt");
            var ok = true;
            string seq = "There is no sequence for the moment";

            while (ok)
            {
                int input = 0;

                while (true)
                {
                    try
                    {
                        Console.WriteLine("0 - \t Exit");
                        Console.WriteLine("1 - \t Show the CFG");
                        Console.WriteLine("2 - \t Input the sequence");
                        Console.WriteLine("3 - \t Show the sequence");
                        input = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("You must input a number between 0 and 1");
                    }
                }



                switch (input)
                {
                    case 0:
                        ok = false;
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine(grammar.ToString());
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Please input the sequence:");
                        seq = Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine(seq);
                        break;
                    default:
                        Console.WriteLine("The input must be 0 / 1");
                        break;
                }
            }
            
        }

    }
}
