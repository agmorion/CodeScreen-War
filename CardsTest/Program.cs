using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = new System.Text.UTF8Encoding();

            CardDeck wholeDeck = CardDeck.BuildNewDeck();
            CardDeck computerDeck, playerDeck;
            CardDeck warDeck = CardDeck.BuildEmptyDeck();

            wholeDeck.Shuffle();
            wholeDeck.SplitDeck(out computerDeck, out playerDeck);

            ConsoleRenderer r = new ConsoleRenderer();

            r.RenderMessage("Drawing...");

            Card computerCard = computerDeck.DrawCard();
            Card playerCard = playerDeck.DrawCard();

            r.PlayCard(computerCard, 1, 0);
            warDeck.AddCard(computerCard);
            r.RenderCombatantStats(warDeck.deckSize);

            r.RenderTaunt(Taunts.GetComputerTaunt(Taunts.computerStatus.even));
            Console.ReadLine();
            r.ClearLastTaunt();

            r.PlayCard(playerCard, 2, 0);
            warDeck.AddCard(playerCard);
            r.RenderCombatantStats(warDeck.deckSize);

            r.RenderMessage("!!War!!");
            System.Threading.Thread.Sleep(2000);

            r.RenderMessage("Drawing...");

            for ( int x = 1; x < 4; x++ )
            {
                computerCard = computerDeck.DrawCard();
                playerCard = playerDeck.DrawCard();

                r.PlayCard(computerCard, 1, x, true);
                warDeck.AddCard(computerCard);
                r.RenderCombatantStats(warDeck.deckSize);

                r.PlayCard(playerCard, 2, x, true);
                warDeck.AddCard(playerCard);
                r.RenderCombatantStats(warDeck.deckSize);
            }

            computerCard = computerDeck.DrawCard();
            playerCard = playerDeck.DrawCard();

            r.PlayCard(computerCard, 1, 4);
            warDeck.AddCard(computerCard);
            r.RenderCombatantStats(warDeck.deckSize);

            r.PlayCard(playerCard, 2, 4);
            warDeck.AddCard(playerCard);
            r.RenderCombatantStats(warDeck.deckSize);

            r.RenderPlayerStatus(1, "Computer", computerDeck.deckSize);
            r.RenderPlayerStatus(2, "Player", playerDeck.deckSize);

            r.RenderMessage(
                playerCard.cardValue > computerCard.cardValue ? "Player Wins!!" :
                computerCard.cardValue > playerCard.cardValue ? "Computer Wins" :
                "!!War!!");

            r.RenderTaunt(Taunts.GetComputerResponse(
                playerCard.cardValue > computerCard.cardValue ?
                Taunts.outcomes.computerLoses : Taunts.outcomes.computerWins));

            Console.In.ReadLine();
        }
    }
}
