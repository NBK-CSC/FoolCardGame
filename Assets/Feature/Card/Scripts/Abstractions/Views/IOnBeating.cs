using System;
using FoolCardGame.Card.Abstractions.Models;

namespace FoolCardGame.Card.Abstractions.Views
{
    /// <summary>
    /// Интерфейс оповещающий, что объект побивал другую карту
    /// </summary>
    public interface IOnBeating
    {
        /// <summary>
        /// Ивент, что бил другую карту
        /// </summary>
        public event Action<ICardModel> OnBeat;
    }
}