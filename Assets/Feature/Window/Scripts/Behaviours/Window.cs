using UnityEngine;

namespace FoolCardGame.Windows.Behaviours
{
    [RequireComponent(typeof(Canvas))]
    public class Window : MonoBehaviour
    {
        public void Open()
        {
            gameObject.SetActive(true);    
        }
        
        public void Close()
        {
            gameObject.SetActive(false);    
        }
    }
}