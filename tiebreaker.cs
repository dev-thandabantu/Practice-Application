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

		//single winner variables
        public static bool win = false;
        public static string winnerName = "Player1";
        public static string winnerScore = "35";
		//tie variables
        public static bool tie = false;
        public static string winner1Name = "Player2";
        public static string winner2Name = "Player3";
        public static int score = 45;

        public static void Main(string[] args)
		{
			string fileName = "";
			string outputFileName = "";

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
			if (fileName == "")
			{
				outputFileName = outputFileName != "" ? outputFileName : "output.txt";
				File.WriteAllText(outputFileName, "No input file specified");
				return;
			}
			if (outputFileName == "")
			{
				File.WriteAllText("output.txt", "No output file specified");
				return;
			}
			if (!File.Exists(fileName))
			{
				File.WriteAllText(outputFileName, "The input file does not exist.");
				return;
			}
            bool validInput = true;
			//validInput = ValidateInput(fileName);
            if (!validInput)
            {
                File.WriteAllText(outputFileName, "Exception:Some reason why the input is wrong.");
                return;
            }

			//find the winner
            FindWinner(fileName);

			//write winner(s)
            if (win)
			{
                File.WriteAllText(outputFileName, winnerName + ":" + winnerScore);
				return;
			}
			else if (tie)
			{
                File.WriteAllText(outputFileName, winner1Name + "," + winner2Name + ":" + score);
				return;
			}
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

			//sort players by score
			players.OrderBy(p => p.Score);
			foreach (var item in players)
			{
				//Console.WriteLine(item.Score);
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
				//Add value to list of scores
				scores.Add(int.TryParse(faceValue, out parsedInt) ? parsedInt : GetFaceValue(faceValue));
			}

            //sort player cards
            scores.Sort();
			scores.Reverse();

            //sum top 3 cards
            totalScore = scores.Sum();
			Console.WriteLine(totalScore);
            return score;
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

    }

	public class Player
	{
		public string Name { get; set; }
		public int Score { get; set; }
	}
}
