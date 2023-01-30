using System;
using System.Collections.Generic;
using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Dealer.Abstractions.Controllers;
using FoolCardGame.Dealer.Abstractions.Views;
using Random = System.Random;

namespace FoolCardGame.Dealer.Controllers
{
    /// <summary>
    /// Контроллер дилера
    /// </summary>
    public class DealerController : IGetterCard
    {
        private readonly AbstractDealerView _view;
        
        private readonly Queue<ICardModel> _pack;
        private readonly List<ICardModel> _cardModels;
        
        private readonly int _numberCardsInPack;
        private readonly Random _random = new Random();

        public ICardModel TrumpCard { get; private set; }
        
        /// <summary>
        /// Ивент присваивании козырной карты
        /// </summary>
        public event Action<ICardModel> OnTrumpCardSet;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="numberCardsInPack">Количество карт в колоде</param>
        /// <param name="cardModels">Модели карты колоды</param>
        /// <param name="view">Вью дилера</param>
        public DealerController(int numberCardsInPack, IEnumerable<ICardModel> cardModels, AbstractDealerView view)
        {
            _numberCardsInPack = numberCardsInPack;
            _cardModels = new List<ICardModel>(cardModels);
            _view = view;
            
            _pack=new Queue<ICardModel>();
        }

        private ICardModel RandomlyPlaceCard()
        {
            var index = _random.Next(_cardModels.Count);
            var removeCard= _cardModels[index];
            _cardModels.RemoveAt(index);
            return removeCard;
        }

        /// <summary>
        /// Потусовать карты
        /// </summary>
        public void ShuffleCards()
        {
            for(var i=0 ; i<_numberCardsInPack; i++)
                _pack.Enqueue(RandomlyPlaceCard());
            TrumpCard = _pack.Dequeue();
            _view.DrawTrumpCard(TrumpCard);
            //TODO чекнуть кто подписан
            OnTrumpCardSet?.Invoke(TrumpCard);
            _pack.Enqueue(TrumpCard);
        }

        public bool TryGetCard(out ICardModel card)
        {
            if (_pack.Count > 0)
            {
                card = _pack.Dequeue();
                return true;
            }
            card = null;
            return false;
        }
    }
}