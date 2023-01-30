using System;
using System.Collections.Generic;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Player.Abstractions.Controllers;
using FoolCardGame.Player.Enums;
using FoolCardGame.Round.Enums;
using FoolCardGame.State.Abstractions.Controllers;
using FoolCardGame.Table.Abstractions.Controllers;

namespace FoolCardGame.State.Controllers
{
    /// <summary>
    /// Контроллер статусов
    /// </summary>
    public class StatusController : IGettingStatus, IStatusChangeable
    {
        private readonly List<IGettingSmallestTrumpCard> _players;
        private readonly Dictionary<IGettingSmallestTrumpCard, PlayerStatus> _statusesPlayers;
        private readonly ICardsChangeable _table;

        /// <summary>
        /// Защитник
        /// </summary>
        public IGettingSmallestTrumpCard Defender { get; private set; }
        public RoundStatus CurrentRoundStatus { get; private set; }

        public event Action OnStatusesChanged;

        /// <summary>
        /// Конструторк контроллера
        /// </summary>
        /// <param name="players">Игроки</param>
        /// <param name="table">Стол</param>
        public StatusController(IEnumerable<IGettingSmallestTrumpCard> players, ICardsChangeable table)
        {
            _players = new List<IGettingSmallestTrumpCard>(players);
            _table = table;
            _statusesPlayers = new Dictionary<IGettingSmallestTrumpCard, PlayerStatus>();

            foreach (var player in _players)
                _statusesPlayers.Add(player, PlayerStatus.ThrowerDisabled);
        }
        
        /// <summary>
        /// Активировать контроллер статусов
        /// </summary>
        public void Enable()
        {
            _table.OnCardsChanged += CheckDispositionCards;
        }
        
        /// <summary>
        /// Деактивировать контроллер статусов
        /// </summary>
        public void Disable()
        {
            _table.OnCardsChanged -= CheckDispositionCards;
        }

        /// <summary>
        /// Запустить контроллер статусов
        /// </summary>
        public void Start()
        {
            SetStatusesOnBeginStep(FindWhoThrowFirst());
        }

        public PlayerStatus GetStatus(IGettingSmallestTrumpCard player) => _statusesPlayers[player];
    
        private void CheckDispositionCards(int numberLowerCards, int numberUpperCards)
        {
            foreach (var player in _players)
                if (player == Defender)
                    _statusesPlayers[player]= numberLowerCards > numberUpperCards ? 
                        PlayerStatus.DefenderActive : PlayerStatus.DefenderPassive;
                else
                    _statusesPlayers[player]= numberLowerCards > numberUpperCards? 
                        PlayerStatus.ThrowerPassive : PlayerStatus.ThrowerActive;
        }
        
        private IGettingSmallestTrumpCard FindWhoThrowFirst()
        {
            ICardModel smallestCard =null;
            IGettingSmallestTrumpCard challenger = null;
            
            foreach (var player in _players)
                if (player.TryGetSmallestTrumpCard(out var card)
                    && (smallestCard == null || card.Seniority < smallestCard.Seniority))
                {
                    challenger = player;
                    smallestCard = card;
                }
            
            return challenger;
        }
        
        private void SetStatusesOnBeginStep(IGettingSmallestTrumpCard firstThrower)
        {
            _statusesPlayers[firstThrower] = PlayerStatus.ThrowerPassive;
            var indexDefender = (_players.IndexOf(firstThrower) + 1) / _players.Count;
            Defender = _players[indexDefender];
            _statusesPlayers[Defender] = PlayerStatus.DefenderPassive;
            
            OnStatusesChanged?.Invoke();
        }

        private void SetStatusesDefault()
        {
            foreach (var statusPlayer in _statusesPlayers)
                _statusesPlayers[statusPlayer.Key] = PlayerStatus.ThrowerDisabled;
        }

        private void SetNextStatuses()
        {
            //TODO
        }
    }
}