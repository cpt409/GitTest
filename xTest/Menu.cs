using System;
using System.Collections.Generic;
using System.Text;

namespace xTest
{
    static class Menu
    {
        static public void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write($"\tWelcome to the BB Stat Machine");
            Console.ResetColor();
            Console.ResetColor();
            Console.WriteLine("\n");
            Console.WriteLine("\t8) Print All Starters");
            Console.WriteLine("\t9) Exit Application");
            Console.WriteLine();
        }

        static public void PrintErrorMessage(string msg)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{msg}");
            Console.ResetColor();
            Console.WriteLine();
        }

        static public void PrintSystemRequest(string msg)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{msg}");
            Console.ResetColor();
        }
        static public void PrintSystemMessage(string msg)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{msg}");
            Console.ResetColor();
            Console.WriteLine();
        }

        static public int GetIntSelection()
        {
            int selection = 0;
            string input = string.Empty;
            do
            {
                PrintSystemRequest("Enter your selection: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out selection));

            return selection;
        }
    }
}
