using System;
using System.Collections.Generic;
using Cards;
using States;
using UnityEngine;

namespace Tables
{
    public class Table
    {
        private List<ICardData> _lowerCards;
        private ICardData[] _upperCards;
        private readonly Suit _trump;

        public event Action<ICardData> OnCardLaid;
        public event Action<ICardData, int> OnCardBeaten;
        
        public Table(Suit trump)
        {
            _trump = trump;
            
            _lowerCards = new List<ICardData>();
            _upperCards = new ICardData[6];
        }
        
        public bool TryTakeCard(ICardData lowerCard, StatusPlayer status)
        {
            if (status != StatusPlayer.Thrower || _lowerCards.Count == 6)
                return false;
            _lowerCards.Add(lowerCard);
            OnCardLaid?.Invoke(lowerCard);
            return true;
        }

        public bool TryTakeCard(ICardData lowerCard, ICardData upperCard, StatusPlayer status)
        {
            if (status != StatusPlayer.Defender || CheckPar(lowerCard, upperCard) == false)
                return false;
            var index = _lowerCards.IndexOf(lowerCard);
            _upperCards[index] = upperCard;
            OnCardBeaten?.Invoke(upperCard, index);
            return true;
        }
        
        private bool CheckPar(ICardData lowerCard, ICardData upperCard)
        {
            if (lowerCard.Suit == upperCard.Suit ) 
                return lowerCard.Seniority < upperCard.Seniority;
            return upperCard.Suit == _trump;
        }
    }
}