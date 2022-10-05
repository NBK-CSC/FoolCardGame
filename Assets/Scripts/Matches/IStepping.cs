using System;

namespace Matches
{
    public interface IStepping
    {
        public event Action StepBegun;
        public event Action StepEnded;
    }
}