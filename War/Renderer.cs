using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    abstract class Renderer
    {
        /// <summary>
        /// A simple pause routine
        /// </summary>
        /// <param name="mS">How long to pause</param>
        public void Pause( int mS)
        {
            System.Threading.Thread.Sleep(mS);
        }

        /// <summary>
        /// Render a card with animation or delay to provide temporal context
        /// </summary>
        /// <param name="card">The card to render</param>
        /// <param name="playerNumber">Player number (1 or 2)</param>
        /// <param name="position">Player relative position of the card (0-4)</param>
        public void PlayCard(Card card, int playerNumber, int position)
        {
            PlayCard(card, playerNumber, position, false);
        }

        /// <summary>
        /// Render a card with animation, sound or delay to provide temporal context
        /// </summary>
        /// <param name="card">The card to render</param>
        /// <param name="playerNumber">Player number (1 or 2)</param>
        /// <param name="position">Player relative position of the card (0-4)</param>
        /// <param name="faceDown">Whether the back of the card should be rendered</param>
        public abstract void PlayCard(Card card, int playerNumber, int position, bool faceDown);

        /// <summary>
        /// Render a card at the given logical player position (0-4) 
        /// </summary>
        /// <param name="card">The card to render</param>
        /// <param name="playerNumber">Player number (1 or 2)</param>
        /// <param name="position">The player relative position of the card (0-4)</param>
        public void RenderPlayerCard(Card card, int playerNumber, int position)
        {
            RenderPlayerCard(card, playerNumber, position, false);
        }

        // 
        /// <summary>
        /// Render a card at the given logical player position (0-4) 
        /// </summary>
        /// <param name="card">The card to render</param>
        /// <param name="playerNumber">The player number (1 or2)</param>
        /// <param name="position">The player relative position of the card (0-4)</param>
        /// <param name="faceDown">Whether the back of the card should be rendered</param>
        public abstract void RenderPlayerCard(Card card, int playerNumber, int position, bool faceDown);

        /// <summary>
        /// Render a card at the absolute position given (top-left)
        /// </summary>
        /// <param name="card">The card to render</param>
        /// <param name="xPosition">The column of the top left corner</param>
        /// <param name="yPosition">The row of the top left corner</param>
        public void RenderCard(Card card, int xPosition, int yPosition)
        {
            RenderCard(card, xPosition, yPosition, false);
        }

        /// <summary>
        /// Render a card at the absolute position given (top-left)
        /// </summary>
        /// <param name="card">The card to render</param>
        /// <param name="xPosition">The column of the top left corner</param>
        /// <param name="yPosition">The row of the top left corner</param>
        /// <param name="faceDown">Whether the back of the card should be rendered</param>
        public abstract void RenderCard(Card card, int xPosition, int yPosition, bool faceDown);

        /// <summary>
        /// Clear or reset the playing surface
        /// </summary>
        public abstract void ClearSurface();
        
        /// <summary>
        /// Render a simple player message
        /// </summary>
        /// <param name="message">The message to render</param>
        public abstract void RenderMessage(String message);

        /// <summary>
        /// Remove a previous message from the display surface
        /// </summary>
        public abstract void ClearLastMessage();

        /// <summary>
        /// Render the player status
        /// </summary>
        /// <param name="playerNumber">Which player is being rendered (1 or2)</param>
        /// <param name="playerName">The name of the player</param>
        /// <param name="deckSize">The number of cards the player has left</param>
        public abstract void RenderPlayerStatus(int playerNumber, string playerName, int deckSize);

        /// <summary>
        /// Render game winner
        /// </summary>
        /// <param name="winner">The name of the winner</param>
        /// <param name="duration">How long the play took</param>
        /// <param name="rounds">How many rounds the play took</param>
        public abstract void RenderGameResult(string winner, string duration, int rounds);

        /// <summary>
        /// Render how many cards are at risk this round
        /// </summary>
        /// <param name="cardsAtWar">Number of cards at risk</param>
        public abstract void RenderCombatantStats(int cardsAtWar);

        /// <summary>
        /// Render Computer Taunt 
        /// </summary>
        /// <param name="message">The message to be rendered</param>
        public abstract void RenderTaunt(String message);

        /// <summary>
        /// Remove a previous taunt from the display surface 
        /// </summary>
        public abstract void ClearLastTaunt();
    }
}
