using Cards;

namespace Dealers
{
    public interface IGetterCard
    {
        public ICardData TrumpCard { get; }
        
        public bool TryGiveCard(out ICardData car);
    }
}