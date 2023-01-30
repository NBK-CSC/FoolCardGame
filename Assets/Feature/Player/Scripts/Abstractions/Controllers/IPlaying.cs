using System;

namespace FoolCardGame.Player.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс играющего
    /// </summary>
    public interface IPlaying
    {
        /// <summary>
        /// Ивент продолжение играть
        /// </summary>
        public event Action<IPlaying> OnContinue;
    }
}