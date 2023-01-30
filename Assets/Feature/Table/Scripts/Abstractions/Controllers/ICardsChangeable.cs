using System;

namespace FoolCardGame.Table.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс изменения карт
    /// </summary>
    public interface ICardsChangeable
    {
        /// <summary>
        /// Ивент изменения карт на столе
        /// </summary>
        public event Action<int, int> OnCardsChanged;
    }
}