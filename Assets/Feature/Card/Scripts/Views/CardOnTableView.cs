using System;
using FoolCardGame.Behaviours;
using FoolCardGame.Behaviours.Abstractions;
using FoolCardGame.Card.Abstractions.Views;
using UnityEngine;

namespace FoolCardGame.Card.Views
{
    public class CardOnTableView : BaseCardView, IOnAttacking
    {
        [SerializeField] private AbstractComponentDetector<IBeating> componentDetector;

        public event Action<IBeating> OnAttacked;
        
        private void OnEnable()
        {
            componentDetector.OnComponentDetected += OnDetectCard;
        }
        
        private void OnDisable()
        {
            componentDetector.OnComponentDetected -= OnDetectCard;
        }
        
        private void OnDetectCard(IBeating card)
        {
            OnAttacked?.Invoke(card);
        }
    }
}