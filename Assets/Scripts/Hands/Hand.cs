using System;
using System.Collections.Generic;
using Cards;
using Players;
using States;
using Tables;

namespace Hands
{
    public class Hand : IGivingCard
    {
        private List<ICardData> _cards;
        private ITable _table;
        private IPlaying _playing;
        
        public event Action<ICardData> CardGiven;
        public event Action<ICardData> CardTakenAway;

        public Hand(IPlaying playing, ITable table)
        {
            _playing = playing;
            _table = table;

            _cards = new List<ICardData>();
        }

        public void GiveCard(ICardData data)
        {
            _cards.Add(data);
            CardGiven?.Invoke(data);
        }

        public void TakeAwayCard(ICardData data)
        {
            if (!_table.TryTakeCard(data,_playing.State))
                return;
            _cards.Remove(data);
            CardTakenAway?.Invoke(data);
        }
        
        public void TakeAwayCard(ICardData data, ICardData ourCardData)
        {
            if (!_table.TryTakeCard(data, ourCardData, _playing.State))
                return;
            _cards.Remove(ourCardData);
            CardTakenAway?.Invoke(ourCardData);
        }

        public bool IsNeedGetCard(out int number)
        {
            number = 6 - _cards.Count; 
            return _cards.Count < 6;
        }
    }
}