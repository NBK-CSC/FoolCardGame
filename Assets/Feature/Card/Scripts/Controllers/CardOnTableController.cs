using FoolCardGame.Card.Abstractions.Controllers;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Abstractions.Views;

namespace FoolCardGame.Card.Controllers
{
    /// <summary>
    /// Контроллер карты в руке
    /// </summary>
    public class CardOnTableController : BaseCardController
    {
        private readonly IOnAttacking _attackingCard;

        /// <summary>
        /// Конструтор контроллера
        /// </summary>
        /// <param name="model">Модель карты</param>
        /// <param name="view">Вью карты</param>
        /// <param name="attackingCard">Карта которую могут побить</param>
        public CardOnTableController(ICardModel model, BaseCardView view, IOnAttacking attackingCard) : base(model, view)
        {
            _attackingCard = attackingCard;
        }

        public override void Enable()
        {
            base.Enable();
            _attackingCard.OnAttacked += OnAttack;
        }

        public override void Disable()
        {
            base.Disable();
            _attackingCard.OnAttacked -= OnAttack;
        }

        private void OnAttack(IBeating card)
        {
            card.Beat(Model);
        }
    }
}