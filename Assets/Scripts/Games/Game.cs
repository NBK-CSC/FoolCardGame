using System.Collections.Generic;
using Cards;
using Dealers;
using Players;
using Tables;
using UnityEngine;

namespace Games
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private List<CardData> _cards;
        [SerializeField] private List<Player> _players;
        [SerializeField] private PlayArea _tableView;
        
        private Dealer _dealer;
        private Table _tableModel;
        private TablePresenter _tablePresenter;

        private void Awake()
        {
            _dealer = new Dealer(_count, _cards);
            _tableModel = new Table(_dealer.TrumpCard.Suit);
            _tablePresenter = new TablePresenter(_tableModel, _tableView);
            
            _players[0].Init(_dealer, _tableModel);
        }

        private void OnEnable()
        {
            _players[0].Enable();
            _tablePresenter.Enable();
        }
        
        private void OnDisable()
        {
            _players[0].Disable();
            _tablePresenter.Disable();
        }
    }
}