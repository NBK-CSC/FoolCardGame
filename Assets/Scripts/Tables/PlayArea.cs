using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using DragHandlers;
using ObjectPool;
using UnityEngine;
using Views;

namespace Tables
{
    [RequireComponent(typeof(TableDetect))]
    public class PlayArea : ViewReceivingCards<CardInTable>
    {
        [SerializeField] private int _numberColumns;
        [SerializeField] private float _xDistanceBetweenCards;
        [SerializeField] private float _yDistanceBetweenCards;
        [SerializeField] private float _xPaddingUpperCard;
        [SerializeField] private float _angleRotationBeatingCard;
        
        
        private CardInTable[] _upperCards;
        
        protected override void Init()
        {
            _pool = new PoolMono<CardInTable>(12, _cardPrefab, _cardsContainer.transform);
            _cards = new List<CardInTable>();

            _upperCards= new CardInTable[6];
        }

        public void AddCard(ICardData data, int index)
        {
            if (data == null)
                throw new ArgumentNullException();
            var newCard = _pool.GetFreeObject();
            newCard.Init(data);
            SubscribeCardEvent(newCard);
            _upperCards[index] = newCard;
            DrawCards();
        }

        protected override void SubscribeCardEvent(CardInTable card) { }
        protected override void UnsubscribeFromCardEvent(CardInTable card) { }

        protected override void DrawCards()
        {
            var currentNumberLines = GetNumberLines();
            for (var i = 0; i < currentNumberLines; i++)
            {
                var yPosition = GetCardPosition(i, currentNumberLines, _yDistanceBetweenCards);
                var currentNumberColumns = GetNumberColumns(i); 
                for (var j=0; j < currentNumberColumns; j++)
                {
                    var xPosition = GetCardPosition(j, currentNumberColumns, _xDistanceBetweenCards);
                    var position = new Vector3(xPosition, -yPosition, 0);
                    
                    _cards[i * _numberColumns + j].transform.localPosition = position;
                    if (_upperCards[i * _numberColumns + j] == null) continue;
                    
                    var upperCardTransform = _upperCards[i * _numberColumns + j].transform;
                    upperCardTransform.localPosition = position + new Vector3(_xPaddingUpperCard, 0, 0);
                    upperCardTransform.rotation = Quaternion.Euler(0, 0, _angleRotationBeatingCard);
                }
            }
        }

        private int GetNumberLines()
        {
            return (_cards.Count - 1) / _numberColumns + 1;
        }
        
        private int GetNumberColumns(int index)
        {
            if (index < _cards.Count / _numberColumns)
                return _numberColumns;
            var count = _cards.Count;
            return count % 3 == 0 ? count : count % _numberColumns;
        }

        private float GetCardPosition(int i, int count, float width)
        {
            return (i - (count - 1 )  * 0.5f) * width;
        }
    }
}