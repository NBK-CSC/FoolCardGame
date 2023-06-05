using UnityEngine;

namespace FoolCardGame.Windows.Entities
{
    /// <summary>
    /// Окно
    /// </summary>
    public class Window : MonoBehaviour
    {
        /// <summary>
        /// Устнавить активность
        /// </summary>
        /// <param name="state"></param>
        public void SetActive(bool state)
        {
            gameObject.SetActive(state);    
        }
    }
}