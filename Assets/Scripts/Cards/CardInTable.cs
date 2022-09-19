using System;
using DragHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(CardInTableDetect))]
    public class CardInTable : Card
    {
        private Image _image;
        private ICardData _data;
        private CardInTableDetect _detect;
        
        public override ICardData Data => _data;

        public override void Init(ICardData data)
        {
            if (_data != null)
                throw new ArgumentException();
            
            _data = data;
            _image.sprite = data.Sprite;
            _detect.Init(_data);
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _detect = GetComponent<CardInTableDetect>();
        }
    }
}