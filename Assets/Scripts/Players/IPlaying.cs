using System;

namespace Players
{
    public interface IPlaying
    {
        public event Action<IPlaying> Proceeded;
    }
}