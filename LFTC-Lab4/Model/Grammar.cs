using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LFTC_Lab4.Model
{
    class Production
    {
        public char RuleName;
        public string Rule;

        public Production(char ruleName, string rule)
        {
            RuleName = ruleName;
            Rule = rule;
        }
        public override string ToString()
        {
            return "\n\t" + RuleName + " -> " + Rule;
        }
    }
    class Grammar
    {
        public List<string> nonTerminals;
        public List<string> terminals;
        public string startSymbol;
        public List<Production> productions;

        public Grammar(List<string> n, List<string> e, string s, List<Production> p)
        {
            this.nonTerminals = n;
            this.terminals = e;
            this.startSymbol = s;
            this.productions = p;
        }

        public static List<string> ParseLine(string line)
        {
            return new List<string>(line.Trim().Split('=')[1].Trim()[1..^1].Trim().Split(',').Select(i => i.Trim()));

        }

        public static Grammar FromFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                var N = Grammar.ParseLine(file.ReadLine());

                foreach (string elem in N)
                {
                    if (elem.ToUpper() != elem)
                    {
                        throw new Exception("There is a terminal in the set of non terminals");
                    }
                }
                var E = Grammar.ParseLine(file.ReadLine());

                foreach (string elem in E)
                {
                    if (elem.ToLower() != elem)
                    {
                        throw new Exception("There is a non-terminal in the set of terminals");
                    }
                }
                var S = file.ReadLine().Split('=')[1].Trim();
                List<string> lines = new List<string>();
                string line = string.Empty;
                while ((line = file.ReadLine()) != null)
                    lines.Add(line);
                var P = Grammar.ParseRules(Grammar.ParseLine(string.Join("", lines)));
                return new Grammar(N, E, S, P);
            }

        }

        public static List<Production> ParseRules(List<string> rules)
        {
            List<Production> result = new List<Production>();
            foreach (var rule in rules)
            {
                var lhs = rule.Split("->")[0].Trim();
                var values = new List<string>(rule.Split("->")[1].Split('|').Select(i => i.Trim()));
                foreach (var value in values)
                    result.Add(new Production(lhs[0], value));

            }
            return result;
        }

        public bool IsNonTerminal(char value)
        {
            return this.nonTerminals.Contains(value.ToString());
        }

        public bool IsTerminal(string value)
        {
            return this.terminals.Contains(value);
        }

        public List<Production> GetProductionsForNonTerminal(char nonTerminal)
        {
            if (!IsNonTerminal(nonTerminal))
                throw new Exception("There is no such non terminal as: " + nonTerminal + "\n");
            List<Production> productionsForNT = new List<Production>();
            return productionsForNT.Where(prod => prod.RuleName == nonTerminal).ToList();
        }

        public override string ToString()
        {
            return "N = { " + string.Join(",", nonTerminals) + " }\n"
               + "E = { " + string.Join(",", terminals) + " }\n"
               + "P = { " + string.Join(",", productions.Select(prod => prod.ToString())) + "\n}\n"
               + "S = " + startSymbol + "\n";
        }
    }
}
