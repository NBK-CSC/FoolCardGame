using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Controllers;

namespace FoolCardGame.Card.Abstractions.Behaviours
{
    /// <summary>
    /// Интерфейс фабрики контроллеров карт на столе
    /// </summary>
    public interface ICardOnTableFactory
    {
        /// <summary>
        /// Создать контроллер карты на столе
        /// </summary>
        /// <param name="model">Модель карты</param>
        /// <returns>Контроллер карты на столе</returns>
        public CardOnTableController CreateCardOnTable(ICardModel model);
    }
}