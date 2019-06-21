using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeSim
{
    public class Score
    {

        public enum LowerSection { ones = 1, twos, threes, fours, fives, sixes };
        public enum UpperSection { threeofakind, fourofakind, fullhouse, smallstraight, largestraight, yahtzee, chance };

        public static IDictionary<string, int> POINTS = new Dictionary<string, int>()
            {
                {"fullhouse", 25},
                {"smallstraight", 30},
                {"largestraight", 40},
                {"yahtzee", 50}
            };

        public static List<List<int>> SMALL_STRAIGHTS = new List<List<int>>() 
        {
            {new List<int> {1, 2, 3, 4}},
            {new List<int> {2, 3, 4, 5}},
            {new List<int> {3, 4, 5, 6}},
        };

        public static List<List<int>> LARGE_STRAIGHTS = new List<List<int>>() 
        {
            {new List<int> {1, 2, 3, 4, 5}},
            {new List<int> {2, 3, 4, 5, 6}},
        };

        public static IDictionary<string, int> ScoreFullCard(Hand hand)
        {
            IDictionary<string, int> ScoreCard = new Dictionary<string, int>() { };

            foreach (var val in LowerSection.GetValues(typeof(LowerSection)))
            {
                string section = val.ToString();
                ScoreCard[section] = Score.ScoreHand(section, hand);
            }

            foreach (var val in UpperSection.GetValues(typeof(UpperSection)))
            {
                string section = val.ToString();
                ScoreCard[section] = Score.ScoreHand(section, hand);
            }

            return ScoreCard;
        }

        public static int ScoreHand(string section, Hand hand)
        {
            section = section.ToLower();

            if (Enum.IsDefined(typeof(LowerSection), section))
            {
                int diceValue = (int)Enum.Parse(typeof(LowerSection), section);
                return ScoreLower(diceValue, hand);
            }
            else if (Enum.IsDefined(typeof(UpperSection), section))
            {
                UpperSection scoringSection = (UpperSection) Enum.Parse(typeof(UpperSection), section);
                return ScoreUpper(scoringSection, hand);
            }
            else
            {
                Console.WriteLine("INVALID SCORE CATEGORY");
                return 0;
            }
        }

        public static int ScoreLower(int section, Hand hand)
        {
            int score = 0;

            List<int> playerHand = hand.GetDiceList();

            foreach (int die in playerHand)
            {
                if (die == section)
                {
                    score += die;
                }
            }

            return score;
        }

        public static int ScoreUpper(UpperSection section, Hand hand)
        {
            int score = 0;
            List<int> playerHand = hand.GetDiceList();

            switch (section)
            {
                case UpperSection.threeofakind:
                    score = ScoreThreeOfAKind(playerHand);
                    break;
                case UpperSection.fourofakind:
                    score = ScoreFourOfAKind(playerHand);
                    break;
                case UpperSection.fullhouse:
                    score = ScoreFullHouse(playerHand);
                    break;
                case UpperSection.smallstraight:
                    score = ScoreSmallStraight(playerHand);
                    break;
                case UpperSection.largestraight:
                    score = ScoreLargeStraight(playerHand);
                    break;
                case UpperSection.yahtzee:
                    score = ScoreYahtzee(playerHand);
                    break;
                case UpperSection.chance:
                    score = ScoreChance(playerHand);
                    break;
                default:
                    Console.WriteLine("Invalid scoring option");
                    break;
            }

            return score;
        }

        public static int ScoreThreeOfAKind(List<int> hand)
        {
            if (isThreeOfAKind(hand))
            {
                return CountUpDice(hand);
            }
            else
            {
                return 0;
            }
        }

        public static int ScoreFourOfAKind(List<int> hand)
        {
            if (isFourOfAKind(hand))
            {
                return CountUpDice(hand);
            }
            else
            {
                return 0;
            }
        }

        public static int ScoreFullHouse(List<int> hand)
        {
            if (isFullHouse(hand))
            {
                return POINTS["fullhouse"];
            }
            else 
            {
                return 0;
            }
        }

        public static int ScoreSmallStraight(List<int> hand)
        {
            if (isSmallStraight(hand))
            {
                return POINTS["smallstraight"];
            }
            else
            {
                return 0;
            }
        }

        public static int ScoreLargeStraight(List<int> hand)
        {
            if (isLargeStraight(hand))
            {
                return POINTS["largestraight"];
            }
            else
            {
                return 0;
            }
        }

        public static int ScoreYahtzee(List<int> hand)
        {
            if (isYahtzee(hand))
            {
                return POINTS["yahtzee"];
            }
            else
            {
                return 0;
            }
        }

        public static int ScoreChance(List<int> hand)
        {
            return CountUpDice(hand);
        }

        public static bool isThreeOfAKind(List<int> hand)
        {
            return isMultipleOfAKind(hand, 3);
        }

        public static bool isFourOfAKind(List<int> hand)
        {
            return isMultipleOfAKind(hand, 4);
        }

        public static bool isFullHouse(List<int> hand)
        {
            return hand.Distinct().Count() == 2;
        }

        public static bool isSmallStraight(List<int> hand)
        {
            return CheckForStraight(hand, SMALL_STRAIGHTS);
        }

        public static bool isLargeStraight(List<int> hand)
        {
            return CheckForStraight(hand, LARGE_STRAIGHTS);
        }

        public static bool isYahtzee(List<int> hand)
        {
            return hand.Distinct().Count() == 1;
        }

        public static int CountUpDice(List<int> hand)
        {
            int score = 0;

            foreach (int die in hand)
            {
                score += die;
            }

            return score;
        }

        public static bool isMultipleOfAKind(List<int> hand, int n)
        {
            var uniques = hand.Distinct().ToList();
            IDictionary<int, int> uniqueCounts = new Dictionary<int, int>() { };

            foreach (int value in uniques)
            {
                uniqueCounts.Add(value, 0);
            }

            foreach (int die in hand)
            {
                int currentCount;

                uniqueCounts.TryGetValue(die, out currentCount);
                uniqueCounts[die] = currentCount + 1;
            }

            foreach (KeyValuePair<int, int> count in uniqueCounts)
            {
                if (count.Value >= n)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckForStraight(List<int> hand, List<List<int>> possibleStraights)
        {
            List<int> uniques = hand.Distinct().ToList();
            uniques.Sort();

            foreach (List<int> straight in possibleStraights)
            {
                if (uniques.SequenceEqual(straight))
                {
                    return true;
                }
            }

            return false;
        }
    }
}