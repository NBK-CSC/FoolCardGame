using Cards;

namespace Dealers
{
    public class DealerPresenter
    {
        private readonly Dealer _model;
        private readonly DealerView _view;

        public DealerPresenter(Dealer model, DealerView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.OnTrumpCardSet += DrawTrumpCard;
        }

        public void Disable()
        {
            _model.OnTrumpCardSet -= DrawTrumpCard;
        }

        private void DrawTrumpCard(ICardData cardData)
        {
            _view.DrawTrumpCard(cardData);
        }
    }
}