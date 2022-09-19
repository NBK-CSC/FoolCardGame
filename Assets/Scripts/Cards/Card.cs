using UnityEngine;

namespace Cards
{
    public abstract class Card : MonoBehaviour
    {
        public abstract ICardData Data { get;}
        public abstract void Init(ICardData data);
    }
}