using System;
using Cards;

namespace Hands
{
    public interface IGivingCard
    {
        public event Action<ICardData> CardGiven;
        
        public void GiveCard(ICardData data);
    }
}