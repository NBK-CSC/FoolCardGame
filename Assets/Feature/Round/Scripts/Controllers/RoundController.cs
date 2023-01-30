using System;
using System.Collections.Generic;
using System.Linq;
using FoolCardGame.Match.Abstractions.Controllers;
using FoolCardGame.Player.Abstractions.Controllers;
using FoolCardGame.Round.Abstractions.Controllers;
using FoolCardGame.Round.Enums;
using FoolCardGame.State.Abstractions.Controllers;

namespace FoolCardGame.Round.Controllers
{
    /// <summary>
    /// Контроллер раунда
    /// </summary>
    public class RoundController : IRound
    {
        private readonly IGettingPlayers _getter;
        private readonly IStatusChangeable _statusChanger;
        private readonly Dictionary<IPlaying, bool> _continuedPlayers;
        private int _numberNeedProceed;
        
        public event Action OnRoundBegun;
        public event Action OnRoundEnded;
        
        /// <summary>
        /// Конструктор раунда
        /// </summary>
        /// <param name="statusChanger">Объект меняющий статус</param>
        /// <param name="getter">Объект выдающий игроков</param>
        public RoundController(IStatusChangeable statusChanger, IGettingPlayers getter)
        {
            _statusChanger = statusChanger;
            _getter = getter;

            _continuedPlayers = new Dictionary<IPlaying, bool>();
            
            foreach (var player in getter.Players)
                _continuedPlayers.Add(player, false);
        }
        
        /// <summary>
        /// Активировать раунд
        /// </summary>
        public void Enable()
        {
            _statusChanger.OnStatusesChanged += UnmarkProceedPlayers;
            _statusChanger.OnStatusesChanged += SetNumberNeedProceed;
            foreach (var player in _getter.Players)
                player.OnContinue += MarkProceedPlayers;
        }
        
        /// <summary>
        /// Деактивировать раунд
        /// </summary>
        public void Disable()
        {
            _statusChanger.OnStatusesChanged -= UnmarkProceedPlayers;
            _statusChanger.OnStatusesChanged -= SetNumberNeedProceed;
            foreach (var player in _getter.Players)
                player.OnContinue -= MarkProceedPlayers;
        }

        /// <summary>
        /// Старт раунда
        /// </summary>
        public void Start()
        {
            OnRoundBegun?.Invoke();
        }

        private void UnmarkProceedPlayers()
        {
            foreach (var proceedPlayer in _continuedPlayers)
                _continuedPlayers[proceedPlayer.Key] = false;
        }

        private void SetNumberNeedProceed()
        {
            _numberNeedProceed = _statusChanger.CurrentRoundStatus == RoundStatus.WaitingDefend
                ? _getter.Players.Count()
                : _getter.Players.Count() - 1;
        }

        private void MarkProceedPlayers(IPlaying player)
        {
            _continuedPlayers[player] = true;
            if (_continuedPlayers.Count(proceedPlayer => proceedPlayer.Value) == _numberNeedProceed)
                OnRoundEnded?.Invoke();
        }
    }
}