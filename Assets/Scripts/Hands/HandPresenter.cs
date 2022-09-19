using Cards;

namespace Hands
{
    public class HandPresenter
    {
        private readonly Hand _model;
        private readonly HandView _view;

        public HandPresenter(Hand model, HandView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.CardGiven += AddCardInView;
            _model.CardTakenAway += RemoveCardInView;
            _view.ToTakeTried += RemoveCardInModel;
            _view.ToBeatTried += RemoveCardInModel;
        }

        public void Disable()
        {
            _model.CardGiven -= AddCardInView;
            _model.CardTakenAway -= RemoveCardInView;
            _view.ToTakeTried -= RemoveCardInModel;
            _view.ToBeatTried -= RemoveCardInModel;
        }

        private void AddCardInView(ICardData data)
        {
            _view.AddCard(data);
        }
        
        private void RemoveCardInView(ICardData data)
        {
            _view.RemoveCard(data);
        }

        private void RemoveCardInModel(ICardData data)
        {
            _model.TakeAwayCard(data);
        }
        
        private void RemoveCardInModel(ICardData data1, ICardData data2)
        {
            _model.TakeAwayCard(data1, data2);
        }
    }
}