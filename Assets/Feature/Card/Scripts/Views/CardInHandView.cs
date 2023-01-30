using System;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Abstractions.Views;
using FoolCardGame.Card.Behaviours;
using UnityEngine;

namespace FoolCardGame.Card.Views
{
    /// <summary>
    /// Вью карты в руке
    /// </summary>
    [RequireComponent(typeof(CardMover))]
    public class CardInHandView : BaseCardView, IThrowing, IOnThrowing, IBeating, IOnBeating
    {
        private CardMover _cardMover;
        
        public event Action OnThrow;
        public event Action<ICardModel> OnBeat;

        protected override void SetAwakeSettings()
        {
            base.SetAwakeSettings();
            _cardMover = GetComponent<CardMover>();
        }

        private void OnEnable()
        {
            _cardMover.DragHandlerBegun += DisableImageRaycast;
            _cardMover.DragHandlerEnded += EnableImageRaycast;
        }

        private void OnDisable()
        {
            Image.sprite = null;
            _cardMover.DragHandlerBegun -= DisableImageRaycast;
            _cardMover.DragHandlerEnded -= EnableImageRaycast;
        }

        private void DisableImageRaycast()
        {
            Image.raycastTarget = false;
        }
        
        private void EnableImageRaycast()
        {
            Image.raycastTarget = true;
        }
        
        public void Beat(ICardModel beatingCardModel)
        {
            OnBeat?.Invoke(beatingCardModel);
        }

        public void Throw()
        {
            OnThrow?.Invoke();
        }
    }
}