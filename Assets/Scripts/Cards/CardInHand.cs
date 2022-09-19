using System;
using DragHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(MoverCardInHand))]
    public class CardInHand : Card, ILaying, IBeating
    {
        private Image _image;
        private ICardData _data;
        private MoverCardInHand _mover;
        public override ICardData Data => _data;
        
        public event Action<ICardData> ToLayTried;
        public event Action<ICardData, ICardData> ToBeatTried;

        public override void Init(ICardData data)
        {
            if (_data != null)
                throw new ArgumentException();

            _data = data;
            _image.sprite = _data.Sprite;
        }

        private void Awake()
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
            _data = null;
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
            ToBeatTried?.Invoke(beatingCardData, _data);
        }
    }
}