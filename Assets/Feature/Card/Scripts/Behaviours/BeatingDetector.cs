using FoolCardGame.Core.Abstractions;
using FoolCardGame.Card.Abstractions.Views;

namespace FoolCardGame.Card.Behaviours
{
    /// <summary>
    /// Детектор карты с компонентов IBeating
    /// </summary>
    public class BeatingDetector : BaseComponentDropHandlerDetector<IBeating> { }
}