using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsTest
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

            r.RenderPlayerStatus(1, "Computer", computerDeck.deckSize);
            r.RenderPlayerStatus(2, "Player", playerDeck.deckSize);

            Card computerCard = computerDeck.DrawCard();
            Card playerCard = playerDeck.DrawCard();

            r.RenderPlayerCard(computerCard, 1, 0);
            warDeck.AddCard(computerCard);

            r.RenderPlayerCard(playerCard, 2, 0);
            warDeck.AddCard(playerCard);

            Console.In.ReadLine();
        }
    }
}
