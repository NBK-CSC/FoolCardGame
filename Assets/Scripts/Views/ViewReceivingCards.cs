using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using ObjectPool;
using UnityEngine;

namespace Views
{
    public abstract class ViewReceivingCards <T>: MonoBehaviour where T : Card
    {
        [SerializeField] protected GameObject _cardsContainer;
        [SerializeField] protected T _cardPrefab;
        
        protected PoolMono<T> _pool;
        protected List<T> _cards;

        protected abstract void Init();
        protected abstract void SubscribeCardEvent(T card);
        protected abstract void UnsubscribeFromCardEvent(T card);
        protected abstract void DrawCards();

        private void Awake()
        {
            Init();
        }

        public void AddCard(ICardData data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var newCard = _pool.GetFreeObject();
            newCard.Init(data);
            SubscribeCardEvent(newCard);
            _cards.Add(newCard);
            
            DrawCards();
        }

        public void RemoveCard(ICardData data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var reCard = _cards.First(c => c.Data == data);
            reCard.gameObject.SetActive(false);
            UnsubscribeFromCardEvent(reCard);
            _cards.Remove(reCard);
            
            DrawCards();
        }
    }
}