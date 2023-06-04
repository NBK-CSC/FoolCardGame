using System;
using FoolCardGame.Rooms.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Views
{
    /// <summary>
    /// Вью элемента комнаты
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class RoomElementView : AbstractRoomElementView
    {
        [SerializeField] private Text textName;
        [SerializeField] private Text textNumber;
        [SerializeField] private string numberFormat = "{0}/{1}";
        private Toggle _toggle;

        public override event Action<bool> OnSelected = delegate {  };

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }
        
        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(Select);
        }
        
        private void Select(bool state)
        {
            OnSelected.Invoke(state);
        }
        
        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(Select);
        }

        public override void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
        
        public override void SetToggleGroup(ToggleGroup group)
        {
            _toggle.group = group;
        }

        public override void UpdateRoomInfo(string name, int count, int maxCount)
        {
            textName.text = name;
            textNumber.text = string.Format(numberFormat, count, maxCount);
        }
    }
}