using Cards;

namespace Dealers
{
    public interface IGetterCard
    {
        public bool TryGiveCard(out ICardData car);
    }
}