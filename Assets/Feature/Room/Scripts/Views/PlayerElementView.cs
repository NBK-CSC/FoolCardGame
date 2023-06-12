using FoolCardGame.Rooms.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Views
{
    /// <summary>
    /// Вью элемента игрока
    /// </summary>
    public class PlayerElementView : AbstractPlayerElementView
    {
        [SerializeField] private Image icon;
        [SerializeField] private Slider slider;
        [SerializeField] private Text nameText;
        [SerializeField] private Image statusImage;

        public override void SetImage(byte[] image)
        {
            //icon.sprite = Sprite.Create(Texture2D.), );
        }

        public override void SetName(string name)
        {
            nameText.text = name;
        }

        public override void SetStatus(bool state)
        {
            statusImage.gameObject.SetActive(state);
        }
    }
}