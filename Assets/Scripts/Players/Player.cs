using Dealers;
using Hands;
using States;
using Tables;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour, IPlaying
    {
        [SerializeField] private HandView _view;
        
        private IGetterCard _dealer;
        public Hand _hand;
        private HandPresenter _presenter;
        private Table _table;

        [field:SerializeField] public StatusPlayer State { get; set; }

        public void Init(IGetterCard dealer, Table table)
        {
            _dealer = dealer;
            _table = table;
            
            _hand = new Hand(this, _table);
            _presenter = new HandPresenter(_hand, _view);
        }

        public void Enable()
        {
            _presenter.Enable();
        }

        public void Disable()
        {
            _presenter.Disable();
        }

        private void GiveCard()
        {
            if (_hand.IsNeedGetCard() && _dealer.TryGiveCard(out var card))
                _hand.GiveCard(card);
        }
    }
}