using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    /// <summary>
    /// Represents a deck or partial deck of playing cards
    /// </summary>
    class CardDeck
    {
        /// <summary>
        /// The internal, private list of playing cards
        /// </summary>
        private List<Card> cards = new List<Card>();

        /// <summary>
        /// The number of cards in the deck
        /// </summary>
        public int deckSize
        {
            get
            {
                return cards.Count();
            }
        }

        /// <summary>
        /// Default constructor is protected to force self
        /// instantiation
        /// </summary>
        protected CardDeck() { }

        /// <summary>
        /// Protected Constructor to build a new deck with the given list of cards 
        /// </summary>
        /// <param name="cards_p">
        /// A list of cards to build the deck from
        /// </param>
        protected CardDeck( List<Card> cards_p )
        {
            // Add the given list of cards to our empty deck
            cards.AddRange(cards_p);
        }

        /// <summary>
        /// Purposely build an empty deck
        /// </summary>
        /// <returns>An empty deck of cards</returns>
        public static CardDeck BuildEmptyDeck()
        {
            return new CardDeck();
        }

        /// <summary>
        /// Build a standard deck with 52 cards in order.
        /// </summary>
        /// <returns>A standard deck of cards</returns>
        public static CardDeck BuildNewDeck()
        {
            CardDeck deck = new CardDeck();

            for (int suitIndex = 0; suitIndex < Card.suitSymbols.Count(); suitIndex++)
                for (int valueIndex = 0; valueIndex < Card.valueLimit; valueIndex++)
                {
                    // Create the card and add it to the list/deck
                    Card c = new Card(suitIndex, valueIndex);
                    deck.cards.Add(c);
                }

            return deck;
        }

        /// <summary>
        /// Shuffle the deck of cards using a simple RNG routine
        /// </summary>
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

        /// <summary>
        /// Split this deck into two equal decks, removing the
        /// cards from this deck. 
        /// </summary>
        /// <param name="deckA">First deck to receive half of this deck</param>
        /// <param name="deckB">Second deck to receive the rest of this deck</param>
        public void SplitDeck( out CardDeck deckA, out CardDeck deckB )
        {
            if (cards.Count() < 2) throw new ArgumentOutOfRangeException("Deck is too small to split!");
            int half = cards.Count() / 2;

            deckA = new CardDeck(cards.GetRange(0, half));
            deckB = new CardDeck(cards.GetRange(half, half));

            // Remove the cards from our own list.
            cards = new List<Card>();
        }

        /// <summary>
        /// Draw and remove the first card from this deck
        /// </summary>
        /// <returns>The first card from the deck</returns>
        public Card DrawCard()
        {
            if (deckSize < 1)
                throw new NotImplementedException();

            Card ret = cards[0]; cards.RemoveAt(0);
            return ret;
        }

        /// <summary>
        /// Adds a single card to the end of this deck
        /// </summary>
        /// <param name="c">The card to add to the end of the deck</param>
        public void AddCard(Card c)
        {
            cards.Add(c);
        }

        /// <summary>
        /// Add the given deck of cards to this deck
        /// </summary>
        /// <param name="sourceDeck">The deck to add to this one</param>
        public void AddDeck( ref CardDeck sourceDeck )
        {
            // Move all the cards in sourceDeck into our own deck
            // and remove them from sourceDeck
            cards.AddRange(sourceDeck.cards);
            sourceDeck.cards = new List<Card>();
        }
    }
}
