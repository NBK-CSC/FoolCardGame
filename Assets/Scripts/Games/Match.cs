using System;
using System.Collections.Generic;
using Cards;
using Dealers;
using Players;
using Tables;
using UnityEngine;

namespace Games
{
    public class GameController : MonoBehaviour, IStepping
    {
        [SerializeField] private int _count;
        [SerializeField] private List<CardData> _cards;
        [SerializeField] private List<Player> _players;
        [SerializeField] private PlayArea _tableView;
        [SerializeField] private DealerView _dealerView;

        private Dealer _dealerModel;
        private DealerPresenter _dealerPresenter;
        private Table _tableModel;
        private TablePresenter _tablePresenter;

        public event Action StepEnded;
        public event Action GameStarted;

        private void Awake()
        {
            _dealerModel = new Dealer(_count, _cards);
            _dealerPresenter = new DealerPresenter(_dealerModel, _dealerView);
            _tableModel = new Table();
            _tablePresenter = new TablePresenter(_tableModel, _tableView);

            foreach (var player in _players)
                player.Init(_dealerModel, _tableModel, this);
        }

        private void OnEnable()
        {
            _dealerPresenter.Enable();
            _tablePresenter.Enable();
            foreach (var player in _players)
                player.Enable();
        }

        private void OnDisable()
        {
            _dealerPresenter.Disable();
            _tablePresenter.Disable();
            foreach (var player in _players)
                player.Disable();
        }

        private void Start()
        {
            _dealerModel.ShuffleCards();
            _tableModel.Trump = _dealerModel.TrumpCard.Suit;
            GameStarted?.Invoke();
        }
    }
}