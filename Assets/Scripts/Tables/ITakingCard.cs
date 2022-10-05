using Cards;
using States;

namespace Tables
{
    public interface ITakingCard
    {
        public bool TryTakeCard(ICardData card, StatusPlayer status);
        public bool TryTakeCard(ICardData lowerCard, ICardData upperCard, StatusPlayer status);
    }
}