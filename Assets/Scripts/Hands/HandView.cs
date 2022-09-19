using System;
using System.Collections.Generic;
using Cards;
using ObjectPool;
using UnityEngine;
using Views;

namespace Hands
{
    public class HandView : View<CardInHand>
    {
        [SerializeField] private RectTransform _cardYPositionInHand;
        [Range(0f, 1f)][SerializeField] private float _screenPadding;
        
        private float _screenWidth;
        private float? _cardWidth;
        
        public event Action<ICardData> ToTakeTried;
        public event Action<ICardData, ICardData> ToBeatTried;

        protected override void Init()
        {
            _pool = new PoolMono<CardInHand>(52, _cardPrefab, _cardsContainer.transform);
            _cards = new List<CardInHand>();

            _screenWidth = Screen.width * _screenPadding;
        }

        protected override void SubscribeCardEvent(CardInHand card)
        {
            card.ToLayTried += TryLay;
            card.ToBeatTried += TryBeat;
        }

        protected override void UnsubscribeFromCardEvent(CardInHand card)
        {
            card.ToLayTried -= TryLay;
            card.ToBeatTried += TryBeat;
        }

        protected sealed override void DrawCards()
        {
            if (_cardWidth == null)
            {
                var rectTransform  = _cards[0]?.GetComponent<RectTransform>();
                _cardWidth = rectTransform.rect.width * rectTransform.localScale.x;
            }
            var count = _cards.Count;
            var width = _cardWidth * count < _screenWidth ? _cardWidth.Value : _screenWidth / count;
            for (var i = 0; i < count; i++)
            {
                var xPosition = GetCardPosition(i, count, width);
                _cards[i].transform.localPosition = new Vector3(xPosition, _cardYPositionInHand.anchoredPosition.y);
            }
        }

        private float GetCardPosition(int i, int count, float width)
        {
            return (i - (count - 1 )  * 0.5f) * width;
        }

        private void TryLay(ICardData data)
        {
            ToTakeTried?.Invoke(data);
        }
        
        private void TryBeat(ICardData data1, ICardData data2)
        {
            ToBeatTried?.Invoke(data1, data2);
        }
    }
}