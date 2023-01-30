using System;
using FoolCardGame.Card.Abstractions.Controllers;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Abstractions.Views;

namespace FoolCardGame.Card.Controllers
{
    /// <summary>
    /// Контроллер карты в руке
    /// </summary>
    public class CardInHandController : BaseCardController
    {
        private readonly IOnThrowing _putDownCard;
        private readonly IOnBeating _beatCard;

        /// <summary>
        /// Ивент попытки подросить карту на стол
        /// </summary>
        public event Action<ICardModel> ToPutDownTried;
        
        /// <summary>
        /// Ивент попытки побить карту
        /// </summary>
        public event Action<ICardModel, ICardModel> ToBeatTried;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="model">Модель карты</param>
        /// <param name="view">Вью карты</param>
        /// <param name="putDownCard">Карта для подкидывания на стол</param>
        /// <param name="beatCard">Карта для побития другой карты</param>
        public CardInHandController(
            ICardModel model, 
            BaseCardView view, 
            IOnThrowing putDownCard,
            IOnBeating beatCard) : base(model, view)
        {
            _putDownCard = putDownCard;
            _beatCard = beatCard;
        }

        public override void Enable()
        {
            base.Enable();
            _putDownCard.OnThrow += OnThrow;
            _beatCard.OnBeat += OnBeat;
        }

        public override void Disable()
        {
            base.Disable();
            _putDownCard.OnThrow -= OnThrow;
            _beatCard.OnBeat -= OnBeat;
        }

        private void OnThrow() => ToPutDownTried?.Invoke(Model);
        
        private void OnBeat(ICardModel card) => ToBeatTried?.Invoke(card, Model);
    }
}