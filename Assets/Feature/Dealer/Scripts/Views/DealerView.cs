using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Views;
using FoolCardGame.Dealer.Abstractions.Views;
using UnityEngine;

namespace FoolCardGame.Dealer.Views
{
    /// <summary>
    /// Вью дилера
    /// </summary>
    public class DealerView : AbstractDealerView
    {
        [SerializeField] private CardAtDealerView trumpCard;

        public override void DrawTrumpCard(ICardModel cardModel)
        {
            trumpCard.Sprite = cardModel.Sprite;
        }
    }
}