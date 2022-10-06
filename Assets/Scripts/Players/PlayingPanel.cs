using System;
using UnityEngine;
using UnityEngine.UI;


namespace Players
{
    public class PlayingPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _buttonText;

        public event Action OnButtonClicked;

        public void Enable()
        {
            _button.onClick.AddListener(Click);
        }
        
        public void Disable()
        {
            _button.onClick.RemoveListener(Click);
        }

        public void Draw(string text)
        {
            if (text == null)
            {
                _button.gameObject.SetActive(false);
                return;
            }
            _buttonText.text = text;
            _button.gameObject.SetActive(true);
        }

        private void Click()
        {
            OnButtonClicked?.Invoke();
        }
    }
}