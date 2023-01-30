using FoolCardGame.Card.Enums;
using UnityEngine;

namespace FoolCardGame.Card.Abstractions.Models
{
    /// <summary>
    /// Интерфейс модели карты
    /// </summary>
    public interface ICardModel
    {
        /// <summary>
        /// Старшинство карты
        /// </summary>
        public Seniority Seniority { get; }
        
        /// <summary>
        /// Масть карты
        /// </summary>
        public Suit Suit { get; }
        
        /// <summary>
        /// Изображение карты
        /// </summary>
        public Sprite Sprite { get; }
    }
}