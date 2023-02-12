using System;
using System.IO;
using System.Threading.Tasks;

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
			int maxScore = 0;

        }
	} 
}
