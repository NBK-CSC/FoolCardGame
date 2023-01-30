using System;
using System.Collections.Generic;
using FoolCardGame.Card.Abstractions.Behaviours;
using FoolCardGame.Card.Abstractions.Controllers;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Enums;
using FoolCardGame.Player.Enums;
using FoolCardGame.Table.Abstractions.Controllers;
using FoolCardGame.Table.Abstractions.Views;

namespace FoolCardGame.Table.Controllers
{
    /// <summary>
    /// Контроллера стола
    /// </summary>
    public class TableController : ITakingCard, ICardsChangeable
    {
        private readonly List<BaseCardController> _lowerCards;
        private readonly BaseCardController[] _upperCards;
        private readonly ITableView _view;

        private readonly ICardOnTableFactory _cardsCreator;

        public Suit Trump { get; set; }

        /// <summary>
        /// Ивент, что на стол подрошена карта
        /// </summary>
        public event Action<ICardModel> OnCardThrew;
        
        /// <summary>
        /// Ивент, что на столе карта была побита
        /// </summary>
        public event Action<ICardModel, int> OnCardBeaten;
        
        public event Action<int, int> OnCardsChanged;
        
        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="view"></param>
        /// <param name="cardsCreator"></param>
        public TableController(ITableView view, ICardOnTableFactory cardsCreator)
        {
            _lowerCards = new List<BaseCardController>();
            _upperCards = new BaseCardController[6];
            _view = view;
            _cardsCreator = cardsCreator;
        }
        
        public bool TryTakeCard(ICardModel lowerCard, PlayerStatus playerStatus)
        {
            if ((playerStatus != PlayerStatus.ThrowerActive
                 && playerStatus != PlayerStatus.ThrowerPassive
                 && playerStatus != PlayerStatus.ThrowerEnabled)
                || _lowerCards.Count == 6)
                return false;
            var newController = _cardsCreator.CreateCardOnTable(lowerCard);
            newController.Enable();
            _lowerCards.Add(newController);
            
            OnCardThrew?.Invoke(lowerCard);
            OnCardsChanged?.Invoke(_lowerCards.Count, _upperCards.Length);

            _view.DrawCards(_lowerCards, _upperCards);
            
            return true;
        }

        //TODO при удалении карты Disable её

        public bool TryTakeCard(ICardModel lowerCard, ICardModel upperCard, PlayerStatus playerStatus)
        {
            if (playerStatus != PlayerStatus.DefenderActive
                && playerStatus != PlayerStatus.DefenderPassive
                || CheckPar(lowerCard, upperCard) == false)
                return false;
            var index = _lowerCards.FindIndex(x => x.Model == lowerCard);
            
            var newController = _cardsCreator.CreateCardOnTable(upperCard);
            newController.Enable();
            _upperCards[index] = newController;
            
            OnCardBeaten?.Invoke(upperCard, index);
            OnCardsChanged?.Invoke(_lowerCards.Count, _upperCards.Length);
            
            return true;
        }
        
        private bool CheckPar(ICardModel lowerCard, ICardModel upperCard)
        {
            if (lowerCard.Suit == upperCard.Suit ) 
                return lowerCard.Seniority < upperCard.Seniority;
            return upperCard.Suit == Trump;
        }
    }
}