using System;
using FoolCardGame.Windows.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Windows.Behaviours
{
    /// <summary>
    /// Тоггл переключения окна
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class WindowToggle : MonoBehaviour
    {
        [SerializeField] private Window window;
        
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(SelectWindow);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(SelectWindow);
        }

        private void SelectWindow(bool state)
        {
            window.SetActive(state);
        }
    }
}