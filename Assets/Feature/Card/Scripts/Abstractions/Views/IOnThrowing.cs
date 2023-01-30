using System;

namespace FoolCardGame.Card.Abstractions.Views
{
    /// <summary>
    /// Интерфейс оповещающий, что он подбрасывал
    /// </summary>
    public interface IOnThrowing
    {
        /// <summary>
        /// Ивент, что подрасывал
        /// </summary>
        public event Action OnThrow;
    }
}