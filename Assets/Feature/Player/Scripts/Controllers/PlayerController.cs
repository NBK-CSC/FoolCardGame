using System;
using System.Collections.Generic;
using FoolCardGame.Core;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Dealer.Abstractions.Controllers;
using FoolCardGame.Hand.Controllers;
using FoolCardGame.Hand.Views;
using FoolCardGame.Player.Abstractions.Controllers;
using FoolCardGame.Player.Enums;
using FoolCardGame.Round.Abstractions.Controllers;
using FoolCardGame.State.Abstractions.Controllers;
using FoolCardGame.Table.Abstractions.Controllers;
using UnityEngine;

namespace FoolCardGame.Player.Controllers
{
    /// <summary>
    /// Контроллер игрока
    /// </summary>
    public class PlayerController : MonoBehaviour, IPlaying, IGettingSmallestTrumpCard, IGivingState
    {
        [SerializeField] private HandView handView;
        [SerializeField] private CardFactory cardFactory;
        
        private HandController _handController;

        private IGetterCard _dealer;
        private ITakingCard _table;
        private IGettingStatus _getterStatus;
        private IRound _round;

        public event Action<IPlaying> OnContinue;
        public event Action OnStatusChanged;
        
        [field:SerializeField] public PlayerStatus PlayerStatus { get; private set; }

        private const string TakeText = "Take";
        private const string PassText = "Pass";

        private Dictionary<PlayerStatus, string> _textStatusProceed = new Dictionary<PlayerStatus, string>()
        {
            { PlayerStatus.DefenderActive, TakeText },
            { PlayerStatus.DefenderPassive, null },
            { PlayerStatus.ThrowerActive, PassText },
            { PlayerStatus.ThrowerPassive, null },
            { PlayerStatus.ThrowerDisabled, null }
        };
        
        /// <summary>
        /// Иницилизация игрока
        /// </summary>
        /// <param name="dealer">Дилер</param>
        /// <param name="table">Стол</param>
        /// <param name="getterStatus">Объект выдающий статус</param>
        /// <param name="round">Раунд</param>
        public void Init(IGetterCard dealer, ITakingCard table, IGettingStatus getterStatus, IRound round)
        {
            _dealer = dealer;
            _table = table;
            _getterStatus = getterStatus;
            _round = round;
            
            _handController = new HandController(handView, this, _table, cardFactory);
        }

        /// <summary>
        /// Активировать игрока
        /// </summary>
        public void Enable()
        {
            _getterStatus.OnStatusesChanged += ChangeStatus;
            _round.OnRoundBegun += GiveCards;
        }

        /// <summary>
        /// Деактивировать игрока
        /// </summary>
        public void Disable()
        {
            _getterStatus.OnStatusesChanged -= ChangeStatus;
            _round.OnRoundBegun -= GiveCards;
        }
        
        public bool TryGetSmallestTrumpCard(out ICardModel resultCard)
        {
            resultCard = null;
            foreach (var card in _handController.Cards)
                if (card.Model.Suit == _dealer.TrumpCard.Suit
                    && (resultCard == null || card.Model.Seniority < resultCard.Seniority))
                    resultCard = card.Model;
            
            return resultCard != null;
        }
        
        private void ChangeStatus()
        {
            PlayerStatus = _getterStatus.GetStatus(this);
            OnStatusChanged?.Invoke();
        }

        private void GiveCards()
        {
            if (!_handController.IsNeedGetCard(out var number)) return;
            for (var i = 0; i < number; i++)
                if (_dealer.TryGetCard(out var card))
                    _handController.GiveCard(card);
                else
                    break;
        }
    }
}