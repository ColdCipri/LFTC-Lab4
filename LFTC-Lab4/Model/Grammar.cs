using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LFTC_Lab4.Model
{
    class Production
    {
        public string RuleName;
        public List<string> Rules;

        public Production(string ruleName, List<string> rules)
        {
            RuleName = ruleName;
            Rules = rules;
        }

        public List<string> GetRules()
        {
            return Rules;
        }

        public override string ToString()
        {
            string end = "\n\t" + RuleName + " -> ";
            foreach (var item in Rules)
            {
                end += item + " | ";
            }
            end = end.Substring(0, end.Length - 2);

            return end;
        }

        public bool HasNonTerminal(string elem)
        {
            return (RuleName == elem) ? true : false;
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
            List<char> usedLhs = new List<char>();
            foreach (var rule in rules)
            {
                var lhs = rule.Split("->")[0].Trim();
                var values = new List<string>(rule.Split("->")[1].Split('|').Select(i => i.Trim()));

                foreach (var value in values)
                {
                    if (!usedLhs.Contains(lhs[0]))
                    {
                        result.Add(new Production(lhs[0].ToString(), values));
                        usedLhs.Add(lhs[0]);
                    }
                }
            }
            return result;
        }

        public bool IsNonTerminal(string value)
        {
            return this.nonTerminals.Contains(value.ToString());
        }

        public bool IsTerminal(string value)
        {
            return this.terminals.Contains(value);
        }

        public List<Production> GetProductionsForNonTerminal(string nonTerminal)
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

        public static string cannonicalCollectionOfStates(List<Production> productions)
        {
            int s = 0, counterForValues = 0, counterForStates = 0;
            List<Pair<char, int>> usedStates = new List<Pair<char, int>>(); //create the list of used states
            usedStates.Add(new Pair<char, int>('s', counterForStates));     //we add s0 as the first state
            counterForStates++;                                             // cfs ++ => next time = s1

            List<Pair<string,string>> values = new List<Pair<string,string>>(); //create the list of pairs
            List<Pair<string, string>> valuesNew = new List<Pair<string, string>>(); //create a new list of pairs
            values.Add(new Pair<string, string>("S'", ".S"));                   //added first pair[0]: S' -> .S

            string result = usedStates[0].ToString2() + " = closure({[" + values[counterForValues].ToString() + "]}) = {[" + values[counterForValues].ToString() + "]";
            counterForValues++;                                                 //cfv++ => next is pair[1]

            foreach (var item in productions)   //we check in productions if there are other productions from S
                if (item.RuleName == productions[0].RuleName)       // check if production starts from S
                    foreach (var elem in item.Rules)                // adds all the rules from s to the collection
                    {
                        values.Add(new Pair<string, string>(item.RuleName, "." + elem)); // pair[cfv] : * -> *
                        result += ",[" + values[counterForValues] + "]";        //write the rule(production)
                        counterForValues++;                                     //cfv++ => 
                    }

            result += "}\n"; // we finished s0 so we have a new row
            s++;             // s++ so we got s1 next

            //while (ok)  //s1    = goto(                     s0  counter for states       ,      S' -> .S => S                       ) = closure({"
            
            result += "s" + s + " = goto(" + usedStates[--counterForStates].ToString2() + ", " + values[0].Value.Substring(1) + ") = closure({"; //we write s1 = goto(usedstate[cfs--], values[0].value.substr(1)
            foreach (var item in values)
            {
                if (item.Value[0] == '.')
                    foreach (var elem in productions)
                        if (elem.HasNonTerminal(item.Value.Substring(1)))
                        {
                            valuesNew.Add(new Pair<string, string>(item.Key, item.Value.Substring(1) + "."));//Add new closure to values;
                            result += "[" + valuesNew[0].ToString() + "]";
                            counterForValues++;                                                         //cfv++
                        }
                          

            }
            result += "} = {[" + valuesNew[0].ToString() + "]}\n";



            return result;
        }
    }
}
