using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsTest
{
    class CardDeck
    {
        private List<Card> cards = new List<Card>();

        // Default constructor is protected
        protected CardDeck() { }

        // Construct a new deck with the given cards
        // ( also protected )
        protected CardDeck( List<Card> cards_p )
        {
            // Add the given list of cards to our empty deck
            cards.AddRange(cards_p);
        }

        // Produce a purposely empty deck!
        public static CardDeck BuildEmptyDeck()
        {
            return new CardDeck();
        }

        // Build a new card deck with 52 cards in order.
        // Static, so users have to call this or SplitDeck
        // to get a new deck (or decks) of cards.
        public static CardDeck BuildNewDeck()
        {
            CardDeck deck = new CardDeck();

            for (int suitIndex = 0; suitIndex < Card.suitSymbols.Count(); suitIndex++)
                for (int valueIndex = 0; valueIndex < Card.valueSymbols.Count(); valueIndex++)
                {
                    // Create the card and add it to the list/deck
                    Card c = new Card(suitIndex, valueIndex);
                    deck.cards.Add(c);
                }

            return deck;
        }

        // Use a simple random number generator to form a shuffling routine
        public void Shuffle()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            List<Card> shuffledDeck = new List<Card>();

            // NOTE: The following loop pulls cards at random from one
            // list and puts them into another until there is only
            // one card left in the original list
            while(cards.Count() > 1)
            {
                int sourceIndex = rand.Next() % cards.Count();
                shuffledDeck.Add(cards[sourceIndex]);
                cards.RemoveAt(sourceIndex);
            }

            // Then we add the 51 cards from the new (shuffled) list
            // back to the original list where the last remaining card is
            // already to make up 52 again.
            cards.AddRange(shuffledDeck);
        }

        // Split this deck into two equal decks, removing the
        // cards from this deck.
        public void SplitDeck( out CardDeck deckA, out CardDeck deckB )
        {
            if (cards.Count() < 2) throw new ArgumentOutOfRangeException("Deck is too small to split!");
            int half = cards.Count() / 2;

            deckA = new CardDeck(cards.GetRange(0, half - 1));
            deckB = new CardDeck(cards.GetRange(half, half - 1));

            // Remove the cards from our own list.
            cards = new List<Card>();
        }

        public Card DrawCard()
        {
            if (deckSize < 1)
                throw new NotImplementedException();

            Card ret = cards[0]; cards.RemoveAt(0);
            return ret;
        }

        public void AddCard(Card c)
        {
            cards.Add(c);
        }

        public int deckSize
        {
            get
            {
                return cards.Count();
            }
        }
    }
}
