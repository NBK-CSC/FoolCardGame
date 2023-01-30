using System.Collections.Generic;
using FoolCardGame.Behaviours;
using FoolCardGame.Card.Models;
using FoolCardGame.Dealer.Controllers;
using FoolCardGame.Dealer.Views;
using FoolCardGame.Match.Abstractions.Controllers;
using FoolCardGame.Player.Abstractions.Controllers;
using FoolCardGame.Player.Controllers;
using FoolCardGame.Round.Controllers;
using FoolCardGame.State.Controllers;
using FoolCardGame.Table.Controllers;
using FoolCardGame.Table.Views;
using UnityEngine;

namespace FoolCardGame.Match.Controllers
{
    /// <summary>
    /// Контроллер матча
    /// </summary>
    public class MatchController : MonoBehaviour, IGettingPlayers
    {
        [SerializeField] private int count;
        [SerializeField] private List<CardModel> cards;
        [SerializeField] private List<PlayerController> players;
        [SerializeField] private TableView tableView;
        [SerializeField] private DealerView dealerView;

        [SerializeField] private CardFactory cardFactory;
        
        private List<PlayerController> _players;
        
        private DealerController _dealerController;
        private TableController _tableController;
        private StatusController _statusController;
        private RoundController _roundController;

        public IEnumerable<IPlaying> Players => _players;

        private void Awake()
        {
            _players = players;
            _dealerController = new DealerController(count, cards, dealerView);
            
            _tableController = new TableController(tableView, cardFactory);
            
            _statusController = new StatusController(players, _tableController);
            _roundController = new RoundController(_statusController, this);

            foreach (var player in players)
                player.Init(_dealerController, _tableController, _statusController, _roundController);
        }

        private void OnEnable()
        {
            _statusController.Enable();
            _roundController.Enable();
            
            foreach (var player in players)
                player.Enable();
        }

        private void OnDisable()
        {
            _statusController.Disable();
            _roundController.Disable();
            
            foreach (var player in players)
                player.Disable();
        }

        private void Start()
        {
            _dealerController.ShuffleCards();
            _tableController.Trump = _dealerController.TrumpCard.Suit;
            _roundController.Start();
        }
    }
}