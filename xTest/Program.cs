using System;
using System.Collections.Generic;
using xTest.Models;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Threading;

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

        static List<Starters> GetStartersFromDB()
        {
            using (var context = new BBStats4Context())
            {
                return context.Starters.ToList();
            }
        }

        static void PrintStarters(List<Starters> starters)
        {
            starters = starters.OrderByDescending(x => x.VarIps).ToList();
            starters.ForEach(x => Console.WriteLine($"{x.StarterId,3} {x.StarterName,-22}"));
        }

        static void FirstInsertIntoStarterBuff()
        {
            using (var context = new BBStats4Context())
            {
                // strikeout artist = ko9 > 8.5;
                // groundball machine = fip < 80
                // control = bb9 < 2.0
                // stamina = ip > 227

                var starters = context.Starters.ToList();


                var tempSAList = starters.FindAll(x => x.VarKo9 > 8.5);
                tempSAList.ForEach(x => context.StarterBuff.Add(new StarterBuff
                {
                    StarterId = x.StarterId,
                    FanId = x.FanId,
                    BuffId = 1,

                }));

                Menu.PrintSystemMessage("\nStrikeout Artist (SA+) " +
                    "qualified Starters inserted into StarterBuff table.");


                var tempGB = starters.FindAll(x => x.VarFip < 80);
                tempGB.ForEach(x => context.StarterBuff.Add(new StarterBuff
                {
                    StarterId = x.StarterId,
                    FanId = x.FanId,
                    BuffId = 2
                }));

                Menu.PrintSystemMessage("\nGroundball Machine (GB+)" +
                    " qualified Starters inserted into StarterBuff table.");


                var tempCtrl = starters.FindAll(x => x.VarBb9 < 2.0);
                tempCtrl.ForEach(x => context.StarterBuff.Add(new StarterBuff
                {
                    StarterId = x.StarterId,
                    FanId = x.FanId,
                    BuffId = 3
                }));

                Menu.PrintSystemMessage("\nControl Pitcher (CN+)" +
                    " qualified Starters inserted into StarterBuff table.");


                var tempStm = starters.FindAll(x => x.VarIps > 227);
                tempStm.ForEach(x => context.StarterBuff.Add(new StarterBuff
                {
                    StarterId = x.StarterId,
                    FanId = x.FanId,
                    BuffId = 4
                }));

                Menu.PrintSystemMessage("\nGreat Stamina (ST+)" +
                    " qualified Starters inserted into StarterBuff table.");



                context.SaveChanges();

            }
        }

        static void Main(string[] args)
        {
            //// keep this code.  this is for initially importing the data from csv
            //// then, inserting from the list to the DB
            //// contains the following fields:
            //// starterID, starterName, teamName, vERA, vIPS, vKO9, VBB9, VFIP, FanID
            // string fileStarters2000 =
            //    @"D:\Source\Repos\GitTest\xTest\bin\Debug\netcoreapp3.1\Starters_2000.csv";
            // List<Starters> starters = new List<Starters>();
            // LoadStarters(starters, fileStarters2000);
            // InsertStartersToDB(starters);
            // FirstInsertIntoStarterBuff();


            List<Starters> starters = GetStartersFromDB();
            int menuSelection;




            do
            {
                Menu.PrintMainMenu();
                menuSelection = Menu.GetIntSelection();

                switch (menuSelection)
                {
                    case 8:
                        PrintStarters(starters);
                        Menu.PrintSystemRequest("Press Enter to go back to Main Menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 9:
                        Menu.PrintSystemMessage("Thank you for using the BB Stat Machine");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Menu.PrintErrorMessage("Bad Input. Please try again.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }

            } while (menuSelection != 9);


        }
    }
}
