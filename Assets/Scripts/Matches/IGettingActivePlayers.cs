using System.Collections.Generic;
using Players;

namespace Matches
{
    public interface IGettingActivePlayers
    {
        public IEnumerable<IPlaying> ActivePlayers { get; }
    }
}