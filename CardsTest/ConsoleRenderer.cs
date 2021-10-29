using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsTest
{
    class ConsoleRenderer : Renderer
    {
        // Clear the console display
        public override void ClearSurface()
        {
            Console.Clear();
        }

        // Render a simple message to the player(s)
        public override void RenderMessage(string message)
        {
            if (Console.BufferWidth > message.Length)
            {
                Console.SetCursorPosition((Console.BufferWidth - message.Length) / 2, Console.WindowHeight / 2);
                Console.Write(message);
            } else
                throw new NotImplementedException();
        }

        private const int statusWidth = 20;

        public override void RenderPlayerStatus(int playerNumber, string playerName, int deckSize)
        {
            if (playerNumber < 1 || playerNumber > 2)
                throw new ArgumentOutOfRangeException("Invalid player number");
            else
            {
                int column = playerNumber == 1 ? 1 : Console.BufferWidth / 2;
                string name = playerName.Length > statusWidth ? playerName.Substring(0, statusWidth) : playerName;
                Console.SetCursorPosition(column, 1);
                Console.Write(name);
                Console.SetCursorPosition(column, 2);
                Console.Write(String.Format("Cards Left: {0}", deckSize));
            }
        }

        private const int playerCardRowBase = 4;

        public override void RenderPlayerCard(Card card, int playerNumber, int position)
        {
            if( playerNumber < 1 || playerNumber > 2 )
                throw new ArgumentOutOfRangeException("Invalid player number");
            else
            {
                int columnBase = playerNumber == 1 ? 0 : Console.BufferWidth / 2;
                int column = columnBase + position;
                int row = playerCardRowBase + position;

                RenderCard(card, column, row);
            }
        }

        public override void RenderCard(Card card, int xPosition, int yPosition)
        {
            for( int y = 0; y < CardPatterns.patternHeight; y ++ )
            {
                Console.SetCursorPosition(xPosition, yPosition + y);
                Console.Write(CardPatterns.GetCardPattern(card.cardSuitSymbol, card.cardValue)[y]);
            }
        }

        public override void RenderGameResult(string winner)
        {
            throw new NotImplementedException();
        }
    }
}
