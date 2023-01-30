using System.Collections.Generic;
using FoolCardGame.Player.Abstractions.Controllers;

namespace FoolCardGame.Match.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс получания списка игроков
    /// </summary>
    public interface IGettingPlayers
    {
        /// <summary>
        /// Перечисления игроков
        /// </summary>
        public IEnumerable<IPlaying> Players { get; }
    }
}