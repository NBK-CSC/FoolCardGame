using FoolCardGame.Card.Abstractions.Models;

namespace FoolCardGame.Dealer.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс выдачи карты
    /// </summary>
    public interface IGetterCard
    {
        /// <summary>
        /// Козырь
        /// </summary>
        public ICardModel TrumpCard { get; }
        
        /// <summary>
        /// Попытаться выдать карту
        /// </summary>
        /// <param name="card">Модель карты</param>
        /// <returns>Получилось ли выдать?</returns>
        public bool TryGetCard(out ICardModel card);
    }
}