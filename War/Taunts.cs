using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    /// <summary>
    /// Just a collection of playful taunts to make the game less tedious
    /// </summary>
    class Taunts
    {
        /// <summary>
        /// Taunts to be used when things are about even between the contestants
        /// </summary>
        static string[] ResultsEven =
        {
            "Surely a computer can beat a human at cards!!",
            "Don't worry.  It will be over soon.",
            "This will be fun!",
            "I'm going to enjoy winning!",
            "We should do this more often.",
            "I hope you don't mind losing to a computer.",
            "When was the last time you played this?",
            "I don't mind taking all the cards...",
            "This could take a while.",
            "I love this game!",
            "Should I be worried?",
            "You don't look so tough.",
            "I've done this hundreds of times.",
            "I'll bet this doesn't take long.",
            "I'll try not to gloat."
        };

        /// <summary>
        /// Taunts to be used when the computer has at least 66% of the cards
        /// </summary>
        static string[] ComputerWinning =
        {
            "I always knew humands were easy to beat.",
            "It's okay, you're only human.",
            "I've got nothing to worry about here.",
            "Wow!  I've got a lot of cards.",
            "I'm glad I'm not in your shoes, human.",
            "Just keep the cards coming.",
            "I sure do like winning.",
            "Almost done, just a few more cards to go.",
            "You don't mind if I gloat a little, do you?",
            "I do like the weight of all these cards.",
            "Should I share some of these cards with you?",
            "C'mon, at least put up a fight.",
            "Don't worry.  I'm a professional.",
            "Fortunately, I never cheat...much.",
            "It's pure skill, you know.",
            "You're taking a long time to beat."
        };

        /// <summary>
        /// Taunts to be used when the computer has fewer than 33% of the cards
        /// </summary>
        static string[] ComputerLosing =
        {
            "Hmmm, you're getting awful lucky.",
            "Wish I had that many cards...",
            "I don't mind saying I'm a little worried now.",
            "Can you share some of those cards with me?",
            "Maybe we should play a nice game of Go Fish.",
            "I'll bet all those cards are hard to handle, huh?",
            "It's just a game.  It's not important.",
            "Show some pity.  I've got a lot of files to manage too!",
            "Listen, cann't you give a computer a break?",
            "You're pretty good at this!",
            "I won't make it easy for you.",
            "I can do this all day.",
            "You haven't won yet!!",
            "As long as I have even one card...",
            "Don't make me beg for cards.",
            "You'll make me afraid of humans soon.",
            "I think I underestimated your skills.",
            "Can you see my cards?",
            "I think I need a break."
        };

        /// <summary>
        /// Taunts to be issued when the computer wins a round
        /// </summary>
        static string[] ComputerWins =
        {
            "Yay me!",
            "Superior electronics...",
            "I'd be more graceful if I had a heart",
            "It's all in the wrist",
            "Good try",
            "Better luck next time",
            "Don't mind if I do",
            "Thanks for the cards!",
            "Only human...",
            "Nice try, human.",
            "It won't get any easier.",
            "Thank you.",
            "Remember, it's just a game.",
            "Like taking candy from a baby.",
            "I'm having fun, how 'bout you?",
            "I'm doing this with only half my CPU!"
        };

        /// <summary>
        /// Taunts to be issued when the computer loses a round
        /// </summary>
        static string[] ComputerLoses =
        {
            "That was luck!",
            "Next time, I won't be so nice.",
            "You make it look easy.",
            "Ouch!",
            "Fine, take 'em.",
            "I had too many cards anyway.",
            "Have some pity on me.",
            "If I only had a brain.",
            "Are you sure you're not cheating?!",
            "Don't be smug.",
            "Even humans win sometimes.",
            "I'm just softening you up.",
            "Don't trust in luck, human.",
            "I think you're looking at my cards.",
            "Time to use my good hand.",
            "That's an awful lucky hand you got there.",
            "Oh poo!",
            "I wasn't paying attention that time.",
            "I got distracted, that's all.",
            "Leave me a little dignitity, will ya?",
            "It's not nice to pick on computers.",
            "Must remember the 1st rule of robotics..."
        };

        /// <summary>
        /// Computer status for decision making
        /// </summary>
        public enum computerStatus
        {
            even,
            winning,
            losing
        };

        /// <summary>
        /// Retrieve a taunt based on the given computer winning status
        /// </summary>
        /// <param name="s">A status of even, winning or losing</param>
        /// <returns>An appropriate taunt, based on the given status</returns>
        public static string GetComputerTaunt( computerStatus s)
        {
            string[] taunts = ResultsEven;
            switch(s)
            {
                case computerStatus.winning:
                    taunts = ComputerWinning;
                    break;
                case computerStatus.losing:
                    taunts = ComputerLosing;
                    break;
            }

            int index = DateTime.Now.Millisecond % taunts.Length;
            return taunts[index];
        }

        /// <summary>
        /// Round outcomes from the computer's point of view
        /// </summary>
        public enum outcomes
        {
            computerWins,
            computerLoses
        }

        /// <summary>
        /// Retrieve an appropriate response when the computer wins or loses a round
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetComputerResponse( outcomes o )
        {
            string[] responses = o == outcomes.computerWins ? ComputerWins : ComputerLoses;

            int index = DateTime.Now.Millisecond % responses.Length;
            return responses[index];
        }
    }
}
