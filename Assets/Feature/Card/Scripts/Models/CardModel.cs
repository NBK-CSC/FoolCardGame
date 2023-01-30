using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Enums;
using UnityEngine;

namespace FoolCardGame.Card.Models
{
    /// <summary>
    /// Модель карты
    /// </summary>
    [CreateAssetMenu(fileName = "New card", menuName = "Card", order = 51)]
    public class CardModel : ScriptableObject, ICardModel
    {
        [SerializeField] private Seniority seniority;
        [SerializeField] private Suit suit;
        [SerializeField] private Sprite sprite;

        /// <summary>
        /// Старшинство карты
        /// </summary>
        public Seniority Seniority => seniority;
 
        /// <summary>
        /// Масть карты
        /// </summary>
        public Suit Suit => suit;
        
        /// <summary>
        /// Изображение карты
        /// </summary>
        public Sprite Sprite => sprite;
    }
}