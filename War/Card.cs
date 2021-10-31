using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    /// <summary>
    ///  Represents a single playing card
    /// </summary>
    public class Card
    {
        // Define static arrays to represent suit and card value symbols
        /// <summary>
        /// An array of Unicode suit symbols
        /// </summary>
        public static char[] suitSymbols = { '\u2660', '\u2663', '\u2665', '\u2666' };

        /// <summary>
        /// The total number of cards in each suit
        /// </summary>
        public const int valueLimit = 13; // Thirteen cards per suit

        // Define instance members to represent the suit and value indecies in the arrays
        /// <summary>
        /// The suit index of this card (0-3)
        /// </summary>
        private int suitIndex;

        /// <summary>
        /// The value index of this card 0-12
        /// </summary>
        private int valueIndex;

        /// <summary>
        /// The read-only symbol associated with this card's suit
        /// </summary>
        public char cardSuitSymbol
        {
            get
            {
                if (suitIndex < 0 || suitIndex >= suitSymbols.Count())
                    throw new ArgumentOutOfRangeException(String.Format("suitValue ({0}) out of range!", suitIndex));
                return suitSymbols[suitIndex];
            }
        }

        // Define public accessors to protect array ranges

        /// <summary>
        /// The set only property of the card suit (0-3)
        /// </summary>
        public int suit
        {
            set
            {
                if (value < 0 || value >= suitSymbols.Count())
                    throw new ArgumentOutOfRangeException(String.Format("suitValue ({0}) out of range!", value));
                suitIndex = value >= 0 && value <= suitSymbols.Count() ? value : 0;
            }
        }

        /// <summary>
        /// The comparative value of the card (0-12)
        /// </summary>
        public int cardValue
        {
            get
            {
                return valueIndex;
            }
            set
            {
                if(value < 0 || value >= Card.valueLimit)
                    throw new ArgumentOutOfRangeException(String.Format("cardValue ({0}) out of range!", value));
                valueIndex = value;
            }
        }

        /// <summary>
        /// Public contructor to initialize a new card with indecies
        /// </summary>
        /// <param name="suitIndex_p">The index of the card suit 0-3</param>
        /// <param name="valueIndex_p">The index of the card value 0-12</param>
        public Card( int suitIndex_p, int valueIndex_p )
        {
            // Use property set accessors to support validation
            cardValue = valueIndex_p;
            suit = suitIndex_p;
        }
    }
}
