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

        public static void Main(string[] args)
        {
            Hand myHand = new Hand();
            Score scoring = new Score();

            string hand = myHand.ShowHand();
            Console.WriteLine(hand + "\n");

            var fullCardScore = Score.ScoreFullCard(myHand);

            foreach (KeyValuePair<string, int> score in fullCardScore)
            {
                Console.WriteLine(score.Key + " = " + score.Value);
            }

            Exit();
        }
    }
}
