using System;
using System.Collections.Generic;
using System.Linq;
using Players;
using States;

namespace Matches
{
    public class Step : IStepping
    {
        private IGettingActivePlayers _getter;
        private StateChanger _stateChanger;
        private Dictionary<IPlaying, bool> _proceedPlayers;
        private int _numberNeedProceed;
        
        public event Action StepBegun;
        public event Action StepEnded;
        
        public Step(StateChanger stateChanger, IGettingActivePlayers getter)
        {
            _stateChanger = stateChanger;
            _proceedPlayers = new Dictionary<IPlaying, bool>();
            _getter = getter;
            
            foreach (var player in getter.ActivePlayers)
                _proceedPlayers.Add(player, false);
        }
        
        public void Enable()
        {
            _stateChanger.OnStatusesChanged += UnmarkProceedPlayers;
            _stateChanger.OnStatusesChanged += SetNumberNeedProceed;
            foreach (var player in _getter.ActivePlayers)
                player.Proceeded += MarkProceedPlayers;
        }
        
        public void Disable()
        {
            _stateChanger.OnStatusesChanged -= UnmarkProceedPlayers;
            _stateChanger.OnStatusesChanged -= SetNumberNeedProceed;
            foreach (var player in _getter.ActivePlayers)
                player.Proceeded -= MarkProceedPlayers;
        }

        public void Start()
        {
            StepBegun?.Invoke();
        }

        private void UnmarkProceedPlayers()
        {
            foreach (var proceedPlayer in _proceedPlayers)
                _proceedPlayers[proceedPlayer.Key] = false;
        }

        private void SetNumberNeedProceed()
        {
            _numberNeedProceed = _stateChanger.StepStatus == StepStatus.WaitingDefend
                ? _getter.ActivePlayers.Count()
                : _getter.ActivePlayers.Count() - 1;
        }

        private void MarkProceedPlayers(IPlaying player)
        {
            _proceedPlayers[player] = true;
            if (_proceedPlayers.Count(proceedPlayer => proceedPlayer.Value) == _numberNeedProceed)
                StepEnded?.Invoke();
        }
    }
}