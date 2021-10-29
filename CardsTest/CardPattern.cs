using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsTest
{
    class CardPatterns
    {
        public const int patternHeight = 11;
        static string[,] patterns = new string[14, patternHeight]
        {
            {
                "+----------+",
                "|X2        |",
                "|          |",
                "|     X    |",
                "|          |",
                "|          |",
                "|          |",
                "|     X    |",
                "|          |",
                "|        2X|",
                "+----------+"
            },
            {
                "+----------+",
                "|X3        |",
                "|          |",
                "|     X    |",
                "|          |",
                "|     X    |",
                "|          |",
                "|     X    |",
                "|          |",
                "|        3X|",
                "+----------+"
            },
            {
                "+----------+",
                "|X4        |",
                "|          |",
                "|   X  X   |",
                "|          |",
                "|          |",
                "|          |",
                "|   X  X   |",
                "|          |",
                "|        4X|",
                "+----------+"
            },
            {
                "+----------+",
                "|X5        |",
                "|          |",
                "|   X  X   |",
                "|          |",
                "|     X    |",
                "|          |",
                "|   X  X   |",
                "|          |",
                "|        5X|",
                "+----------+"
            },
            {
                "+----------+",
                "|X6        |",
                "|          |",
                "|   X  X   |",
                "|          |",
                "|   X  X   |",
                "|          |",
                "|   X  X   |",
                "|          |",
                "|        6X|",
                "+----------+"
            },
            {
                "+----------+",
                "|X7        |",
                "|          |",
                "|  X   X   |",
                "|    X     |",
                "|  X   X   |",
                "|          |",
                "|  X   X   |",
                "|          |",
                "|        7X|",
                "+----------+"
            },
            {
                "+----------+",
                "|X8        |",
                "|  X   X   |",
                "|          |",
                "|  X   X   |",
                "|          |",
                "|  X   X   |",
                "|          |",
                "|  X   X   |",
                "|        8X|",
                "+----------+"
            },
            {
                "+----------+",
                "|X9        |",
                "|  X   X   |",
                "|    X     |",
                "|  X   X   |",
                "|          |",
                "|  X   X   |",
                "|          |",
                "|  X   X   |",
                "|        9X|",
                "+----------+"
            },
            {
                "+----------+",
                "|X10       |",
                "|  X   X   |",
                "|    X     |",
                "|  X   X   |",
                "|          |",
                "|  X   X   |",
                "|    X     |",
                "|  X   X   |",
                "|       10X|",
                "+----------+"
            },
            {
                "+----------+",
                "|XJ        |",
                "|   XXXXX  |",
                "|     XX   |",
                "|     XX   |",
                "|     XX   |",
                "|     XX   |",
                "|  XX XX   |",
                "|   XXX    |",
                "|        JX|",
                "+----------+"
            },
            {
                "+----------+",
                "|XQ        |",
                "|   XXXX   |",
                "|  XX  XX  |",
                "|  XX  XX  |",
                "|  XX  XX  |",
                "|  XX  XX  |",
                "|   XXX    |",
                "|      XX  |",
                "|        QX|",
                "+----------+"
            },
            {
                "+----------+",
                "|XK        |",
                "|  XX  XX  |",
                "|  XX XX   |",
                "|  XXXX    |",
                "|  XXX     |",
                "|  XXXX    |",
                "|  XX XX   |",
                "|  XX  XX  |",
                "|        KX|",
                "+----------+"
            },
            {
                "+----------+",
                "|XA        |",
                "|   XXXX   |",
                "|  XX  XX  |",
                "|  XX  XX  |",
                "|  XXXXXX  |",
                "|  XX  XX  |",
                "|  XX  XX  |",
                "|  XX  XX  |",
                "|        AX|",
                "+----------+"
            },
            {
                "+----------+",
                "|          |",
                "| ++++++++ |",
                "| ++++++++ |",
                "| ++++++++ |",
                "| ++++++++ |",
                "| ++++++++ |",
                "| ++++++++ |",
                "| ++++++++ |",
                "|          |",
                "+----------+"
            }
        };

        public static string[] GetCardPattern( char suitSymbol, int cardValue )
        {
            string[] cardPattern = new string[patternHeight];

            // Copy the pattern for the card into the output
            // replacing occurrances of 'X' with the suit symbol.
            for (int x = 0; x < patternHeight; x++)
                cardPattern[x] = patterns[cardValue, x].Replace('X', suitSymbol);

            return cardPattern;
        }
    }
}
