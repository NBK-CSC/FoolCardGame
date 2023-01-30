using FoolCardGame.Card.Abstractions.Models;

namespace FoolCardGame.Player.Abstractions.Controllers
{
    /// <summary>
    /// Интерфес выдающего наименьщий козырь
    /// </summary>
    public interface IGettingSmallestTrumpCard
    {
        /// <summary>
        /// Попытаться получить наименьший козырь
        /// </summary>
        /// <param name="card">Модель карты</param>
        /// <returns>Есть ли козырь в руке?</returns>
        public bool TryGetSmallestTrumpCard(out ICardModel card);
    }
}