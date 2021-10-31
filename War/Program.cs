using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    class Program
    {
        // Program flags

        /// <summary>
        /// The version number of the war.exe program
        /// </summary>
        public static string versionNumber = "v0.1.0.1";

        /// <summary>
        /// Do not wait for keystrokes
        /// </summary>
        public static bool runToEnd = false;

        /// <summary>
        /// Show some program help from the command line
        /// </summary>
        static void ShowHelp()
        {
            Console.WriteLine(
                String.Format("War {0}\n", versionNumber) +
                "Usage: war.exe [--runtoend] [--help]\n\n" +
                "--runtoend        run without pausing for keystrokes or taunts,\n" +
                "                  for testing purposes only\n" +
                "--help, -help, /? show this help screen\n\n"
                );
        }

        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">
        /// Allows some basic arguments
        /// --runToEnd
        /// --help
        /// </param>
        static void Main(string[] args)
        {
            // Look for the runtoend argument
            runToEnd =
                args.Contains("--runtoend", StringComparer.InvariantCultureIgnoreCase) ||
                args.Contains("/runtoend", StringComparer.InvariantCultureIgnoreCase);

            // Look for the help argument
            if (
                args.Contains("--help", StringComparer.InvariantCultureIgnoreCase) ||
                args.Contains("-help", StringComparer.InvariantCultureIgnoreCase) ||
                args.Contains("/help", StringComparer.InvariantCultureIgnoreCase) ||
                args.Contains("/?", StringComparer.InvariantCultureIgnoreCase)
                )

                ShowHelp();
            else
            {
                // "Do you want to play a game?"
                PlayCycle p = new PlayCycle() { playerName = GetPlayerName() };
                p.BeginPlay();
            }
        }

        /// <summary>
        /// Retrieve the player name through user input
        /// </summary>
        /// <returns>The player name they want</returns>
        private static string GetPlayerName()
        {
            string ret = "";
            while (ret.Length == 0)
            {
                Console.WriteLine("What is your name, Player 1?");
                Console.Write(">");
                string line = Console.ReadLine();
                line = line.Length == 0 ? "Player 1" : line.Trim();

                Console.WriteLine(String.Format("Is this right [y/n]? \"{0}\"", line));
                string key;
                do
                {
                    // Read a key from the console, supressing echo to the screen.
                    key = Console.ReadKey(true).KeyChar.ToString().ToLower();
                } while (key != "y" && key != "n");

                if (key == "y")
                    ret = line;
            }

            return ret;
        }
    }
}
