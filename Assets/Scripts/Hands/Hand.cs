using System;
using System.Collections.Generic;
using Cards;
using Players;
using Tables;

namespace Hands
{
    public class Hand : IGivingCard
    {
        private ITakingCard _takingCard;
        private IGivingState _player;

        public List<ICardData> Cards { get; private set; }
        
        public event Action<ICardData> CardGiven;
        public event Action<ICardData> CardTakenAway;

        public Hand(IGivingState player, ITakingCard takingCard)
        {
            _player = player;
            _takingCard = takingCard;

            Cards = new List<ICardData>();
        }

        public void GiveCard(ICardData data)
        {
            Cards.Add(data);
            CardGiven?.Invoke(data);
        }

        public void TakeAwayCard(ICardData data)
        {
            if (!_takingCard.TryTakeCard(data,_player.Status))
                return;
            Cards.Remove(data);
            CardTakenAway?.Invoke(data);
        }
        
        public void TakeAwayCard(ICardData data, ICardData ourCardData)
        {
            if (!_takingCard.TryTakeCard(data, ourCardData, _player.Status))
                return;
            Cards.Remove(ourCardData);
            CardTakenAway?.Invoke(ourCardData);
        }

        public bool IsNeedGetCard(out int number)
        {
            number = 6 - Cards.Count; 
            return Cards.Count < 6;
        }
    }
}