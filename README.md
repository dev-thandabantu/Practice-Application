# Practice-Application

Programming Task
We've created a simple multiplayer card game where:
• 6 players are dealt 5 cards from two 52 card decks, and the winner is the one with the highest score.
The score for each player is calculated by adding the highest 3 card values for each player, where the
number cards have their face value, J = 11, Q = 12, K = 13 and A = 11 (not 1).
•
In the event of a tie, the scores are recalculated for only the tied players by calculating a "suit score" for
each player to see if the tie can be broken (it may not).
Each card is given a score based on its suit, with clubs = 1, diamonds = 2, hearts = 3 and spades
= 4, and the player's score is the suit value of the player’s highest card.
o
•
You are required to write a command line application using C# that needs to do
the following:
• Run on Windows.
• Be invoked with the name of the input and output text files.
• Read the data from the input file, find the winner(s) and write them to the output file.
• Handle any problems with the input or input file contents.
