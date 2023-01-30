using FoolCardGame.Card.Abstractions.Models;
using FoolCardGame.Card.Abstractions.Views;
using UnityEngine;

namespace FoolCardGame.Card.Abstractions.Controllers
{
    /// <summary>
    /// Абстрактный контроллер карты
    /// </summary>
    public abstract class BaseCardController
    {
        private readonly BaseCardView _view;

        /// <summary>
        /// Модель карты
        /// </summary>
        public ICardModel Model { get; }
        
        /// <summary>
        /// Линейный размер карты
        /// </summary>
        public (float, float) CardSize => (_view.Width * _view.Scale, _view.Height * _view.Scale);
        
        /// <summary>
        /// Контруктор контроллера
        /// </summary>
        /// <param name="model">Модель карты</param>
        /// <param name="view">Вью карты</param>
        protected BaseCardController(ICardModel model, BaseCardView view)
        {
            Model = model;
            _view = view;
            _view.Sprite = Model.Sprite;
        }

        /// <summary>
        /// Активировать контроллер
        /// </summary>
        public virtual void Enable()
        {
            
        }

        /// <summary>
        /// Деактивировать контроллер
        /// </summary>
        public virtual void Disable()
        {
            _view.gameObject.SetActive(false);
        }

        /// <summary>
        /// Переместить карту в позицию
        /// </summary>
        /// <param name="vector">Вектор точки в пространстве</param>
        public void SetPosition(Vector3 vector)
        {
            _view.Position = vector;
        }

        /// <summary>
        /// Повернуть карту
        /// </summary>
        /// <param name="quaternion">Кватранион поворота</param>
        public void SetRotation(Quaternion quaternion)
        {
            _view.Rotation = quaternion;
        }
    }
}