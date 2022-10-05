using System.Collections.Generic;
using Cards;
using Dealers;
using Players;
using States;
using Tables;
using UnityEngine;

namespace Matches
{
    public class Match : MonoBehaviour, IGettingActivePlayers
    {
        [SerializeField] private int _count;
        [SerializeField] private List<CardData> _cards;
        [SerializeField] private List<Player> _players;
        [SerializeField] private PlayArea _tableView;
        [SerializeField] private DealerView _dealerView;

        private List<Player> _activePlayers;
        
        private Dealer _dealerModel;
        private DealerPresenter _dealerPresenter;
        
        private Table _tableModel;
        private TablePresenter _tablePresenter;
        
        private StateChanger _stateChanger;
        private Step _step;

        public IEnumerable<IPlaying> ActivePlayers => _activePlayers;

        private void Awake()
        {
            _activePlayers = new List<Player>(_players);
            _dealerModel = new Dealer(_count, _cards);
            _dealerPresenter = new DealerPresenter(_dealerModel, _dealerView);
            _tableModel = new Table();
            _tablePresenter = new TablePresenter(_tableModel, _tableView);
            _stateChanger = new StateChanger(_players, _tableModel);
            _step = new Step(_stateChanger, this);
            
            foreach (var player in _players)
                player.Init(_dealerModel, _tableModel, _stateChanger, _step);
        }

        private void OnEnable()
        {
            _dealerPresenter.Enable();
            _tablePresenter.Enable();
            _stateChanger.Enable();
            _step.Enable();
            
            foreach (var player in _players)
                player.Enable();
        }

        private void OnDisable()
        {
            _dealerPresenter.Disable();
            _tablePresenter.Disable();
            _stateChanger.Disable();
            _step.Disable();
            
            foreach (var player in _players)
                player.Disable();
        }

        private void Start()
        {
            _dealerModel.ShuffleCards();
            _tableModel.Trump = _dealerModel.TrumpCard.Suit;
            _step.Start();
        }
    }
}