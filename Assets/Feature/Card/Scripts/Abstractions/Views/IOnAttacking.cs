using System;

namespace FoolCardGame.Card.Abstractions.Views
{
    /// <summary>
    /// Интерфейс оповещающий, что его атаковали
    /// </summary>
    public interface IOnAttacking
    {
        /// <summary>
        /// Ивент, что его атаковали
        /// </summary>
        public event Action<IBeating> OnAttacked;
    }
}