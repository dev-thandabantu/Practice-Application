using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TieBreakerApp
{
	/// <summary>
	/// This class finds a winner (demonstrating documentation knowledge).
	/// </summary>
	public class TieBreaker
	{
		public static string outputFileName { get; set; }
        public static int winnerScore { get; set; }
        //single winner variables
        public static bool win { get; set; }
        public static string winnerName { get; set; }
		//tie variables
        public static bool tie { get; set; }
        public static string winner1Name { get; set; }
        public static string winner2Name { get; set; }

        public static void Main(string[] args)
		{
			string fileName = "";

			//get names for input and output files
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == "--in")
				{
                    fileName = i + 1 < args.Length ? args[i + 1] : "";
				}
				else if (args[i] == "--out")
				{
					outputFileName = i + 1 < args.Length ? args[i + 1] : "";
				}
			}

			//some guard clauses
			if (String.IsNullOrEmpty(fileName))
			{
				outputFileName = outputFileName != "" ? outputFileName : "output.txt";
				File.WriteAllText(outputFileName, "Exception:No input file specified");
				return;
			}
			if (String.IsNullOrEmpty(outputFileName))
			{
				File.WriteAllText("output.txt", "Exception:No output file specified");
				return;
			}
			if (!File.Exists(fileName))
			{
				File.WriteAllText(outputFileName, "Exception:The input file does not exist.");
				return;
			}
            bool validInput = true;
			validInput = ValidateInput(fileName);
            if (!validInput)
            {
                File.WriteAllText(outputFileName, "Exception:Invalid file contents.");
                return;
            }

			//find the winner
            FindWinner(fileName);
		}

		public static void FindWinner(string inputFile)
		{
			string playerName = "";
			int playerScore = 0;
			List<Player> players = new List<Player>();
			foreach (var player in File.ReadLines(inputFile))
			{
				var playerAndCards = player.Trim().Split(':');
                playerName = playerAndCards[0];
                playerScore = SumPlayerScore(playerAndCards[1]);
				players.Add(new Player
				{
                    Name = playerName, 
					Score = playerScore
                });
            }

			//sort players by score in desc order
			players = players.OrderByDescending(p => p.Score).ToList();
			
			winnerScore = players.Max(p => p.Score);
			tie = players.Where(p => p.Score == winnerScore).Count() == 2;
			win = players.Where(p => p.Score == winnerScore).Count() == 1;
			if (tie)
			{
				winner1Name = players[0].Name;
				winner2Name = players[1].Name;
                //TODO: winnerScore = CalculateFaceSuitValue(inputFile);
                File.WriteAllText(outputFileName, winner1Name + "," + winner2Name + ":" + winnerScore);
			}
			else if (win)
			{
				winnerName = players[0].Name;
                File.WriteAllText(outputFileName, winnerName + ":" + winnerScore);
			}
			else
			{
                File.WriteAllText(outputFileName, "Exception:More than two winners.");
            }
        }

		public static int SumPlayerScore(string cards)
		{
			var totalScore = 0;
			var scores = new List<int>();
			foreach (var card in cards.Trim().Split(','))
			{
				var faceValue = card.Substring(0, card.Length - 1);
				var suit = card[card.Length - 1];
				var parsedInt = 0;
				//Add value to list of scores--*
				scores.Add(int.TryParse(faceValue, out parsedInt) ? int.Parse(faceValue) : GetFaceValue(faceValue));
			}

            //sort player cards
            scores.Sort();
			scores.Reverse();

            //sum top 3 cards
            totalScore = scores.Take(3).Sum();
            return totalScore;
		}

		public static int GetFaceValue(string faceValueString)
		{
            //handle "input is not case-sensitive"
            var input = faceValueString.ToUpper();

			var value = 0;
			switch (input)
			{
				case "J":
					value = 11;
					break;
				case "Q":
					value = 12;
					break;
				case "K":
					value = 13;
					break;
				case "A":
					value = 11;
					break;
				default:
					value = 0;
					break;
			}
			return value;
		}

		public static bool ValidateInput(string fileName)
		{
			//TODO: Validate the contents of the input file
			//"tie may not be broken"

			return true;
		}
    }

	public class Player
	{
		public string Name { get; set; }
		public int Score { get; set; }
	}
}
