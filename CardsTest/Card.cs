using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsTest
{
    public class Card
    {
        // Define static arrays to represent suit and card value symbols
        public static char[] suitSymbols = { '\u2660', '\u2663', '\u2665', '\u2666' };
        public static string[] valueSymbols = 
            { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        // Define instance members to represent the suit and value indecies in the arrays
        private int suitIndex;
        private int valueIndex;

        // Define public accessors to protect array ranges
        public char cardSuitSymbol {
            get
            {
                if (suitIndex < 0 || suitIndex >= suitSymbols.Count())
                    throw new ArgumentOutOfRangeException(String.Format("suitValue ({0}) out of range!", suitIndex));
                return suitSymbols[suitIndex];
            }
        }

        // Define public accessors to protect array ranges
        public int suit
        {
            set
            {
                if (value < 0 || value >= suitSymbols.Count())
                    throw new ArgumentOutOfRangeException(String.Format("suitValue ({0}) out of range!", value));
                suitIndex = value >= 0 && value <= suitSymbols.Count() ? value : 0;
            }
        }

        // Define public accessors to protect array ranges
        public string cardValueSymbol {
            get {
                if (valueIndex < 0 || valueIndex >= valueSymbols.Count())
                    throw new ArgumentOutOfRangeException(String.Format("cardValue ({0}) out of range!", valueIndex));
                return valueIndex >= 0 && valueIndex < valueSymbols.Count() ? valueSymbols[valueIndex] : "";
            }
        }

        // Define public accessors to protect array ranges
        public int cardValue
        {
            get
            {
                return valueIndex;
            }
            set
            {
                if(value < 0 || value >= valueSymbols.Count())
                    throw new ArgumentOutOfRangeException(String.Format("cardValue ({0}) out of range!", value));
                valueIndex = value;
            }
        }

        // Define public contructor to initialize a new card with indecies
        public Card( int suitIndex_p, int valueIndex_p )
        {
            // Use property set accessors for validation
            cardValue = valueIndex_p;
            suit = suitIndex_p;
        }
    }
}
