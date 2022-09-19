using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "New card", menuName = "Card", order = 51)]
    public class CardData : ScriptableObject, ICardData
    {
        [SerializeField] private Seniority _seniority;
        [SerializeField] private Suit _suit;
        [SerializeField] private Sprite _sprite;

        public Seniority Seniority => _seniority;
        public Suit Suit => _suit;
        public Sprite Sprite => _sprite;
    }
}