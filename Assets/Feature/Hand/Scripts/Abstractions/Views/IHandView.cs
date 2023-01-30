using System.Collections.Generic;
using FoolCardGame.Card.Abstractions.Controllers;

namespace FoolCardGame.Hand.Abstractions.Views
{
    /// <summary>
    /// Абстракция вью руки
    /// </summary>
    public interface IHandView
    {
        /// <summary>
        /// Отрисовать карты в руке
        /// </summary>
        /// <param name="cards">Карты в руке</param>
        public void DrawCards(IEnumerable<BaseCardController> cards);
    }
}