using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Player.Enums;

namespace FoolCardGame.Table.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс подроски карт на стол
    /// </summary>
    public interface ITakingCard
    {
        /// <summary>
        /// Подпросить карту на стол
        /// </summary>
        /// <param name="card"> Модель карты</param>
        /// <param name="playerStatus">Статус игрока бросившего карту</param>
        /// <returns>Возврат статуса подкидывания (получилось побить или нет)</returns>
        public bool TryTakeCard(ICardModel card, PlayerStatus playerStatus);
        
        /// <summary>
        /// Подпросить карту на стол, поверх другой карты (т.е. побить)
        /// </summary>
        /// <param name="lowerCard">Нижняя карта, которую бьют</param>
        /// <param name="upperCard">Верхняя карта, которой бьют</param>
        /// <param name="playerStatus">Статус игрока</param>
        /// <returns>Возврат статуса подкидывания (получилось побить или нет)</returns>
        public bool TryTakeCard(ICardModel lowerCard, ICardModel upperCard, PlayerStatus playerStatus);
    }
}