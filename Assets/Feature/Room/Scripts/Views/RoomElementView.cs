using FoolCardGame.Rooms.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Views
{
    /// <summary>
    /// Вью элемента комнаты
    /// </summary>
    public class RoomElementView : AbstractRoomElementView
    {
        [SerializeField] private Text textName;
        [SerializeField] private Text textNumber;
        [SerializeField] private string numberFormat = "{0}/{1}";

        public override void SetActive(bool state) => gameObject.SetActive(state);

        public override void UpdateRoomInfo(string name, int count, int maxCount)
        {
            textName.text = name;
            textNumber.text = string.Format(numberFormat, count, maxCount);
        }
    }
}