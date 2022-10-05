using System;
using System.Collections.Generic;
using Cards;
using Matches;
using Players;
using Tables;

namespace States
{
    public class StateChanger : IGettingState
    {
        private List<IGettingSmallestTrumpCard> _players;
        private Dictionary<IGettingSmallestTrumpCard, StatusPlayer> _statusesPlayers;
        private ICardChangeable _table;

        public IGettingSmallestTrumpCard Defender { get; private set; }
        public StepStatus StepStatus { get; private set; }

        public event Action OnStatusChanged;

        public StateChanger(IEnumerable<IGettingSmallestTrumpCard> players, ICardChangeable table)
        {
            _players = new List<IGettingSmallestTrumpCard>(players);
            _table = table;
            _statusesPlayers = new Dictionary<IGettingSmallestTrumpCard, StatusPlayer>();
            
            foreach (var player in _players)
                _statusesPlayers.Add(player, StatusPlayer.ThrowerDisabled);
        }
        
        public void Enable()
        {
            _table.OnCardsChanged += CheckDispositionCards;
        }
        
        public void Disable()
        {
            _table.OnCardsChanged -= CheckDispositionCards;
        }

        public void Start()
        {
            SetStatusesOnBeginStep(FindWhoThrowFirst());
        }

        public StatusPlayer GetStatus(IGettingSmallestTrumpCard player) => _statusesPlayers[player];
    
        private void CheckDispositionCards(int numberLowerCards, int numberUpperCards)
        {
            foreach (var player in _players)
                if (player == Defender)
                    _statusesPlayers[player]= numberLowerCards > numberUpperCards ? 
                        StatusPlayer.DefenderActivating : StatusPlayer.DefenderWaiting;
                else
                    _statusesPlayers[player]= numberLowerCards > numberUpperCards? 
                        StatusPlayer.ThrowerWaiting : StatusPlayer.ThrowerActivating;
        }
        
        private IGettingSmallestTrumpCard FindWhoThrowFirst()
        {
            ICardData smallestCard =null;
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
            _statusesPlayers[firstThrower] = StatusPlayer.ThrowerWaiting;
            var indexDefender = (_players.IndexOf(firstThrower) + 1) / _players.Count;
            Defender = _players[indexDefender];
            _statusesPlayers[Defender] = StatusPlayer.DefenderWaiting;
            
            OnStatusChanged?.Invoke();
        }

        private void SetStatusesDefault()
        {
            foreach (var statusPlayer in _statusesPlayers)
                _statusesPlayers[statusPlayer.Key] = StatusPlayer.ThrowerDisabled;
        }
    }
}