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
        [SerializeField] private HandView _handView;
        [SerializeField] private PlayingPanel _panelView;
        
        private Hand _handModel;
        private HandPresenter _handPresenter;

        private Panel _panelModel;
        private PanelPresenter _panelPresenter;
        
        private IGetterCard _dealer;
        private ITakingCard _table;
        private IGettingState _getterState;
        private IStepping _step;

        public event Action<IPlaying> Proceeded;
        public event Action OnStatusChanged;
        
        [field:SerializeField] public StatusPlayer Status { get; private set; }

        public void Init(IGetterCard dealer, ITakingCard table, IGettingState getterState, IStepping step)
        {
            _dealer = dealer;
            _table = table;
            _getterState = getterState;
            _step = step;
            
            _handModel = new Hand(this, _table);
            _handPresenter = new HandPresenter(_handModel, _handView);

            _panelModel = new Panel(this);
            _panelPresenter = new PanelPresenter(_panelModel, _panelView);
        }

        public void Enable()
        {
            _handPresenter.Enable();
            
            _panelModel.Enable();
            _panelPresenter.Enable();
            _panelView.Enable();
            
            _getterState.OnStatusesChanged += ChangeStatus;
            _step.StepBegun += GiveCards;
        }

        public void Disable()
        {
            _handPresenter.Disable();
            
            _panelModel.Disable();
            _panelPresenter.Disable();
            _panelView.Disable();
            
            _getterState.OnStatusesChanged -= ChangeStatus;
            _step.StepBegun -= GiveCards;
        }
        
        public bool TryGetSmallestTrumpCard(out ICardData resultCard)
        {
            resultCard = null;
            foreach (var card in _handModel.Cards)
                if (card.Suit == _dealer.TrumpCard.Suit
                    && (resultCard == null || card.Seniority < resultCard.Seniority))
                    resultCard = card;
            
            return resultCard != null;
        }
        
        private void ChangeStatus()
        {
            Status = _getterState.GetStatus(this);
            OnStatusChanged?.Invoke();
        }

        private void GiveCards()
        {
            if (!_handModel.IsNeedGetCard(out var number)) return;
            for (var i = 0; i < number; i++)
                if (_dealer.TryGiveCard(out var card))
                    _handModel.GiveCard(card);
                else
                    break;
        }
    }
}