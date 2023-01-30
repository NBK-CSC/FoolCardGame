using FoolCardGame.Card.Abstractions.Models;

namespace FoolCardGame.Card.Abstractions.Views
{
    /// <summary>
    /// Интерфейс побивающей карты
    /// </summary>
    public interface IBeating
    {
        /// <summary>
        /// Побить карту
        /// </summary>
        /// <param name="beatingCardModel">Карта, которую бьют</param>
        public void Beat(ICardModel beatingCardModel);
    }
}