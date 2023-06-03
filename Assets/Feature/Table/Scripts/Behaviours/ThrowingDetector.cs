using FoolCardGame.Core.Abstractions;
using FoolCardGame.Card.Abstractions.Views;

namespace FoolCardGame.Table.Behaviours
{
    /// <summary>
    /// Детектор подкидывания карты
    /// </summary>
    public class ThrowingDetector : BaseComponentDropHandlerDetector<IThrowing> { }
}