using FoolCardGame.Card.Abstractions.Models;

namespace FoolCardGame.Hand.Abstractions.Controllers
{
    
    public interface IGivingCard
    {
        public void GiveCard(ICardModel model);
    }
}