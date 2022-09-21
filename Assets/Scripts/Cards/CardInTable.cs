using System;
using DragHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(CardInTableDetect))]
    public class CardInTable : Card
    {
        private CardInTableDetect _detect;
        
        public override void Init(ICardData data)
        {
            if (Data != null)
                throw new ArgumentException();
            
            Data = data;
            _image.sprite = data.Sprite;
            _detect.Init(Data);
        }

        protected override void SetAwakeSettings()
        {
            _image = GetComponent<Image>();
            _detect = GetComponent<CardInTableDetect>();
        }
    }
}