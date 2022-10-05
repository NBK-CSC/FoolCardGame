using System;
using System.Collections.Generic;
using Cards;
using States;

namespace Tables
{
    public class Table : ITakingCard, ICardChangeable
    {
        private List<ICardData> _lowerCards;
        private ICardData[] _upperCards;

        public Suit Trump { get; set; }

        public event Action<ICardData> OnCardLaid;
        public event Action<ICardData, int> OnCardBeaten;
        public event Action<int, int> OnCardsChanged;
        
        public Table()
        {
            _lowerCards = new List<ICardData>();
            _upperCards = new ICardData[6];
        }

        public bool TryTakeCard(ICardData lowerCard, StatusPlayer status)
        {
            if ((status != StatusPlayer.ThrowerActivating
                 && status != StatusPlayer.ThrowerWaiting)
                || _lowerCards.Count == 6)
                return false;
            _lowerCards.Add(lowerCard);
            
            OnCardLaid?.Invoke(lowerCard);
            OnCardsChanged?.Invoke(_lowerCards.Count, _upperCards.Length);
            
            return true;
        }

        public bool TryTakeCard(ICardData lowerCard, ICardData upperCard, StatusPlayer status)
        {
            if (status != StatusPlayer.DefenderActivating
                && status != StatusPlayer.DefenderWaiting
                || CheckPar(lowerCard, upperCard) == false)
                return false;
            
            var index = _lowerCards.IndexOf(lowerCard);
            _upperCards[index] = upperCard;
            
            OnCardBeaten?.Invoke(upperCard, index);
            OnCardsChanged?.Invoke(_lowerCards.Count, _upperCards.Length);
            
            return true;
        }
        
        private bool CheckPar(ICardData lowerCard, ICardData upperCard)
        {
            if (lowerCard.Suit == upperCard.Suit ) 
                return lowerCard.Seniority < upperCard.Seniority;
            return upperCard.Suit == Trump;
        }
    }
}