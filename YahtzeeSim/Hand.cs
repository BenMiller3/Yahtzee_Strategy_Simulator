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

            
            for (int i = 0; i < totalDice; i++)
            {
                currentDice.Add(RollDice());
            }
            
        }

        private static int RollDice()
        {
            return rnd.Next(1, 7);
        }

        public String ShowHand()
        {
            String hand = "";

            foreach (int dice in currentDice)
            {
                hand += dice.ToString() + " ";
            }

            return hand;
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
