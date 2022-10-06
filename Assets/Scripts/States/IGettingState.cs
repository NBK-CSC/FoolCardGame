using System;
using Players;

namespace States
{
    public interface IGettingState
    {
        public event Action OnStatusesChanged;
        public StatusPlayer GetStatus(IGettingSmallestTrumpCard player);
    }
}