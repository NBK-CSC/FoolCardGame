using System;
using Cards;
using Dealers;
using Hands;
using Matches;
using States;
using Tables;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour, IPlaying, IGettingSmallestTrumpCard, IGivingState
    {
        [SerializeField] private HandView _view;
        
        private Hand _hand;
        private HandPresenter _presenter;
        
        private IGetterCard _dealer;
        private ITakingCard _table;
        private IGettingState _getterState;
        private IStepping _step;

        public event Action<IPlaying> Proceed;

        [field:SerializeField] public StatusPlayer Status { get; private set; }

        public void Init(IGetterCard dealer, ITakingCard table, IGettingState getterState, IStepping step)
        {
            _dealer = dealer;
            _table = table;
            _getterState = getterState;
            _step = step;
            
            _hand = new Hand(this, _table);
            _presenter = new HandPresenter(_hand, _view);
        }

        public void Enable()
        {
            _presenter.Enable();
            _getterState.OnStatusChanged += ChangeStatus;
            _step.StepBegun += GiveCards;
        }

        public void Disable()
        {
            _presenter.Disable();
            _getterState.OnStatusChanged -= ChangeStatus;
            _step.StepBegun -= GiveCards;
        }
        
        public bool TryGetSmallestTrumpCard(out ICardData resultCard)
        {
            resultCard = null;
            foreach (var card in _hand.Cards)
                if (card.Suit == _dealer.TrumpCard.Suit
                    && (resultCard == null || card.Seniority < resultCard.Seniority))
                    resultCard = card;
            
            return resultCard != null;
        }
        
        private void ChangeStatus()
        {
            Status = _getterState.GetStatus(this);
        }

        private void GiveCards()
        {
            if (!_hand.IsNeedGetCard(out var number)) return;
            for (var i = 0; i < number; i++)
                if (_dealer.TryGiveCard(out var card))
                    _hand.GiveCard(card);
                else
                    break;
        }
    }
}