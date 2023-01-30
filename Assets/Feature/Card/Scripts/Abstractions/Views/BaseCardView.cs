using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Card.Abstractions.Views
{
    /// <summary>
    /// Абстрактный класс вью карты
    /// </summary>
    [RequireComponent(typeof(Image), typeof(Transform))]
    public abstract class BaseCardView : MonoBehaviour
    {
        private RectTransform _rectTransform;
        
        protected Image Image;
        
        /// <summary>
        /// Маштаб объекта
        /// </summary>
        public float Scale =>  _rectTransform.localScale.x;

        /// <summary>
        /// Ширина объекта
        /// </summary>
        public float Width => _rectTransform.rect.width;
        
        /// <summary>
        /// Длина объекта
        /// </summary>
        public float Height => _rectTransform.rect.height;

        /// <summary>
        /// Установить спрайт
        /// </summary>
        public Sprite Sprite { set => Image.sprite = value; }
        
        /// <summary>
        /// Установить позицию изображения
        /// </summary>
        public Vector3 Position { set => _rectTransform.localPosition = value; }
        
        /// <summary>
        /// Установить поворот изображения
        /// </summary>
        public Quaternion Rotation { set => _rectTransform.rotation = value; }

        private void Awake()
        {
            SetAwakeSettings();
        }
        
        protected virtual void SetAwakeSettings()
        {
            Image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}