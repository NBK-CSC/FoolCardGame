using Cards;

namespace Players
{
    public interface IGettingSmallestTrumpCard
    {
        public bool TryGetSmallestTrumpCard(out ICardData card);
    }
}