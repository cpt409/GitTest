using System;
using System.IO;
using System.Collections.Generic;
using static System.Console;
using System.Linq;

namespace zTest
{
    class Program
    {
        class Pitcher
        {
            public Pitcher()
            {

            }

            public string Name { get; set; }
            public string Team { get; set; }
            public double varERA { get; set; }
            public double varIPS { get; set; }
            public double varKO9 { get; set; }
            public double varGBP { get; set; }
            public double varBB9 { get; set; }
            public double varFBP { get; set; }
            public int playerId { get; set; }

            public override string ToString()
            {
                return $"{Name,-20} {varERA,-7}" +
                    $"{varIPS,-7} {varKO9,-7} {varGBP,-7} " +
                    $"{varBB9,-7} {varFBP,-7} {playerId,-20}";

                //                return $"{playerId,10}   {Name,-25} {varGBP,-7} {varFBP,-7}";
            }

        }

        class Batter
        {

        }


        static List<string> SetPitcherTitle(string title)
        {
            var tempTitleArray = title.Split(',');
            var tempTitleList = tempTitleArray.ToList();

            List<string> temp = new List<string>();
            foreach (var item in tempTitleList)
            {
                temp.Add(item);
            }

            return temp;
        }


        static void LoadPitchers(List<Pitcher> pitchers, string filename, List<string> title)
        {
            try
            {
                using (var sr = new StreamReader(filename))
                {

                    int count = 0, tempPlayerId = 0;
                    double tempIP = 0.0, tempKO9 = 0.0, tempGBP = 0.0,
                        tempBB9 = 0.0, tempFBP = 0.0;

                    // skip first line because it is the header row
                    var tempTitleLine = sr.ReadLine();

                    title = SetPitcherTitle(tempTitleLine);
                    // PrintPitcherTitle(title);

                    while (!sr.EndOfStream)
                    {
                        //int tempPlayerId;
                        //double tempERA, tempIP, tempKO9, tempGBP,
                        //    tempBB9, tempFBP;

                        var line = sr.ReadLine();
                        var item = line.Split(',');

                        double tempERA = 0.0;
                        double.TryParse(item[2], out tempERA);
                        double.TryParse(item[3], out tempIP);
                        double.TryParse(item[4], out tempKO9);

                        item[5] = item[5].Trim('%');
                        double.TryParse(item[5], out tempGBP);

                        double.TryParse(item[6], out tempBB9);

                        item[7] = item[7].Trim('%');
                        double.TryParse(item[7], out tempFBP);
                        int.TryParse(item[8], out tempPlayerId);

                        pitchers.Add(new Pitcher()
                        {
                            Name = item[0],
                            Team = item[1],
                            varERA = tempERA,
                            varIPS = tempIP,
                            varKO9 = tempKO9,
                            varGBP = tempGBP,
                            varBB9 = tempBB9,
                            varFBP = tempFBP,
                            playerId = tempPlayerId
                        });

                        count++;
                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"\n\nCannot open file: {ex}\n\n");
            }
        }

        static void PrintPitchers(List<Pitcher> pitchers)
        {
            pitchers = pitchers.OrderByDescending(x => x.varGBP).ToList();
            pitchers.ForEach(x => Console.WriteLine($"{x}"));

            Console.WriteLine($"{pitchers.Count}");
        }


        static void CombinePitcherLists(List<Pitcher> first, List<Pitcher> second)
        {
            //            var combined = 

            List<Pitcher> newList = new List<Pitcher>();

            foreach (var f in first)
            {
                var n = second.Find(x => x.playerId == f.playerId);
                if (n is Pitcher)
                {
                    f.varGBP = n.varGBP;
                    f.varFBP = n.varFBP;
                }
            }
        }

        static void SortPitchers(List<Pitcher> pitchers)
        {
            pitchers.OrderByDescending(x => x.varGBP);

        }

        static List<Pitcher> RemoveGBP(List<Pitcher> pitchers)
        {
            pitchers.RemoveAll(x => x.varGBP < 1);
            return pitchers;
        }

        static void PrintPitcherTitle(List<string> title)
        {
            if (title.Count > 0)
            {
                Console.WriteLine();
                foreach (var item in title)
                {
                    Console.Write($"{item}\t");
                }

                Console.WriteLine("\n");
            }

        }

        static public void InsertIntoPitcherBuff()
        {

            //using (var context = new 


        }


        static void Main(string[] args)
        {

            List<string> title = new List<string>();

            string file2000Pitching = "2000_pitching_qualified.csv";
            string file2002Pitching = "2002_pitching_qualified.csv";
            string file1999Pitching = "pitchers_1999_2002.csv";

            List<Pitcher> pitchers = new List<Pitcher>();
            LoadPitchers(pitchers, file1999Pitching, title);
            RemoveGBP(pitchers);
            //PrintPitcherTitle(title);
            //PrintPitchers(pitchers);

            InsertIntoPitcherBuff();


        }
    }
}
