using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(Image))]
    public abstract class Card : MonoBehaviour
    {
        protected Image _image;

        public ICardData Data { get; protected set; }

        public virtual void Init(ICardData data)
        {
            if (Data != null)
                throw new ArgumentException();
            Data = data;
            _image.sprite = Data.Sprite;
        }

        private void Awake()
        {
            SetAwakeSettings();
        }
        
        protected virtual void SetAwakeSettings()
        {
            _image = GetComponent<Image>();
        }
    }
}