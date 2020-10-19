using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using yTest.Models;

namespace yTest
{
    class Program
    {

        static List<Pitchers> LoadFanGraphPitchers(List<Pitchers> pitchers, string filename)
        {

            try
            {
                using (var sr = new StreamReader(filename))
                {

                    double tempERA, tempKO9, tempGBP, tempBB9, tempFBP;
                    double tempIP = 0.00d;
                    int tempFanId;
                    var title = sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var items = line.Split(',');

                        double.TryParse(items[2], out tempERA);
                        double.TryParse(items[3], out tempIP);
                        tempIP = Math.Round((tempIP / 3), 2);
                        double.TryParse(items[4], out tempKO9);
                        items[5] = items[5].Trim('%');
                        double.TryParse(items[5], out tempGBP);
                        double.TryParse(items[6], out tempBB9);
                        items[7] = items[7].Trim('%');
                        double.TryParse(items[7], out tempFBP);
                        int.TryParse(items[8], out tempFanId);


                        pitchers.Add(new Pitchers
                        {
                            PitcherName = items[0],
                            TeamName = items[1],
                            VarEra = tempERA,
                            VarIps = tempIP,
                            VarKo9 = tempKO9,
                            VarGbp = tempGBP,
                            VarBb9 = tempBB9,
                            VarFb9 = tempFBP,
                            VarFanId = tempFanId
                        });

                        Console.Write($"Item added to Pitcher list: ");
                        PrintPitcher(pitchers.Last());
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"\n\nCannot open file {filename}.\n{ex}\n\n");
            }

            return pitchers;

        }


        static void PrintPitcher(Pitchers p)
        {
            Console.WriteLine($"{p.VarFanId,10} {p.PitcherName,-20}" +
                $" {p.VarIps,-5:0.0}  {p.VarGbp,-5}");
        }


        static List<Pitchers> RemoveGBP(List<Pitchers> pitchers)
        {
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{pitchers.RemoveAll(x => x.VarGbp <= 0)} " +
                $"pitchers removed due to missing GBP.");
            Console.ResetColor();
            Console.WriteLine("\n");
            return pitchers;
        }

        static void InsertPitchersTable(List<Pitchers> pitchers)
        {
            using (var context = new BBStats3Context())
            {
                pitchers.ForEach(x => context.Pitchers.Add(new Pitchers
                {
                    PitcherName = x.PitcherName,
                    TeamName = x.TeamName,
                    VarEra = x.VarEra,
                    VarIps = x.VarIps,
                    VarKo9 = x.VarKo9,
                    VarGbp = x.VarGbp,
                    VarBb9 = x.VarBb9,
                    VarFb9 = x.VarFb9,
                    VarFanId = x.VarFanId
                }));

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n\n{context.Pitchers.Count()} added to Pitchers Table.\n\n");
                Console.ResetColor();
                context.SaveChanges();
            }
        }


        static void InsertPitcherBuffTableGB()
        {
            try
            {
                using (var context = new BBStats3Context())
                {
                    var pitchers = context.Pitchers.ToList();

                    List<Pitchers> gmEligible = pitchers.FindAll(x => x.VarGbp >= 55.0);
                    gmEligible.ForEach(x => Console.WriteLine($"{x.PitcherName} " +
                        $"is eligible for Groundball Machine buff." ));
                    gmEligible.ForEach(x => context.PitcherBuff.Add(new PitcherBuff
                    {
                        PitcherId = x.PitcherId,
                        BuffId = 2
                    }));


                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting into pitcher buff table GB\n{ex}");
            }
        }


        static void InsertPitcherBuffTableSA()
        {
            try
            {
                using (var context = new BBStats3Context())
                {
                    var pitchers = context.Pitchers.ToList();

                    List<Pitchers> saEligible = pitchers.FindAll(x => x.VarKo9 > 8);
                    saEligible.ForEach(x => Console.WriteLine($"{x.PitcherId} {x.VarKo9}"));
                    saEligible.ForEach(x => context.PitcherBuff.Add(new PitcherBuff
                    {
                        PitcherId = x.PitcherId,
                        BuffId = 1
                    }));

                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load record.\n{ex}\n\n");

            }

        }


        static void Main(string[] args)
        {

            List<Pitchers> pitchers = new List<Pitchers>();
            string fileName =
                @"D:\Source\Repos\GitTest\yTest\bin\Debug\netcoreapp3.1\pitchers_1999_2002.csv";

            //LoadFanGraphPitchers(pitchers, fileName);
            //RemoveGBP(pitchers);
            //InsertPitchersTable(pitchers);

            //pitchers.ForEach(x => Console.WriteLine($"{x.PitcherId}"));

            //InsertPitcherBuffTableSA();

            //InsertPitcherBuffTableGB();


        }
    }
}
