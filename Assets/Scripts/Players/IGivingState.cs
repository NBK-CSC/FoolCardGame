using System;
using States;

namespace Players
{
    public interface IGivingState
    {
        public StatusPlayer Status { get; }
        public event Action OnStatusChanged;

    }
}