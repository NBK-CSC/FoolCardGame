using System.Collections.Generic;
using System.Linq;
using FoolCardGame.Behaviours;
using FoolCardGame.Behaviours.Abstractions;
using FoolCardGame.Card.Abstractions.Controllers;
using FoolCardGame.Card.Abstractions.Views;
using FoolCardGame.Table.Abstractions.Views;
using UnityEngine;

namespace FoolCardGame.Table.Views
{
    /// <summary>
    /// Вью стола
    /// </summary>
    public class TableView : MonoBehaviour, ITableView
    {
        [SerializeField] private AbstractComponentDetector<IThrowing> componentDetector;
        [Space]
        [SerializeField] private int numberColumns = 3;
        [SerializeField] private float xDistanceBetweenCards;
        [SerializeField] private float yDistanceBetweenCards;
        [SerializeField] private float xPaddingUpperCard;
        [SerializeField] private float angleRotationBeatingCard;
        
        private BaseCardController[] _lowerCards;
        private BaseCardController[] _upperCards;
        
        private void OnEnable()
        {
            componentDetector.OnComponentDetected += OnDetectCard;
        }
        
        private void OnDisable()
        {
            componentDetector.OnComponentDetected -= OnDetectCard;
        }

        private void OnDetectCard(IThrowing card)
        {
            card.Throw();
        }

        public void DrawCards(IEnumerable<BaseCardController> lowerCards, IEnumerable<BaseCardController> upperCards)
        {
            _lowerCards = lowerCards.ToArray();
            _upperCards = upperCards.ToArray();
            var currentNumberLines = GetNumberLines();

            for (var i = 0; i < currentNumberLines; i++)
            {
                var yPosition = GetCardPosition(i, currentNumberLines, yDistanceBetweenCards);
                var currentNumberColumns = GetNumberColumns(i); 
                for (var j=0; j < currentNumberColumns; j++)
                {
                    var xPosition = GetCardPosition(j, currentNumberColumns, xDistanceBetweenCards);
                    var position = new Vector3(xPosition, -yPosition, 0);
                    
                    _lowerCards[i * numberColumns + j].SetPosition(position);
                    if (_upperCards[i * numberColumns + j] == null) 
                        continue;
                    
                    var upperCard = _upperCards[i * numberColumns + j];
                    upperCard.SetPosition(position + new Vector3(xPaddingUpperCard, 0, 0));
                    upperCard.SetRotation(Quaternion.Euler(0, 0, angleRotationBeatingCard));
                }
            }
        }
        
        private int GetNumberLines()
        {
            return (_lowerCards.Count() - 1) / numberColumns + 1;
        }
        
        private int GetNumberColumns(int index)
        {
            if (index < _lowerCards.Count() / numberColumns)
                return numberColumns;
            var count = _lowerCards.Count();
            return count % 3 == 0 ? count : count % numberColumns;
        }

        private float GetCardPosition(int i, int count, float width)
        {
            return (i - (count - 1 )  * 0.5f) * width;
        }
    }
}