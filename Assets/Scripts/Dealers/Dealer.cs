using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;
using Random = System.Random;

namespace Dealers
{
    public class Dealer: IGetterCard
    {
        private Queue<ICardData> _pack;
        private List<ICardData> _cardsData;
        private int _numberCardsInPack;
        private Random _random = new Random();

        public ICardData TrumpCard { get; private set; }
        public event Action<ICardData> OnTrumpCardSet;

        public Dealer(int numberCardsInPack, IEnumerable<ICardData> cardsData)
        {
            _numberCardsInPack = numberCardsInPack;
            _cardsData = new List<ICardData>(cardsData);
            _pack=new Queue<ICardData>();
        }

        private ICardData RandomlyPlaceCard()
        {
            var index = _random.Next(_cardsData.Count);
            var removeCard= _cardsData[index];
            _cardsData.RemoveAt(index);
            return removeCard;
        }

        public void ShuffleCards()
        {
            for(var i=0;i<_numberCardsInPack;i++)
                _pack.Enqueue(RandomlyPlaceCard());
            TrumpCard = _pack.Dequeue();
            OnTrumpCardSet?.Invoke(TrumpCard);
            _pack.Enqueue(TrumpCard);
        }

        public bool TryGiveCard(out ICardData card)
        {
            if (_pack.Count > 0)
            {
                card = _pack.Dequeue();
                return true;
            }
            card = null;
            return false;
        }
    }
}