using System;
using DragHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(MoverCardInHand))]
    public class CardInHand : Card, ILaying, IBeating
    {
        private MoverCardInHand _mover;

        public event Action<ICardData> ToLayTried;
        public event Action<ICardData, ICardData> ToBeatTried;

        protected override void SetAwakeSettings()
        {
            _image = GetComponent<Image>();
            _mover = GetComponent<MoverCardInHand>();
        }

        private void OnEnable()
        {
            _mover.DragHandlerBegun += DisableImageRaycast;
            _mover.DragHandlerEnded += EnableImageRaycast;
        }

        private void OnDisable()
        {
            Data = null;
            _image.sprite = null;
        }

        private void DisableImageRaycast()
        {
            _image.raycastTarget = false;
        }
        
        private void EnableImageRaycast()
        {
            _image.raycastTarget = true;
        }

        public void TryLay()
        {
            ToLayTried?.Invoke(Data);
        }

        public void TryBeat(ICardData beatingCardData)
        {
            ToBeatTried?.Invoke(beatingCardData, Data);
        }
    }
}