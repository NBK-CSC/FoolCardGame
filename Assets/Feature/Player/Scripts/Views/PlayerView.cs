using FoolCardGame.Player.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Player.Views
{
    public class PlayerView : AbstractPlayerView
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image stateBackground;
        [SerializeField] private Text nameText;

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
            stateBackground.gameObject.SetActive(state);
        }
    }
}