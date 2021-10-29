using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsTest
{
    abstract class Renderer
    {
        // Render a card at the given logical player position (1-5)
        public abstract void RenderPlayerCard(Card card, int playerNumber, int position);

        // Render a card at the given position (top-left)
        public abstract void RenderCard(Card card, int xPosition, int yPosition);

        // Clear the playing surface
        public abstract void ClearSurface();
        
        // Render a message to the player(s)
        public abstract void RenderMessage(String message);

        // Render player status
        public abstract void RenderPlayerStatus(int playerNumber, string playerName, int deckSize);

        // Render game winner
        public abstract void RenderGameResult(string winner);
    }
}
