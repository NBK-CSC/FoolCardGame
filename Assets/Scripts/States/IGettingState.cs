using System;
using Players;

namespace States
{
    public interface IGettingState
    {
        public event Action OnStatusChanged;
        public StatusPlayer GetStatus(IGettingSmallestTrumpCard player);
    }
}