using System;
using States;

namespace Players
{
    public class Panel
    {
        private IGivingState _giverState;

        public event Action<StatusPlayer> OnStatusChanged;
        
        public Panel(IGivingState giverState)
        {
            _giverState = giverState;
        }

        public void Enable()
        {
            _giverState.OnStatusChanged += Change;
        }
        
        public void Disable()
        {
            _giverState.OnStatusChanged -= Change;
        }

        private void Change()
        {
            OnStatusChanged?.Invoke(_giverState.Status);
        }

        public void Proceed()
        {
            
        }
    }
}