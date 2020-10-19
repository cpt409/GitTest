using System;
using System.Collections.Generic;
using xTest.Models;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.ComponentModel;

namespace xTest
{
    class Program
    {
        static void LoadStarters(List<Starters> starters, string filename)
        {
            try
            {
                using (var sr = new StreamReader(filename))
                {

                    var title = sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var items = line.Split(',');

                        double tempERA;
                        double.TryParse(items[2], out tempERA);
                        double tempIPS;
                        double.TryParse(items[3], out tempIPS);
                        double tempKO9;
                        double.TryParse(items[4], out tempKO9);
                        double tempBB9;
                        double.TryParse(items[5], out tempBB9);
                        int tempFIP;
                        int.TryParse(items[6], out tempFIP);
                        int tempFanID;
                        int.TryParse(items[7], out tempFanID);

                        starters.Add(new Starters
                        {
                            StarterName = items[0],
                            TeamName = items[1],
                            VarEra = tempERA,
                            VarIps = tempIPS,
                            VarKo9 = tempKO9,
                            VarBb9 = tempBB9,
                            VarFip = tempFIP,
                            FanId = tempFanID
                        });

                        Console.WriteLine($"{starters.Last().StarterName} added to Starters List.");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not open file: {filename}");
                Console.WriteLine($"{ex}");
            }

            PrintSystemInfo($"Total Starters added to Starter List: {starters.Count}");
        }

        static void PrintSystemInfo(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{msg}\n");
            Console.ResetColor();
        }

        static void InsertStartersToDB(List<Starters> starters)
        {
            try
            {
                using (var context = new BBStats4Context())
                {
                    starters.ForEach(x => context.Starters.Add(new Starters
                    {
                        StarterName = x.StarterName,
                        TeamName = x.TeamName,
                        VarEra = x.VarEra,
                        VarIps = x.VarIps,
                        VarKo9 = x.VarKo9,
                        VarBb9 = x.VarBb9,
                        VarFip = x.VarFip,
                        FanId = x.FanId
                    }));

                    context.SaveChanges();
                    PrintSystemInfo($"{context.Starters.Count()} " +
                        $"Starters added to the Starter Table.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open context.");
                Console.WriteLine($"{ex}");
            }
        }



        static void Main(string[] args)
        {
            string fileStarters2000 =
                @"D:\Source\Repos\GitTest\xTest\bin\Debug\netcoreapp3.1\Starters_2000.csv";


            List<Starters> starters = new List<Starters>();
          //  LoadStarters(starters, fileStarters2000);
          //  InsertStartersToDB(starters);

        }
    }
}
