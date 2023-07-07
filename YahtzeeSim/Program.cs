using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeSim
{
    public class Program
    {
        static void Exit()
        {
            Console.WriteLine("\nPress ENTER to exit.");
            Console.ReadLine();
        }

        public static Hand Round(Hand hand)
        {
            hand.ShowHand();
            hand = Turn(hand);
            hand.ShowHand();
            hand = Turn(hand);
            hand.ShowHand();

            return hand;
        }

        public static Hand Turn(Hand hand)
        {
            List<string> reRolls = new List<string>();
            List<int> reRolledPositions = new List<int>();
            string input = "\n";

            do
            {
                input = Console.ReadLine();
                reRolls.Add(input);
            }
            while (input != "");

            foreach (string position in reRolls)
            {
                try
                {
                    reRolledPositions.Add(int.Parse(position));
                }
                catch (FormatException e)
                {
                    //Console.WriteLine("Format exception as: " + e);
                }
            }

            hand.ReRollDice(reRolledPositions);

            return hand;
        }

        public static void Main(string[] args)
        {
            Game game = new Game();

            for (int i = 0; i < 15; i++)
            {
                Hand myHand = new Hand();
                myHand = Round(myHand);
                Console.Write("Enter the section you want to score on: ");
                string section = Console.ReadLine();
                game.ScoreSection(myHand, section);
                game.DisplayBoard();
            }

            int finalScore = game.CalculateFinalScore();

            Console.WriteLine("\nCongratulations! You scored: " + finalScore);
            

            Exit();
        }
    }
}
