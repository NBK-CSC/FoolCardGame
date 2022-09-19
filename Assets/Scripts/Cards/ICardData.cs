using UnityEngine;

namespace Cards
{
    public interface ICardData
    {
        public Seniority Seniority { get; }
        public Suit Suit { get; }
        public Sprite Sprite { get; }
    }
}