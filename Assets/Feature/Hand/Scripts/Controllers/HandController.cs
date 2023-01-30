using System;
using System.Collections.Generic;
using System.Linq;
using FoolCardGame.Card.Abstractions.Behaviours;
using FoolCardGame.Card.Abstractions.Controllers;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Hand.Abstractions.Controllers;
using FoolCardGame.Hand.Abstractions.Views;
using FoolCardGame.Player.Abstractions.Controllers;
using FoolCardGame.Table.Abstractions.Controllers;

namespace FoolCardGame.Hand.Controllers
{
    /// <summary>
    /// Контроллер рук
    /// </summary>
    public class HandController : IGivingCard
    {
        private readonly IHandView _view;
        
        private readonly ITakingCard _table;
        private readonly IGivingState _player;

        private readonly List<BaseCardController> _cardControllers;
        private readonly ICardInHandFactory _cardsCreator;
        
        /// <summary>
        /// Контроллеры карт в руке
        /// </summary>
        public IEnumerable<BaseCardController> Cards => _cardControllers;
        
        /// <summary>
        /// Карта получена
        /// </summary>
        public event Action<ICardModel> CardGiven;
        
        /// <summary>
        /// Карта выкинута
        /// </summary>
        public event Action<ICardModel> CardTakenAway;

        /// <summary>
        /// Конструктор контроллер
        /// </summary>
        /// <param name="view">Вье руки</param>
        /// <param name="player">Игрок</param>
        /// <param name="table">Стол</param>
        /// <param name="cardsCreator">Фабрика карт</param>
        public HandController(IHandView view, IGivingState player, ITakingCard table, ICardInHandFactory cardsCreator)
        {
            _view = view;
            _player = player;
            _table = table;
            _cardsCreator = cardsCreator;

            _cardControllers = new List<BaseCardController>();
        }
        
        /// <summary>
        /// Получить карту
        /// </summary>
        /// <param name="model">Модель карты</param>
        public void GiveCard(ICardModel model)
        {
            var newCardController = _cardsCreator.CreateCardInHand(model);
            newCardController.Enable();
            newCardController.ToPutDownTried += TakeAwayCard;
            newCardController.ToBeatTried += TakeAwayCard;
            
            _cardControllers.Add(newCardController);
            _view.DrawCards(_cardControllers);
        }
        
        /// <summary>
        /// Нужна ли карта?
        /// </summary>
        /// <param name="number">Количество карт, сколько нужно</param>
        /// <returns>Нужна ли карта?</returns>
        public bool IsNeedGetCard(out int number)
        {
            number = 6 - Cards.Count(); 
            return Cards.Count() < 6;
        }
        
        private void TakeAwayCard(ICardModel model)
        {
            if (!_table.TryTakeCard(model, _player.PlayerStatus))
                return;
            DeleteCard(model);
        }
        
        private void TakeAwayCard(ICardModel model, ICardModel ourCardModel)
        {
            if (!_table.TryTakeCard(model, ourCardModel, _player.PlayerStatus))
                return;
            DeleteCard(ourCardModel);
        }

        private void DeleteCard(ICardModel model)
        {
            _cardControllers.RemoveAll(cardController =>
            {
                if (cardController.Model != model) return false;
                cardController.Disable();
                return true;
            });
            _view.DrawCards(_cardControllers);
            CardTakenAway?.Invoke(model);
        }
    }
}