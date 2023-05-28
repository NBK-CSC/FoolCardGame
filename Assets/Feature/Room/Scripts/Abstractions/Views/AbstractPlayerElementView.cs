using UnityEngine;

namespace FoolCardGame.Rooms.Abstractions.Views
{
    public abstract class AbstractPlayerElementView : MonoBehaviour
    {
        public abstract void SetImage(byte[] image);

        public abstract void SetName(string name);

        public abstract void SetStatus(bool state);
    }
}