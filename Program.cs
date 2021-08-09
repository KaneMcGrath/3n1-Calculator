using System;
using System.Collections;
using System.Collections.Generic;

namespace Calculator
{


    class Program
    {
        static void Main(string[] args)
        {

            bool exit = false;
            string input = "";


            while (!exit)
            {
                c.l();
                c.ww("3n+1 Calculator");
                c.w("Type a command: ");

                input = Console.ReadLine().ToLower();
                c.ww(input, ConsoleColor.Green);
                c.l();

                string[] command = input.ToLower().Split(' ');

                
                Int64 x = 0;

                if (command[0] == "range")
                {
                    bool silent = true;
                    foreach (string arg in command)
                    {
                        if (arg == "-silent")
                        {
                            silent = true;
                        }
                    }
                    Range(Int64.Parse(command[1]), Int64.Parse(command[2]), silent);

                }
                else if (command[0] == "crange")
                {
                    bool silent = true;
                    foreach (string arg in command)
                    {
                        if (arg == "-silent")
                        {
                            silent = true;
                        }
                    }
                    Crange(Int64.Parse(command[1]), Int64.Parse(command[2]), silent);

                }
                else if (command[0] == "srange")
                {

                    sRange(Int64.Parse(command[1]), Int64.Parse(command[2]));

                }
                else if (command[0] == "qrange")
                {
                    
                    sRange(Int64.Parse(command[1]), Int64.Parse(command[2]), true);

                }
                else if (command[0] == "calc" || command[0] == "find" || command[0] == "calculate")
                {

                    calc(Int64.Parse(command[1]));

                }
                else if (command[0] == "help")
                {
                    help();
                }
                else if (Int64.TryParse(input, out x))
                {
                    calc(x);
                }
                else
                {
                    c.e("Command not Recognised");
                }


            }

        }
        static void help()
        {
            c.ww("Available Commands");
            helpCommand("help", new string[0], new string[0]);
            helpCommand("range", new string[] { "min", "max" }, new string[] { "-silent" });
            helpCommand("calc", new string[] { "value" }, new string[0]);
        }
        static void helpCommand(string commandName, string[] args, string[] options)
        {
            c.w(commandName, ConsoleColor.Green);
            c.w(" ");
            foreach (string a in args)
            {
                c.w("<");
                c.w(a);
                c.w(">");
            }
            c.w(" ");
            foreach (string o in options)
            {
                c.w("[");
                c.w(o);
                c.w("]");
            }
            c.l();
        }
        static void calc(Int64 input)
        {

            Int64 result = input;
            Int64 count = 0;
            Int64 highest = 0;
            c.w(result.ToString(), ConsoleColor.Yellow);
            c.w(",");
            while (result > 1)
            {
                result = Conjecture.calc(result);
                if (result > highest) highest = result;
                c.w(result.ToString(), ConsoleColor.Yellow);
                c.w(",");
                if (result < 0)
                {
                    c.e("Negative number " + result + " found after " + count + " steps.");
                    c.e("Likely after reaching max int of " + Int64.MaxValue.ToString());
                }
                count++;
            }

            c.l();

            c.w(input.ToString(), ConsoleColor.Green);
            c.w(" - steps[ ");
            c.w(count.ToString(), ConsoleColor.Yellow);
            c.w(" ] - highest[ ");
            c.w(highest.ToString(), ConsoleColor.Yellow);
            c.w(" ]");
            c.l();
        }

        static void Range(Int64 low, Int64 high, bool silent = false)
        {
            Int64 startTime = DateTime.Now.Ticks;
            Int64[] highestStep = { 0, 0 };
            Int64[] Int64estStep = { 0, 0 };

            for (Int64 i = low; i <= high; i++)
            {
                Int64 result = i;
                Int64 count = 0;
                Int64 highest = 0;

                while (result > 1)
                {
                    result = Conjecture.calc(result);
                    if (result > highest) highest = result;
                    if (result < 0)
                    {
                        c.e("Negative number " + result + " found after " + count + " steps.");
                    }
                    count++;
                }
                if (highest > highestStep[0])
                {
                    highestStep[0] = highest;
                    highestStep[1] = i;
                }
                if (count > Int64estStep[0])
                {
                    Int64estStep[0] = count;
                    Int64estStep[1] = i;
                }

                if (!silent)
                {
                    c.w(i.ToString(), ConsoleColor.Green);
                    c.w(" - steps[ ");
                    c.w(count.ToString(), ConsoleColor.Yellow);
                    c.w(" ] - highest[ ");
                    c.w(highest.ToString(), ConsoleColor.Yellow);
                    c.w(" ]");
                    c.l();
                }
            }
            Int64 endTime = DateTime.Now.Ticks;
            Int64 elapsedTime = startTime - endTime;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTime);
            c.l();
            c.w("Finished ");
            c.w((high - low).ToString(), ConsoleColor.Yellow);
            c.ww(" numbers");
            c.w("Time elapsed ");
            c.ww(elapsedSpan.ToString(), ConsoleColor.Yellow);
            

            c.w("Int64est Step Count - [ ");
            c.w(Int64estStep[1].ToString(), ConsoleColor.Green);
            c.w(" ] with ");
            c.w(Int64estStep[0].ToString(), ConsoleColor.Yellow);
            c.ww(" steps!");

            c.w("Largest number - [ ");
            c.w(highestStep[1].ToString(), ConsoleColor.Green);
            c.w(" ] with ");
            c.w(highestStep[0].ToString(), ConsoleColor.Yellow);
            c.ww(" as its highest number!");

        }

        static void Crange(Int64 low, Int64 high, bool silent = false)
        {
            Int64 startTime = DateTime.Now.Ticks;
            for (Int64 i = low; i <= high; i++)
            {
                if (Conjecture.doesConverge(i))
                {
                    if (!silent)
                    {
                        c.w(i.ToString(), ConsoleColor.Green);
                        c.l();
                    }
                }
            }
            Int64 endTime = DateTime.Now.Ticks;
            Int64 elapsedTime = startTime - endTime;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTime);
            c.l();
            c.w("Finished ", ConsoleColor.Yellow);
            c.w((high - low).ToString(), ConsoleColor.Yellow);
            c.ww(" numbers");
            c.w("Time elapsed ");
            c.ww(elapsedSpan.ToString(), ConsoleColor.Yellow);

        }

        static void sRange(Int64 low, Int64 high, bool quiet = false)
        {
            Int64[] Int64estStep = { 0, 0 };
            Int64 startTime = DateTime.Now.Ticks;
            if (quiet)
            {
                for (Int64 i = low; i <= high; i++)
                {
                    Int64 count = Conjecture.quietSteps(i);
                    if (count > Int64estStep[0])
                    {
                        Int64estStep[0] = count;
                        Int64estStep[1] = i;
                    }
                }
            }
            else
            {
                for (Int64 i = low; i <= high; i++)
                {
                    Int64 count = Conjecture.steps(i);
                    if (count > Int64estStep[0])
                    {
                        Int64estStep[0] = count;
                        Int64estStep[1] = i;
                    }
                }

            }
            Int64 endTime = DateTime.Now.Ticks;
            Int64 elapsedTime = startTime - endTime;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTime);
            c.l();
            
            c.w("Finished ", ConsoleColor.Yellow);
            c.w((high - low).ToString(), ConsoleColor.Yellow);
            c.ww(" numbers");
            c.w("Time elapsed ");
            c.ww(elapsedSpan.ToString(), ConsoleColor.Yellow);
            c.w("Int64est Step Count - [ ");
            c.w(Int64estStep[1].ToString(), ConsoleColor.Green);
            c.w(" ] with ");
            c.w(Int64estStep[0].ToString(), ConsoleColor.Yellow);
            c.ww(" steps!");
        }
    }

    public static class Conjecture
    {
        public static HashSet<Int64> cache = new HashSet<Int64>();
        public static Hashtable stepCache = new Hashtable();

        public static Int64 steps(Int64 input)
        {
            c.l();
            c.ww(input.ToString(), ConsoleColor.Yellow);
            Int64 result = input;
            Int64 count = 0;
            while (result > 1)
            {
                c.w(count.ToString(),ConsoleColor.Green);
                c.w("[", ConsoleColor.Magenta);
                c.w(result.ToString(), ConsoleColor.Yellow);
                c.w("],", ConsoleColor.Magenta);

                if (stepCache.ContainsKey(result))
                {
                    c.l();
                    c.w("Found number ", ConsoleColor.Magenta);
                    c.w(result.ToString(), ConsoleColor.Cyan);
                    c.w(" in cache.  Added ", ConsoleColor.Magenta);
                    c.w(stepCache[result].ToString(), ConsoleColor.Cyan);


                    count += (Int64)stepCache[result];


                    c.w(" for ", ConsoleColor.Magenta);
                    c.w(count.ToString(), ConsoleColor.Yellow);
                    c.w(" total steps", ConsoleColor.Magenta);
                    
                    break;
                }
                result = Conjecture.calc(result);
                count++;
                if (result < 0)
                {
                    c.e("Negative number " + result + " found");
                    c.e("Likely after reaching max int of " + Int64.MaxValue.ToString());
                    return 0;
                }
                
            }

            c.l();
            if (!stepCache.ContainsKey(input))
            {
                c.w("Adding ", ConsoleColor.Magenta);
                c.w(input.ToString(),ConsoleColor.Cyan);
                c.w(" to cache at ",ConsoleColor.Magenta);
                c.ww(count.ToString(), ConsoleColor.Cyan);
                stepCache.Add(input, count);
            }
            return count;
        }

        public static Int64 quietSteps(Int64 input)
        {
            Int64 result = input;
            Int64 count = 0;
            while (result > 1)
            {
                

                if (stepCache.ContainsKey(result))
                {
                   


                    count += (Int64)stepCache[result];


                   

                    break;
                }
                result = Conjecture.calc(result);
                count++;
                if (result < 0)
                {
                    c.e("Negative number " + result + " found");
                    c.e("Likely after reaching max int of " + Int64.MaxValue.ToString());
                    return 0;
                }

            }

            
            if (!stepCache.ContainsKey(input))
            {
                stepCache.Add(input, count);
            }
            return count;
        }

        public static bool doesConverge(Int64 input)
        {
            Int64 result = input;
            while (result > 1)
            {
                if (cache.Contains(result))
                {
                    
                    break;
                }
                result = Conjecture.calc(result);
                

                
                if (result < 0)
                {
                    c.e("Negative number " + result + " found");
                    c.e("Likely after reaching max int of " + Int64.MaxValue.ToString());
                    return false;
                }
                
            }
            cache.Add(input);
            return true;
        }
        public static Int64 calc(Int64 input)
        {
            
            return (input % 2 == 0) ? input / 2 : (3 * input) + 1;
        }

    }

    

   
    /// <summary>
    /// Simplified Console
    /// </summary>
    public static class c
    {
        public static ConsoleColor foreground = ConsoleColor.White;
        public static ConsoleColor Background = ConsoleColor.Black;
        
        

        public static void w(string s)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = Background;
            Console.Write(s);
        }
        public static void ww(string s)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = Background;
            Console.WriteLine(s);
        }
        public static void w(string s, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.BackgroundColor = Background;
            Console.Write(s);
        }
        public static void ww(string s, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.BackgroundColor = Background;
            Console.WriteLine(s);
        }

        public static void l()
        {
            Console.WriteLine();
        }
        public static void l(int lines)
        {
            for (int i = 0; i < lines; i++)
                Console.WriteLine();
        }

        public static void e(string s)
        {
            l();
            ww(s, ConsoleColor.Red);
            l();
        }

        public static void er(string s)
        {
            ww(s, ConsoleColor.Red);
            
        }

        public static string r()
        {
            return Console.ReadLine();
        }
    }
}
