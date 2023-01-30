using FoolCardGame.Card.Abstractions.Models;
using UnityEngine;

namespace FoolCardGame.Dealer.Abstractions.Views
{
    /// <summary>
    /// Абракция вью дилера
    /// </summary>
    public abstract class AbstractDealerView : MonoBehaviour
    {
        /// <summary>
        /// Отрисовать козырную карту
        /// </summary>
        /// <param name="cardModel">Модель карты</param>
        public abstract void DrawTrumpCard(ICardModel cardModel);
    }
}