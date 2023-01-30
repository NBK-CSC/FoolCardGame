using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Controllers;

namespace FoolCardGame.Card.Abstractions.Behaviours
{
    /// <summary>
    /// Интерфейс фабрики контроллеров карт в руке
    /// </summary>
    public interface ICardInHandFactory
    {
        /// <summary>
        /// Создать контроллер карты в руке
        /// </summary>
        /// <param name="model">Модель карты</param>
        /// <returns>Контроллер карты в руке</returns>
        public CardInHandController CreateCardInHand(ICardModel model);
    }
}