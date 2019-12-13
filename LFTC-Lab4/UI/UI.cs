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
                        Console.WriteLine("\n0 - \t Exit");
                        Console.WriteLine("1 - \t Show the CFG");
                        Console.WriteLine("2 - \t Cannonical collection of states of CFG");
                        Console.WriteLine("3 - \t Input the sequence");
                        Console.WriteLine("4 - \t Show the sequence");
                        input = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("You must input a number between 0 and 1");
                    }
                }

                //THEORY.
                //S' -> .S starting point.                                                                                          }
                //if we have a variable we have to add all of the productions of that variable. (in this case S -> .aA and S -> .E) } = closure of s0
                //we add A -> .bA                                                                                                   }
                //
                //later, for example if we have S -> .AA and we have A -> .aA, we write in the same state S and A. After we shift (S -> A.A) we write again A -> .aA because we have(.A)
                //
                //whenever a dot is in the right most side then that is a final item/state

                // https://www.youtube.com/watch?v=APJ_Eh60Qwo


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
                        Console.WriteLine(Grammar.cannonicalCollectionOfStates(grammar.productions));
                        break;
                    case 3:
                        //WE need to add S' -> S to production
                        Console.Clear();
                        Console.WriteLine("Please input the sequence:");
                        Console.Write("w = ");
                        seq = Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("w = " + seq);
                        break;
                    default:
                        Console.WriteLine("The input must be 0 / 1");
                        break;
                }
            }
            
        }

    }
}
