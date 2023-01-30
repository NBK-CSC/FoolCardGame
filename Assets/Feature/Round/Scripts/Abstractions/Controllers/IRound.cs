using System;

namespace FoolCardGame.Round.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс раунда
    /// </summary>
    public interface IRound
    {
        /// <summary>
        /// Ивент начала раунда
        /// </summary>
        public event Action OnRoundBegun;
        
        /// <summary>
        /// Ивент конца раунда
        /// </summary>
        public event Action OnRoundEnded;
    }
}