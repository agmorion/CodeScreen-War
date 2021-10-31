using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    /// <summary>
    /// Because .NET doesn't supply this directly, we 
    /// use interop to call out to the kbhit C function
    /// </summary>
    public class Win32Interop {
        [DllImport("crtdll.dll")]   public static extern int _kbhit();
    }

    /// <summary>
    /// Manages the cycle of game play for an entire game session
    /// </summary>
    class PlayCycle
    {
        /// <summary>
        /// The sum of rounds a game is played over
        /// </summary>
        private int totalRounds;

        /// <summary>
        /// The outcome of a single round
        /// </summary>
        enum RoundResults
        {
            undefined,
            computerWins,
            playerWins,
            War
        };

        /// <summary>
        /// The constant name of the computer player
        /// </summary>
        public const string computerPlayerName = "Computer";

        /// <summary>
        /// The name of the human player
        /// </summary>
        public string playerName { get; set; }

        /// <summary>
        /// The renderer to be used for the game
        /// </summary>
        private Renderer renderer = new ConsoleRenderer();

        /// <summary>
        /// The computer's deck of cards
        /// </summary>
        private CardDeck computerDeck;

        /// <summary>
        /// The players deck of cards
        /// </summary>
        private CardDeck playerDeck;

        /// <summary>
        /// A method to wait for a console keystroke
        /// </summary>
        /// <param name="secondsToWait">How long to wait in seconds or -1 (forever)</param>
        /// <returns>Whether a keystroke was received</returns>
        private bool WaitForKeyStroke(int secondsToWait )
        {
            bool ret = false;

            if (Program.runToEnd)
                ret = true;
            else
            {

                DateTime startTime = DateTime.Now;
                while (Win32Interop._kbhit() == 0)
                {
                    // If secondsToWait is -1, wait indefinitely
                    if (-1 == secondsToWait ||
                        (DateTime.Now - startTime).TotalSeconds < secondsToWait)
                        System.Threading.Thread.Sleep(50);
                    else
                        break;
                }
                while (Win32Interop._kbhit() != 0)
                {
                    Console.ReadKey(true);
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// Begin a new game
        /// </summary>
        public void BeginPlay()
        {
            // Clear the playing surface
            renderer.ClearSurface();

            // Make a deck of cards
            CardDeck cleanDeck = CardDeck.BuildNewDeck();

            //Shuffle the cards
            renderer.RenderMessage("Shuffling the cards...");
            cleanDeck.Shuffle();
            renderer.Pause(2000);

            // Deal the cards to the players
            renderer.RenderMessage("Dealing the cards...");
            cleanDeck.SplitDeck(out computerDeck, out playerDeck);
            renderer.Pause(2000);

            DateTime startTime = DateTime.Now;

            Play();

            renderer.RenderGameResult(
                computerDeck.deckSize == 0 ? playerName : "Computer",
                DateTime.Now - startTime,
                totalRounds
                );
        }

        /// <summary>
        /// See if one or the other players has no cards left
        /// </summary>
        private bool isGameOver
        {
            get
            {
                return computerDeck.deckSize == 0 || playerDeck.deckSize == 0;
            }
        }

        /// <summary>
        /// Cycle rounds until there is a winner
        /// </summary>
        private void Play()
        {
            totalRounds = 0;

            while ( !isGameOver )
            {
                // Play a round of the game
                PlayRound();
            }

            if (computerDeck.deckSize == 0)
                renderer.RenderTaunt("Congratulations, human.  I'll beat you next time.");
            else
                renderer.RenderTaunt("You put up a good fight, for a human.  Better luck next time.");

            WaitForKeyStroke(10);
        }

        /// <summary>
        /// Property that indicates the taunt status of the computer
        /// </summary>
        Taunts.computerStatus computerTauntStatus
        {
            get
            {
                int computerCardPercentage = computerDeck.deckSize * 100 / 52;
                return
                    computerCardPercentage < 33 ? Taunts.computerStatus.losing :
                    computerCardPercentage < 66 ? Taunts.computerStatus.even :
                    Taunts.computerStatus.winning;
            }
        }

        /// <summary>
        /// Issue a computer taunt at the beginning of the round
        /// </summary>
        private void IssueRoundStartTaunt()
        {
            // Render and wait for the user to press a key
            renderer.RenderTaunt(Taunts.GetComputerTaunt(computerTauntStatus));
            WaitForKeyStroke(5);
            renderer.ClearLastTaunt();
        }

        /// <summary>
        /// At the end of the round, issue a computer response 
        /// based on whether the computer won the round or not
        /// </summary>
        /// <param name="computerWonOrLost">
        /// A Taunts.outcomes value indicating whether the computer won or lost
        /// </param>
        private void IssueRoundResponse(Taunts.outcomes computerWonOrLost )
        {
            // Render and wait for the user to press a key
            renderer.RenderTaunt(Taunts.GetComputerResponse(computerWonOrLost));
            if (!WaitForKeyStroke(5))
            {
                renderer.RenderMessage("Press a key to continue");
                WaitForKeyStroke(-1);
                renderer.ClearLastMessage();
            }
            renderer.ClearLastTaunt();
        }

        /// <summary>
        /// A single round of play
        /// </summary>
        private void PlayRound()
        {
            CardDeck warDeck = CardDeck.BuildEmptyDeck();

            totalRounds++;

            // Clear the game surface
            renderer.ClearSurface();

            RoundResults result = RoundResults.undefined;
            do
            {
                int cardPosition = result == RoundResults.War ? 4 : 0;

                // Show the player status
                renderer.RenderPlayerStatus(1, computerPlayerName, computerDeck.deckSize);
                renderer.RenderPlayerStatus(2, playerName, playerDeck.deckSize);

                // Issue computer taunt.
                IssueRoundStartTaunt();

                // Now draw the cards.
                Card computerCard = computerDeck.DrawCard();
                Card playerCard = playerDeck.DrawCard();

                // Show the player and computer cards
                renderer.PlayCard(computerCard, 1, cardPosition);
                warDeck.AddCard(computerCard);
                renderer.RenderCombatantStats(warDeck.deckSize);

                renderer.PlayCard(playerCard, 2, cardPosition);
                warDeck.AddCard(playerCard);
                renderer.RenderCombatantStats(warDeck.deckSize);

                // Now determine the result
                result =
                    playerCard.cardValue > computerCard.cardValue ? RoundResults.playerWins :
                    computerCard.cardValue > playerCard.cardValue ? RoundResults.computerWins :
                    RoundResults.War;

                // Render an appropriate message
                renderer.RenderMessage(
                    result == RoundResults.playerWins ? String.Format("{0} Wins!!", playerName) :
                    result == RoundResults.computerWins ? "Computer Wins" :
                    "!!War!!");

                if ( result == RoundResults.War)
                {
                    // Pause for a moment to consider the implications of war
                    renderer.Pause(2000);

                    // See if we need to adjust based on whether one side or the
                    // other has fewer than four cards left
                    if (playerDeck.deckSize < 4)
                    {
                        result = RoundResults.computerWins;
                        renderer.RenderMessage(String.Format("{0} is out of cards!!!", playerName ));
                        renderer.Pause(2000);
                        renderer.RenderMessage("Computer Wins");
                        warDeck.AddDeck( ref playerDeck);
                    }
                    else if (computerDeck.deckSize < 4)
                    {
                        result = RoundResults.playerWins;
                        renderer.RenderMessage("Computer is out of cards!!!");
                        renderer.Pause(2000);
                        renderer.RenderMessage(String.Format("{0} Wins!!", playerName));
                        warDeck.AddDeck( ref computerDeck);
                    }
                    else
                    {
                        // Deal out three cards from each player to the casualty space
                        for (int x = 1; x < 4; x++)
                        {
                            computerCard = computerDeck.DrawCard();
                            playerCard = playerDeck.DrawCard();

                            renderer.PlayCard(computerCard, 1, x, true);
                            warDeck.AddCard(computerCard);
                            renderer.RenderCombatantStats(warDeck.deckSize);

                            renderer.PlayCard(playerCard, 2, x, true);
                            warDeck.AddCard(playerCard);
                            renderer.RenderCombatantStats(warDeck.deckSize);
                        }
                    }
                }
            } while (result == RoundResults.War);

            // Now add the war deck to the winners pile
            if (result == RoundResults.computerWins)
                computerDeck.AddDeck(ref warDeck);
            else
                playerDeck.AddDeck(ref warDeck);

            // Update the player status
            renderer.RenderPlayerStatus(1, computerPlayerName, computerDeck.deckSize);
            renderer.RenderPlayerStatus(2, playerName, playerDeck.deckSize);

            // Now render the computer response based on whether they won or lost.
            IssueRoundResponse(result == RoundResults.computerWins ? Taunts.outcomes.computerWins : Taunts.outcomes.computerLoses);
        }
    }
}
