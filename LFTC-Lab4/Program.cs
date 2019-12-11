using LFTC_Lab4.Model;
using System;

namespace LFTC_Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Grammar");
            var grammar = Grammar.FromFile("seminar.txt");
            Console.WriteLine(grammar.ToString());
        }
    }
}
