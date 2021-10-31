using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    /// <summary>
    /// A console-based render class for Windows console applications
    /// </summary>
    class ConsoleRenderer : Renderer
    {
        /// <summary>
        /// A useful string full of spaces to use when clearing lines on the console
        /// </summary>
        const String clearLine = "                                                                                ";

        /// <summary>
        /// Indicates the width of the player status area
        /// </summary>
        private const int statusWidth = 20;

        /// <summary>
        /// The length of the last message line written.
        /// </summary>
        int lastMessageLength = 0;

        /// <summary>
        /// The starting X position of the last message line
        /// </summary>
        int lastMessageX = 0;

        /// <summary>
        /// The starting row on the console where cards are rendered (from the top/0)
        /// </summary>
        private const int playerCardRowBase = 4;

        /// <summary>
        /// The width of the last computer taunt message
        /// </summary>
        int lastTauntLength = 0;

        /// <summary>
        /// Clear the display surface
        /// </summary>
        public override void ClearSurface()
        {
            Console.Clear();
        }

        /// <summary>
        /// Render a simple message to the player(s) 
        /// </summary>
        /// <param name="message">The message to be rendered</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When the message length is too long for the display
        /// </exception>
        public override void RenderMessage(string message)
        {
            // Remove any previous message from the console.
            ClearLastMessage();

            // Now see if our new message is within acceptable limits
            if (54 > message.Length)
            {
                int xPosition = 27 - (message.Length / 2);
                int yPosition = 20;
                Console.SetCursorPosition(xPosition, yPosition);
                Console.Out.Write(message);

                lastMessageLength = message.Length;
                lastMessageX = xPosition;
            }
            else
                throw new ArgumentOutOfRangeException("message");
        }

        /// <summary>
        /// Clears the last message from the console
        /// </summary>
        public override void ClearLastMessage()
        {
            if (lastMessageLength > 0 && lastMessageLength < 80)
            {
                int yPosition = 20;
                Console.SetCursorPosition(lastMessageX, yPosition);
                Console.Out.Write(clearLine.Substring(0, lastMessageLength));
            }
        }

        /// <summary>
        /// Renders player name and cards left
        /// </summary>
        /// <param name="playerNumber">1 or 2</param>
        /// <param name="playerName">The name of the player</param>
        /// <param name="deckSize">The number of cards the player has left</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the playerNumber given is not 1 or 2
        /// </exception>
        public override void RenderPlayerStatus(int playerNumber, string playerName, int deckSize)
        {
            if (playerNumber < 1 || playerNumber > 2)
                throw new ArgumentOutOfRangeException("Invalid player number");
            else
            {
                int column = playerNumber == 1 ? 1 : Console.BufferWidth / 2;
                string name = playerName.Substring( 0, Math.Min( statusWidth, playerName.Length ));
                Console.SetCursorPosition(column, 1);
                Console.Write(name);

                // Clear their variable data first
                int variableDataWidth = Console.BufferWidth / 2 - 12;
                Console.SetCursorPosition(column, 2);
                Console.Write(String.Format("Cards Left: {0}", clearLine.Substring( 0, variableDataWidth )));

                // Now write the updated variable data
                Console.SetCursorPosition(column, 2);
                Console.Write(String.Format("Cards Left: {0}", deckSize ));
            }
        }

        /// <summary>
        /// Render a single card on the console
        /// </summary>
        /// <param name="card">The card to be rendered</param>
        /// <param name="playerNumber">Which player space to render in (1 or 2)</param>
        /// <param name="position">The card offset position (0-4)</param>
        /// <param name="faceDown">Whether the back of the card should be rendered instead</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If player number is neither 1 or 2 
        /// </exception>
        public override void RenderPlayerCard(Card card, int playerNumber, int position, bool faceDown )
        {
            if( playerNumber < 1 || playerNumber > 2 )
                throw new ArgumentOutOfRangeException("Invalid player number");
            else
            {
                int columnBase = playerNumber == 1 ? 0 : Console.BufferWidth / 2;
                int column = columnBase + position;
                int row = playerCardRowBase + position;

                RenderCard(card, column, row, faceDown);
            }
        }

        /// <summary>
        /// Render a single card on the console
        /// </summary>
        /// <param name="card">The card to be rendered</param>
        /// <param name="xPosition">The absolute X column to start from</param>
        /// <param name="yPosition">The absolute Y row to start from</param>
        /// <param name="faceDown">Whether the back of the card should be rendered instead</param>
        public override void RenderCard(Card card, int xPosition, int yPosition, bool faceDown )
        {
            // NOTE: The last pattern in the pattern array is the card "back"
            // Using the valueLimit as the pattern index points to the card "back" pattern.
            int patternIndex = faceDown ? Card.valueLimit : card.cardValue;

            for ( int y = 0; y < CardPatterns.patternHeight; y ++ )
            {
                Console.SetCursorPosition(xPosition, yPosition + y);
                Console.Write(CardPatterns.GetCardPattern(card.cardSuitSymbol, patternIndex)[y]);
            }
        }

        /// <summary>
        /// Present the winning screen
        /// </summary>
        /// <param name="winner">The name of the winner</param>
        /// <param name="duration">The amount of time it took to play</param>
        /// <param name="rounds">The number of rounds it took to finish</param>
        public override void RenderGameResult(string winner, string duration, int rounds)
        {
            ClearSurface();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine(
                "Congratulations to both players for a hard fought game of War!\n" +
                String.Format("But special congratultions to {0}.\n\n", winner) +
                String.Format("{0} won after {1} rounds in {2}.", winner, rounds, duration)
                );
        }

        /// <summary>
        /// Render a card for the given player and pause for a short time
        /// </summary>
        /// <param name="card">The card to be played (rendered)</param>
        /// <param name="playerNumber">The player number (1 or 2)</param>
        /// <param name="position">The relative card position (0-4)</param>
        /// <param name="faceDown">Whether the back of the card should be rendered</param>
        public override void PlayCard(Card card, int playerNumber, int position, bool faceDown)
        {
            const int playTime = 300; //ms

            RenderPlayerCard(card, playerNumber, position, faceDown);

            // Wait for a short time to provide pacing.
            Pause(playTime);
        }

        /// <summary>
        /// Render the number of cards in contest
        /// </summary>
        /// <param name="cardsAtWar">How many cards are ar risk (in play)</param>
        public override void RenderCombatantStats(int cardsAtWar)
        {
            string message = String.Format("Cards at war: {0}", cardsAtWar);
            int xPosition = 27 - (message.Length / 2);
            int yPosition = 5;
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Out.Write(message);
        }

        /// <summary>
        /// Render a computer taunt 
        /// </summary>
        /// <param name="message">The taunting message to render</param>
        public override void RenderTaunt(string message)
        {
            String taunt = "Computer: " + message;

            // Remove any previous taunt from the console.
            ClearLastTaunt();

            // Now see if our new message is within acceptable limits
            if (Console.BufferWidth > taunt.Length)
            {
                int xPosition = 1;
                int yPosition = 21;
                Console.SetCursorPosition(xPosition, yPosition);
                Console.Out.Write(taunt);

                lastTauntLength = taunt.Length;
            }
            else
                throw new ArgumentOutOfRangeException("message");
        }

        /// <summary>
        /// Remove the last computer taunt from the console
        /// </summary>
        public override void ClearLastTaunt()
        {
            if (lastTauntLength > 0 && lastTauntLength < Console.BufferWidth)
            {
                int yPosition = 21;
                Console.SetCursorPosition(1, yPosition);
                Console.Out.Write(clearLine.Substring(0, lastTauntLength));
            }
        }
    }
}
