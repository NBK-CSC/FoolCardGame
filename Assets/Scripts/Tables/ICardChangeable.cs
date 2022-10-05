using System;

namespace Tables
{
    public interface ICardChangeable
    {
        public event Action<int, int> OnCardsChanged;
    }
}