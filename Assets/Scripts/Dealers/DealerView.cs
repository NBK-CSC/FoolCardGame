using Cards;
using UnityEngine;

namespace Dealers
{
    public class DealerView : MonoBehaviour
    {
        [SerializeField] private CardInDealer _trumpCard;

        public void DrawTrumpCard(ICardData cardData)
        {
            _trumpCard.Init(cardData);
        }
    }
}