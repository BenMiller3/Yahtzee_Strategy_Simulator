using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeSim
{
    public class Hand
    {
        public static List<int> currentDice = new List<int> { };

        private int totalDice = 5;
        private static Random rnd;

        public Hand()
        {
            rnd = new Random();

            currentDice = new List<int> { };
            for (int i = 0; i < totalDice; i++)
            {
                currentDice.Add(RollDie());
            }
            
        }

        private static int RollDie()
        {
            return rnd.Next(1, 7);
        }

        public void ReRollDice(List<int> diePositions)
        {
            foreach (int position in diePositions)
            {
                if (position > 0 && position <= totalDice)
                {
                    currentDice[position - 1] = RollDie();
                }
            }
        }

        public String GetHandString()
        {
            String hand = "";

            foreach (int dice in currentDice)
            {
                hand += dice.ToString() + " ";
            }

            return hand;
        }

        public void ShowHand()
        {
            Console.WriteLine(GetHandString());
        }

        public List<int> GetDiceList()
        {
            List<int> diceList = new List<int> { };

            foreach (int die in currentDice)
            {
                diceList.Add(die);
            }

            return diceList;
        }
    }
}
