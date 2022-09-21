using System;
using Cards;
using UnityEngine;

namespace Dealers
{
    public class DealerView : MonoBehaviour
    {
        [SerializeField] private CardInDealer _trumpCard;
        [SerializeField] private Transform _container;

        private void Awake()
        {
            Instantiate(_trumpCard, _container);
        }

        private void OnEnable()
        {
        }

        public void DrawTrumpCard(ICardData cardData)
        {
            _trumpCard.Init(cardData);
        }
    }
}