using System;
using System.Collections.Generic;
using System.Linq;
using Cards;

namespace Dealers
{
    public class Dealer: IGetterCard
    {
        private Queue<ICardData> _pack;
        private List<ICardData> _cardsData;
        private int _numberCardsInPack;
        private Random _random = new Random();

        public ICardData TrumpCard { get; private set; }

        public Dealer(int numberCardsInPack, IEnumerable<ICardData> cardsData)
        {
            _numberCardsInPack = numberCardsInPack;
            _cardsData = cardsData.ToList();
            _pack=new Queue<ICardData>();
            
            ShuffleCards();
        }

        private ICardData RandomlyPlaceCard()
        {
            return _cardsData[_random.Next(_cardsData.Count)];
        }

        private void ShuffleCards()
        {
            for(var i=0;i<_numberCardsInPack;i++)
                _pack.Enqueue(RandomlyPlaceCard());
            TrumpCard = _pack.Dequeue();
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