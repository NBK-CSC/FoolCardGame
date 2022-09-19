using Cards;

namespace Tables
{
    public class TablePresenter
    {
        private readonly Table _model;
        private readonly PlayArea _view;

        public TablePresenter(Table model, PlayArea view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.OnCardBeaten += AddCardInView;
            _model.OnCardLaid += AddCardInView;
        }

        public void Disable()
        {
            _model.OnCardBeaten -= AddCardInView;
            _model.OnCardLaid -= AddCardInView;
        }

        private void AddCardInView(ICardData data, int index)
        {
            _view.AddCard(data, index);
        }
        
        private void AddCardInView(ICardData data)
        {
            _view.AddCard(data);
        }
        
        private void RemoveCardInView(ICardData data)
        {
            _view.RemoveCard(data);
        }
    }
}