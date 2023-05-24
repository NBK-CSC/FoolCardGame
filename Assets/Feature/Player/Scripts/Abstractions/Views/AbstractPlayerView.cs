using UnityEngine;

namespace FoolCardGame.Player.Abstractions.Views
{
    public abstract class AbstractPlayerView : MonoBehaviour
    {
        public abstract void SetImage(byte[] image);

        public abstract void SetName(string name);

        public abstract void SetStatus(bool state);
    }
}