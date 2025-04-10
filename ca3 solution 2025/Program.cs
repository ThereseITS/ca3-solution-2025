using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace ca3_solution_2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string playerPath = @"../../../entrants.csv";
            string resultsPath = @"../../../results.csv";

            List<Player> players = new List<Player>();

            players =  SetUpPlayers(playerPath);
            players = AddResults(players, resultsPath);

            PrintReport(players);


        }
        /// <summary>
        /// This method creates a list of players from a file with their names only. 
        /// It could be adapted 
        /// </summary>
        /// <param name="path">path of the file storing the players names</param>
        /// <returns></returns>
        static List<Player> SetUpPlayers(string path)
        {
            List<Player> players = new List<Player>();
            string input;
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while ((input = sr.ReadLine()) != null)
                    {
                        // This assumes the input line contains a name (string).

                        Player p = new Player(input);
                        players.Add(p);
                    }
                }
              
            }
            catch (FileNotFoundException ex)
            { 
            Console.WriteLine(ex.Message);
            }
            
            return players;
        }
/// <summary>
/// This method updates the players by reading in results from a results file.
/// </summary>
/// <param name="players"> List of players</param>
/// <param name="path"> file path of the file containing results in format name,name, winner's name (or "drawn")</param>
/// <returns>updated list of players</returns>
        static List<Player> AddResults(List<Player> players, string path)
        {
            string input;
            using (StreamReader sr = File.OpenText(path))
            {
                while ((input = sr.ReadLine()) != null)
                {
                    string[] fields = input.Split(','); // split input line
                    int index1 = 0;
                    int index2 = 0;

                    if (   (fields.Length == 3)
                        && ((index1 = FindIndexFromName(fields[0].Trim(), players)) != -1)
                        && ((index2 = FindIndexFromName(fields[1].Trim(),players)) != -1)
                        && (fields[2].ToLower() == "draw" || fields[2].Trim() == players[index1].Name || fields[2].Trim() == players[index2].Name))
                     { 
                      
                        if (fields[2].Trim() == fields[0].Trim())// player 1 won
                        {
                            players[index1].AddResults("won");
                            players[index2].AddResults("lost");

                        }

                        else if (fields[2].Trim() == fields[1].Trim())// player 2 won
                        {

                            players[index2].AddResults("won");
                            players[index1].AddResults("lost");
                        }
                        else // stalemate
                        {
                            
                            players[index1].AddResults("drawn");
                            players[index2].AddResults("drawn");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Record {input}");
                    }
                }
            }
            return players;
        }
        /// <summary>
        /// This method searches for a name in the list, and returns the index.
        /// We can also use the Linq methods which take an expression as input (better solution) .
        /// </summary>
        /// <param name="name"></param>
        /// <param name="players"></param>
        /// <returns></returns>
        public static int FindIndexFromName(string name, List <Player> players )
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// This method prints a report to the console, including the biggest player.
        /// This could be improved by building and returning a string using the StringBuilder class
        /// This would mean that the returned string could be written to a file or the console.
        /// </summary>
        /// <param name="players"></param>
        public static void PrintReport(List<Player> players)
        {
            int biggestIndex = 0;

            Console.WriteLine($"{"Name",-10} {"Played",-10} {"Won",-10} {"Drawn",-10} {"Lost",-10} {"Points",-10}\n");

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Points > players[biggestIndex].Points)
                {
                    biggestIndex = i;
                }

                Console.WriteLine(players[i].ToString());
            }
          

            for (int i = 0; i < players.Count; i++)
            {
                if ((players[i].Points == players[biggestIndex].Points))
                {
                    Console.WriteLine($"\nThe winning player is: {players[i].Name}");
                }
            }

        }
    }
}
