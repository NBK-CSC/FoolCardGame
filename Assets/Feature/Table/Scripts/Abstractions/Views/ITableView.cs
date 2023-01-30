using System.Collections.Generic;
using FoolCardGame.Card.Abstractions.Controllers;

namespace FoolCardGame.Table.Abstractions.Views
{
    /// <summary>
    /// Интерфейс вью стола
    /// </summary>
    public interface ITableView
    {
        /// <summary>
        /// Отрисовать карты на столе
        /// </summary>
        /// <param name="lowerCards">Нижние карты</param>
        /// <param name="upperCards">Верхние карты</param>
        public void DrawCards(IEnumerable<BaseCardController> lowerCards, IEnumerable<BaseCardController> upperCards);
    }
}