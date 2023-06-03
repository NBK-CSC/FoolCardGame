using System;
using FoolCardGame.Card.Abstractions.Behaviours;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Controllers;
using FoolCardGame.Card.Views;
using UnityEngine;

namespace FoolCardGame.Core
{
    /// <summary>
    /// Фабрика карт
    /// </summary>
    public class CardFactory : MonoBehaviour, ICardInHandFactory, ICardOnTableFactory
    {
        [Header("On Table")]
        [SerializeField] protected GameObject cardsOnTableContainer;
        [SerializeField] protected CardOnTableView cardOnTableViewPrefab;
        [SerializeField] private int numberOnTableCards;
        [Header("In Hand")]
        [SerializeField] protected GameObject cardsInHandContainer;
        [SerializeField] protected CardInHandView cardInHandViewPrefab;
        [SerializeField] private int numberInHandCards;
        
        private PoolMono<CardOnTableView> _poolOnTableViews;
        private PoolMono<CardInHandView> _poolInHandViews;
        
        private void Awake()
        {
            _poolOnTableViews = new PoolMono<CardOnTableView>(numberOnTableCards, cardOnTableViewPrefab, cardsOnTableContainer.transform);
            _poolInHandViews = new PoolMono<CardInHandView>(numberInHandCards, cardInHandViewPrefab, cardsInHandContainer.transform);
        }

        public CardOnTableController CreateCardOnTable(ICardModel model)
        {
            if (model == null)
                throw new ArgumentNullException();
            var newView = _poolOnTableViews.GetFreeObject();
            return new CardOnTableController(model, newView, newView);
        }
        
        public CardInHandController CreateCardInHand(ICardModel model)
        {
            if (model == null)
                throw new ArgumentNullException();
            var newView = _poolInHandViews.GetFreeObject();
            return new CardInHandController(model, newView, newView, newView);
        }
    }
}