using System.Collections.Generic;
using System.Linq;
using FoolCardGame.Card.Abstractions.Controllers;
using FoolCardGame.Hand.Abstractions.Views;
using UnityEngine;

namespace FoolCardGame.Hand.Views
{
    /// <summary>
    /// Вью рук
    /// </summary>
    public class HandView : MonoBehaviour, IHandView
    {
        [SerializeField] private Transform vector2Position; //вообще это точка поднявшись над которой карат может в право и влево двигаться
        private float _screenWidth;
        
        private void Awake()
        {
            _screenWidth = Screen.width;
        }
        
        public void DrawCards(IEnumerable<BaseCardController> cards)
        {
            var cardList = cards.ToList();
            if (!cardList.Any())
                return;
            var cardWidth = cardList.First().CardSize.Item1;
            
            var numberCardInHand = cardList.Count;
            var widthCardInHand = cardWidth * numberCardInHand < _screenWidth ? cardWidth : 
                (_screenWidth - cardWidth) / (numberCardInHand - 1);
            
            var vector3 = new Vector3();
            for (var i = 0; i < numberCardInHand; i++)
            {
                vector3.x = GetNewCardPosition(i, numberCardInHand, widthCardInHand);
                vector3.y = vector2Position.position.y;
                cardList[i].SetPosition(vector3);
            }
        }
        
        private float GetNewCardPosition(int i, int number, float width)
        {
            return (i - (number - 1) * 0.5f) * width;
        }
    }
}