using System;
using FoolCardGame.Round.Enums;

namespace FoolCardGame.State.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс меняющего статусы
    /// </summary>
    public interface IStatusChangeable
    {
        /// <summary>
        /// Текущий статус раунда
        /// </summary>
        public RoundStatus CurrentRoundStatus { get; }

        /// <summary>
        /// Ивент, что статус изменились
        /// </summary>
        public event Action OnStatusesChanged;
    }
}